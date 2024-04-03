namespace VideoApi.Models
{
    public class TemporaryChunkDetails
    {
        public int id { get; set; }
        public string VideoId { get; set; } = null!;
        public int CurrentChunk { get; set; } = 0;
        public int TotalChunks { get; set; }
        public virtual VideoMetaData Video { get; set; } = null!;
    }
}
