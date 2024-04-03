namespace VideoApi.Dto
{
    public class VideoUploadDto
    {
        public string VideoId { get; set; } = null!;
        public string VideoLocation { get; set; } = null!;
        public string VideoChunkNumber { get; set; }
        public IFormFile VideoChunk { get; set; } = null!;
    }
}
