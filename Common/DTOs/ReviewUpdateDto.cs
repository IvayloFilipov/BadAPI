namespace Common.DTOs
{
    public class ReviewUpdateDto
    {
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public string? ReviewerName { get; set; }
    }
}
