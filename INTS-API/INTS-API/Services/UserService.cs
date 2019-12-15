using INTS_API.Entities;
using INTS_API.Interfaces;
using INTS_API.Services.Models;
using System.Threading.Tasks;

namespace INTS_API.Services
{
    public class UserService : IUserService
    {
        protected readonly IHasher _hasher;
        protected readonly ITokenManager _token;
        protected readonly IUserRepository _repository;

        public UserService(IHasher hasher, ITokenManager token, IUserRepository repository)
        {
            _hasher = hasher;
            _token = token;
            _repository = repository;
        }

        public virtual async Task<AuthLoginResult> Login(string userName, string plainPassword)
        {
            //Dohvati korisnika iz baze
            User fetchedUser = await _repository.GetUserByNameAsync(userName);

            var result = new AuthLoginResult();

            if (fetchedUser != null) //ako korisnik postoji
            {
                //provjeri password
                bool goodPassword = await _hasher.CheckPassword(fetchedUser.HashedPassword, plainPassword);
                if (!goodPassword)
                {
                    result.SetErrorMessage("Wrong password :(");
                    return result;
                }

                //kreiraj jwt token (korisnik je uspjesno prijavljen)
                result.Token = _token.CreateJWTToken(fetchedUser);
                return result;
            }

            //Vrati poruku da korisnik ne postoji
            result.SetErrorMessage("User doesnt exist");
            return result;
        }

        public virtual async Task<ServiceResult> Register(string userName, string plainPassword)
        {
            //Dohvati korisnika iz baze
            User fetchedUser = await _repository.GetUserByNameAsync(userName);

            //objekt koji predstavlja rezultt ove metode
            var result = new ServiceResult();

            if (fetchedUser != null) //korisnik vec postoji
            {
                result.SetErrorMessage($"Oh boy username {userName} is alreday taken");
                return result;
            }

            //kreiraj novog korisnika i hashiraj password
            User newUser = new User()
            {
                UserName = userName,
                HashedPassword = await _hasher.HashPassword(plainPassword)
            };

            //Dodaj korisnika u bazu (ovaj korisnik ce sada imati id ako se uspjesno doda u bazu, inace vraca null)
            newUser = await _repository.AddAsync(newUser);

            if(newUser == null) //Desila se pogreška u bazi
            {
                result.SetErrorMessage($"Ups something went wrong :(");
                return result;
            }

            return result;
        }
    }
}
