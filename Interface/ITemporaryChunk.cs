using VideoApi.Models;

namespace VideoApi.Interface
{
    public interface ITemporaryChunk
    {
        public TemporaryChunkDetails getRow(string videoId);
        public bool isVideoUploaded(TemporaryChunkDetails row);
        public void setCurrentChunk(TemporaryChunkDetails row, int chunkNumber);
        public void deleteChunk(TemporaryChunkDetails row);
        public void addChunk(TemporaryChunkDetails tcd);
    }
}
