

using Point.API.Domain.Interfaces.Base;
using Point.API.Domain.Interfaces.Repositories;
using Point.API.Domain.Services;

namespace Point.API.Domain.Interfaces.Services
{
    public class UsersPointService : ServiceBase<UserPoint>, IUsersPointService
    {
        public UsersPointService(IUnitOfWork unitOfWork, IRepositoryBase<UserPoint> repository) : base(unitOfWork, repository)
        {
        }
    }
}
