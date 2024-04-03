using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using VideoApi.Dto;
using VideoApi.External;
using VideoApi.Interface;
using VideoApi.Models;

namespace VideoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoUploadController : ControllerBase
    {
        private ITemporaryChunk TemporaryChunk;
        private IVideoMetaData VideoMetaData;
        private IMapper mapper;
        public VideoUploadController(ITemporaryChunk _TemporaryChunk, IVideoMetaData _VideoMetaData, IMapper _Mapper)
        {
            TemporaryChunk = _TemporaryChunk;
            VideoMetaData = _VideoMetaData;
            mapper = _Mapper;
        }

        #region FILE UPLOAD
        [HttpPost("upload")]
        [RequestTimeout(60000)]
        [Consumes("multipart/form-data")]
        public ActionResult VideoFile([FromForm] VideoUploadDto videoUploadDto)
        {
            
            TemporaryChunkDetails row = TemporaryChunk.getRow(videoUploadDto.VideoId);
            if (row == null)
            {
                return Conflict("Video Already uploaded");
            }
            if (videoUploadDto.VideoChunk == null)
            {
                return Ok(new { msg = "file is empty" });
            }
            string videolocation = $"{videoUploadDto.VideoLocation}/{videoUploadDto.VideoChunk.FileName}";
            FileStream fileStream = new FileStream(videolocation, FileMode.Append, FileAccess.Write);
            using (var inputFile = videoUploadDto.VideoChunk.OpenReadStream())
            {
                inputFile.CopyTo(fileStream);
            }
            fileStream.Dispose();
            TemporaryChunk.setCurrentChunk(row, int.Parse(videoUploadDto.VideoChunkNumber));
            if (TemporaryChunk.isVideoUploaded(row))
            {
                var videoRow = VideoMetaData.getRow(videoUploadDto.VideoId);
                VideoMetaData.updateUploadStatus(videoRow);
                TemporaryChunk.deleteChunk(row);    
                new VideoSplitter().SplitVideo(videolocation);
            }
            return Ok();
        }
        #endregion

        [HttpPost]
        public ActionResult UploadVideoData([FromBody] VideoMetaDataDto videoDataDto)
        {
            var videoData = mapper.Map<VideoMetaData>(videoDataDto);
            string videoDataString = $"{videoData.VideoTitle}{videoData.VideoDescription}{videoData.Tags}";
            SHA256 sHA256 = SHA256.Create();
            byte[] videoSHA = sHA256.ComputeHash(Encoding.Unicode.GetBytes(videoDataString));

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < videoSHA.Length; i++)
            {
                stringBuilder.Append(videoSHA[i].ToString("x2"));
            }

            string basePath = $"Videos/{stringBuilder}";
            var a = Directory.CreateDirectory($"{basePath}");
            videoData.VideoId = stringBuilder.ToString();
            videoData.VideoLocation = basePath;
            videoData.ThumbnailLocation = basePath;
            VideoMetaData.addVideoData(videoData);

            var chunkData = new TemporaryChunkDetails()
            {
                VideoId = videoData.VideoId,
                TotalChunks = videoDataDto.TotalChunks
            };
            TemporaryChunk.addChunk(chunkData);
            return Ok(new {videoId = videoData.VideoId, videoLocation = basePath});
        }
    }
}
