namespace FogTracker.Data.Postgres.Repositories
{
    using Contracts.Repositories;
    using Model.Entities;

    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(FogContext fogContext) : base(fogContext)
        {
        }
    }
}