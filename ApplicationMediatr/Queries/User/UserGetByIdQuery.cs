using AutoMapper;
using Domain.DTOs.User;
using Domain.EntitiesModels;
using Domain.Interfaces.UnitOfWork;
using UserEntity = Domain.EntitiesModels.User;
using MediatR;

namespace ApplicationMediatr.Queries.User;


public class UserGetByIdQuery : BaseId, IRequest<UserEntity>
{
}
public class UserGetByIdQueryHandler : IRequestHandler<UserGetByIdQuery, UserEntity>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public UserGetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserEntity> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.GetUserRepository<UserEntity, int>().GetByIdAsync(request.Id);
    }
}