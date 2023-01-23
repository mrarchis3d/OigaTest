using ApplicationMediatr.Exceptions;
using AutoMapper;
using Domain.DTOs.User;
using Domain.EntitiesModels;
using Domain.Interfaces.UnitOfWork;
using MediatR;

namespace ApplicationMediatr.Commands.User;

public class UserCreateUpdateCommand : UserCreateUpdate, IRequest<UserDTO>
{
}
public class UserCreateUpdateCommandHandler : IRequestHandler<UserCreateUpdateCommand, UserDTO>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public UserCreateUpdateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<UserDTO> Handle(UserCreateUpdateCommand request, CancellationToken cancellationToken)
    {

        var userCreate = _mapper.Map<Domain.EntitiesModels.User>(request);
        Domain.EntitiesModels.User userGenerated;
        if (!request.Id.HasValue)
        {
            userCreate.DateCreated = DateTime.Now;
            userGenerated = await _unitOfWork.GetRepository<Domain.EntitiesModels.User, int>().AddAsync(userCreate);
        }
        else
        {
            var findUser = await _unitOfWork.GetRepository<Domain.EntitiesModels.User, int>().GetByIdAsync(request.Id.Value);
            if (findUser == null)
                throw new NotFoundException("User not Found");
            userGenerated = await _unitOfWork.GetRepository<Domain.EntitiesModels.User, int>().UpdateAsync(userCreate);
            userGenerated.DateCreated = findUser.DateCreated;
        }
        _unitOfWork.SaveChanges();
        return _mapper.Map<UserDTO>(userGenerated);
    }
}
