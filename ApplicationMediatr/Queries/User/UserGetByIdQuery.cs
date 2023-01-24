using AutoMapper;
using Domain.DTOs.User;
using Domain.EntitiesModels;
using Domain.Interfaces.UnitOfWork;
using MediatR;

namespace ApplicationMediatr.Queries.User;


public class UserGetByIdQuery : BaseId, IRequest<UserDTO>
{
}
public class UserGetByIdQueryHandler : IRequestHandler<UserGetByIdQuery, UserDTO>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public UserGetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDTO> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.GetUserRepository<Domain.EntitiesModels.User, int>().GetByIdAsync(request.Id);
        return _mapper.Map<UserDTO>(result);
    }
}