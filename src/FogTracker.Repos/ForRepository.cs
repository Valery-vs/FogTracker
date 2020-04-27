namespace FogTracker.Repos
{
    using Contracts;
    using Model;

    public class ForRepository : IFogRepository
    {
        private readonly FogContext fogContext;
        private IUserRepository userRepository;

        public ForRepository(FogContext fogContext)
        {
            this.fogContext = fogContext;
        }

        public IUserRepository Users
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(this.fogContext);
                }

                return this.userRepository;
            }
        }

        public void Save()
        {
            this.fogContext.SaveChanges();
        }
    }
}