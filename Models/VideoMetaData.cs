namespace VideoApi.Models
{
    public class VideoMetaData
    {
        public string VideoId { get; set; } = null!;
        public string VideoTitle { get; set; } = null!;
        public string VideoDescription { get; set; } = null!;
        public string VideoLocation { get; set; } = null!;
        public string ThumbnailLocation { get; set; } = null!;
        public string? Tags { get; set; } 
        public bool isVideoUploaded { get; set; } = false;
        public bool isVideoProcessed { get; set; } = false;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
        public virtual TemporaryChunkDetails TemporaryChunks { get; set; } = null!;
    }
}
