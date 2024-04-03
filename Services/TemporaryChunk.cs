using Microsoft.IdentityModel.Tokens;
using VideoApi.Interface;
using VideoApi.Models;

namespace VideoApi.Services
{
    public class TemporaryChunk : ITemporaryChunk
    {
        private readonly VideoUploadContext _context;

        public TemporaryChunk(VideoUploadContext context)
        {
            _context = context;
        }

        public TemporaryChunkDetails getRow(string videoId)
        {
            var res = _context.TemporaryChunkDetails.Single(tcd => tcd.VideoId == videoId);
            return res;
        }

        public void deleteChunk(TemporaryChunkDetails row)
        {
            _context.TemporaryChunkDetails.Remove(row);
            _context.SaveChanges();
        }

        public bool isVideoUploaded(TemporaryChunkDetails row)
        {
            if(row.CurrentChunk == row.TotalChunks)
            {
                return true;
            }
            return false;
        }

        public void setCurrentChunk(TemporaryChunkDetails row, int chunkNumber)
        {
            row.CurrentChunk = chunkNumber;
            _context.SaveChanges();
        }

        public void addChunk(TemporaryChunkDetails tcd)
        {
            _context.TemporaryChunkDetails.Add(tcd);
            _context.SaveChanges();
        }
    }
}
