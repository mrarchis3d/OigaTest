using Application.Exceptions;
using AutoMapper;
using Domain.DTOs.User;
using UserEnity = Domain.EntitiesModels.User;
using Domain.Interfaces.UnitOfWork;
using MediatR;

namespace Application.Commands.User;

public class UserCreateUpdateCommand : UserCreateUpdate, IRequest<UserEnity>
{
}
public class UserCreateUpdateCommandHandler : IRequestHandler<UserCreateUpdateCommand, UserEnity>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public UserCreateUpdateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<UserEnity> Handle(UserCreateUpdateCommand request, CancellationToken cancellationToken)
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
        return userGenerated;
    }
}
