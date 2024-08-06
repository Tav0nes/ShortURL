namespace ShortURL.Models
{
    public class URL
    {
        public string OriginalURL { get; set; } = string.Empty;
        public string ShortenedUrl { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; } = string.Empty;

    }
}