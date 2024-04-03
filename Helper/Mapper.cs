using AutoMapper;
using VideoApi.Dto;
using VideoApi.Models;

namespace VideoApi.Helper
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<VideoMetaDataDto, VideoMetaData>();
        }
    }
}
