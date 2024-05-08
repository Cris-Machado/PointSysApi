

using Point.API.Domain.Interfaces.Base;
using Point.API.Domain.Interfaces.Repositories;
using Point.API.Domain.Services;

namespace Point.API.Domain.Interfaces.Services
{
    public class UsersService : ServiceBase<User>, IUsersService
    {
        public UsersService(IUnitOfWork unitOfWork, IRepositoryBase<User> repository) : base(unitOfWork, repository)
        {
        }

        #region Methods
        public string Calculo()
        {
            return "teste";
        }
        #endregion
    }
}
