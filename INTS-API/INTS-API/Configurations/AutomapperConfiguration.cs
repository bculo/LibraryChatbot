using AutoMapper;
using INTS_API.Entities;
using INTS_API.Interfaces;
using INTS_API.Models.BookAPI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace INTS_API.Configurations
{
    public class AutomapperConfiguration : IInstaller
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<BookModel, Book>().ReverseMap();

                CreateMap<Book, BookResponseModel>();
            }
        }
    }
}
