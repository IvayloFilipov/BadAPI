namespace Common.DTOs
{
    public class ReviewCreateDto
    {
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public string? ReviewerName { get; set; }
    }
}
