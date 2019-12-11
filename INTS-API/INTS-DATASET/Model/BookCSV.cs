namespace INTS_DATASET.Model
{
    public sealed class BookCSV
    {
        public string Title { get; set; }
        public string Authors { get; set; }
        public double AvarageRating { get; set; }
        public string ISBN { get; set; }
        public string ISBN13 { get; set; }
        public string LanguageCode { get; set; }
        public int PageNumber { get; set; }
        public int RatingsCount { get; set; }
        public int TextReviewsCount { get; set; }
    }
}
