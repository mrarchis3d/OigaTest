using AutoMapper;
using Domain.DTOs.User;
using Domain.Interfaces.UnitOfWork;
using Domain.Utils;
using MediatR;

namespace Application.Queries.User;

public class UserGetQuery : UserPaged, IRequest<PagedResult<UserDTO>>
{
}
public class UserGetQueryHandler : IRequestHandler<UserGetQuery, PagedResult<UserDTO>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public UserGetQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper= mapper;
        _unitOfWork= unitOfWork;
	}

    public async Task<PagedResult<UserDTO>> Handle(UserGetQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.GetUserRepository<Domain.EntitiesModels.User, int>().GetFilteredUsers(request);
        PagedResult<UserDTO> proccessed = new PagedResult<UserDTO>
        {
            Total = result.Total,
            Results = new List<UserDTO>()  
        };
        if (result.Results!= null && result.Results.Any())
        {
            proccessed.Results = result.Results.ToList()!.Select(x =>
            new UserDTO
            {
                FullName = $"{x.FirstName} {x.LastName}",
                DateCreated = x.DateCreated,
                UserName = x.UserName,
                Id = x.Id
            }).ToList();
        }

        return proccessed;
    }
}
