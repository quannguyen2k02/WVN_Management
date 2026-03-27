using AutoMapper;
using BusinessLogicLayer.ModelDTOs.LED;
using DataAccessLayer.Entities.LED;
namespace BusinessLogicLayer.ExternalServices.Mapper
{
    public class ApplicationMapper:Profile
    {
        public ApplicationMapper() 
        {
            CreateMap<LED, LEDDTO>();
            CreateMap<Job, JobDTO>();
            CreateMap<LedCamera, LedCameraDTO>();
            CreateMap<LedModel, LedModelDTO>();
            CreateMap<LedModelConfig, LedModelConfigDTO>();
            CreateMap<LedStatus, LedStatusDTO>();
            CreateMap<Line, LineDTO>();

            //reverse mapping
            CreateMap<LEDDTO, LED>();
            CreateMap<JobDTO, Job>();
            CreateMap<LedCameraDTO, LedCamera>();
            CreateMap<LedModelDTO, LedModel>();
            CreateMap<LedModelConfigDTO, LedModelConfig>();
            CreateMap<LedStatusDTO, LedStatus>();
            CreateMap<LineDTO, Line>();
        }     
    }
}
