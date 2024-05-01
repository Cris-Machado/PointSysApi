using Point.API.Domain.Services;


namespace Point.API.Domain.Interfaces.Repositories
{
    public interface IUsersService : IServiceBase<User>
    {
        string Calculo();
    }
}
