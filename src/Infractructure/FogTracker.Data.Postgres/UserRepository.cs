namespace FogTracker.Repos
{
    using Contracts.Repositories;
    using Model;
    using Model.Entities;

    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(FogContext fogContext) : base(fogContext)
        {
        }
    }
}