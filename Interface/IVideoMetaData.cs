using VideoApi.Models;

namespace VideoApi.Interface
{
    public interface IVideoMetaData
    {
        public void addVideoData(VideoMetaData videoData);
        public VideoMetaData getRow(string videoId);
        public void updateUploadStatus(VideoMetaData row);
    }
}
