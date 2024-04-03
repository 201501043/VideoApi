using VideoApi.Interface;
using VideoApi.Models;

namespace VideoApi.Services
{
    public class VideoMetaData : IVideoMetaData
    {
        private VideoUploadContext _context;
        public VideoMetaData(VideoUploadContext context) 
        { 
            _context = context;
        }
        public void addVideoData(Models.VideoMetaData videoData)
        {
            _context.VideoMetaData.Add(videoData);
            _context.SaveChanges();
        }

        public Models.VideoMetaData getRow(string videoId)
        {
            return _context.VideoMetaData.Single(vmd => vmd.VideoId == videoId);
        }

        public void updateUploadStatus(Models.VideoMetaData row)
        {
            row.isVideoUploaded = true;
            _context.SaveChanges();
        }
    }
}
