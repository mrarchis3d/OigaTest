using AutoMapper;
using Domain.EntitiesModels;
using Domain.Interfaces.Services.ProjectCharterBLL;
using Domain.Interfaces.UnitOfWork;
using Domain.DTOs.ProjectCharter;
using Application.Exceptions;

namespace Application.Services
{
    public class ProjectCharterBLL : IProjectCharterBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public ProjectCharterBLL(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ProjectCharterDTO>> GetAll()
        {
            using (var unitOfWork = _unitOfWork)
                return _mapper.Map<List<ProjectCharterDTO>>(await unitOfWork.GetRepository<ProjectCharter, int>().GetAsync());
        }

        public async Task<ProjectCharterDTO> GetById(int id)
        {
            using (var unitOfWork = _unitOfWork)
            {
                var project = await unitOfWork.GetRepository<ProjectCharter, int>().GetByIdAsync(id);

                if (project == null) throw new ValidationException("El proyecto no existe");

                return _mapper.Map<ProjectCharterDTO>(project);
            }
        }

        public async Task<ProjectCharterDTO> Update(ProjectCharterUpdate project)
        {
            using (var unitOfWork = _unitOfWork)
            {
                var projectUpdate = await unitOfWork.GetRepository<ProjectCharter, int>().GetByIdAsync(project.Id);

                if(projectUpdate == null) throw new ValidationException("No existe el proyecto para actualizar");

                _mapper.Map(project, projectUpdate);

                await unitOfWork.GetRepository<ProjectCharter, int>().UpdateAsync(projectUpdate);

                unitOfWork.SaveChanges();

                return _mapper.Map<ProjectCharterDTO>(projectUpdate);
            }
        }



        public async Task<ProjectCharterDTO> Create(ProjectCharterCreate project)
        {
            using (var unitOfWork = _unitOfWork)
            {
                var projectCharterCreate = _mapper.Map<ProjectCharter>(project);
                projectCharterCreate.CreateDate = DateTime.Now;

                await unitOfWork.GetRepository<ProjectCharter, int>().AddAsync(projectCharterCreate);
                unitOfWork.SaveChanges();

                return _mapper.Map<ProjectCharterDTO>(projectCharterCreate);
            }
        }

        public async Task<ProjectCharterDTO> Delete(int id)
        {
            using (var unitOfWork = _unitOfWork)
            {
                var project = await unitOfWork.GetRepository<ProjectCharter, int>().GetByIdAsync(id);

                if (project == null) throw new ValidationException("no se puede eliminar el proyecto ya que no existe o ya fue eliminado");

                await unitOfWork.GetRepository<ProjectCharter, int>().UpdateAsync(project);
                unitOfWork.SaveChanges();

                return _mapper.Map<ProjectCharterDTO>(project);
            }
        }
    }
}
