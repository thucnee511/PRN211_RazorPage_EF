using SE171089_Repositories.AccountRepository;
using SE171089_Repositories.RoleRepository;

namespace SE171089_Services.AccountService
{
    public class AccountService : IAccountService
    {
        private static AccountService? instance;
        private readonly IAccountRepository accountRepository;
        private readonly IRoleRepository roleRepository;
        private AccountService()
        {
            accountRepository = AccountRepository.Instance;
            roleRepository = RoleRepository.Instance;
        }
        public static AccountService Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }
    }
}
