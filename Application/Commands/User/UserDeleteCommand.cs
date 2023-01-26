using Application.Exceptions;
using AutoMapper;
using Domain.EntitiesModels;
using Domain.Interfaces.UnitOfWork;
using MediatR;

namespace Application.Commands.User
{
    public class UserDeleteCommand : BaseId,  IRequest
    {
    }
    public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UserDeleteCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
            var findUser = await _unitOfWork.GetRepository<Domain.EntitiesModels.User, int>().GetByIdAsync(request.Id);
            if (findUser == null)
                throw new NotFoundException("User not Found");
            await _unitOfWork.GetRepository<Domain.EntitiesModels.User, int>().DeleteAsync(request.Id);
            _unitOfWork.SaveChanges();
            return Unit.Value;
        }
    }
}
