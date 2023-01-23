using AutoMapper;
using ProjectCharterDAO = Domain.EntitiesModels.ProjectCharter;

using Domain.DTOs.ProjectCharter;


namespace Application.MapperProfiles.ProjectCharter
{
    public class ProjectCharterProfile : Profile
    {
        public ProjectCharterProfile()
        {

            #region ProjectCharter
            CreateMap<ProjectCharterDTO, ProjectCharterDAO>().ReverseMap();

            CreateMap<ProjectCharterCreate, ProjectCharterDAO>().ReverseMap();

            CreateMap<ProjectCharterCreate, ProjectCharterDTO>().ReverseMap();

            CreateMap<ProjectCharterUpdate, ProjectCharterDAO>().ReverseMap();

            CreateMap<ProjectCharterUpdate, ProjectCharterDTO>().ReverseMap();
            #endregion

        }
    }
}
