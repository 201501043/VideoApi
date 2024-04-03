namespace VideoApi.Dto
{
    public class VideoMetaDataDto
    {
        public string VideoTitle { get; set; } = null!;
        public string VideoDescription { get; set; } = null!;
        public string? Tags { get; set; }
        public int TotalChunks { get; set; }
    }
}
