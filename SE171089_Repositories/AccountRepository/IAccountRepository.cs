using SE171089_BusinessObjects;

namespace SE171089_Repositories.AccountRepository
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account?> GetAccountByEmail(string email);
        Task<List<Account>> GetAccountsByRoleId(int roleId);
        Task<List<Account>> GetActiveAccounts();
        Task<List<Account>> SearchAccounts(string keyword);
    }
}
