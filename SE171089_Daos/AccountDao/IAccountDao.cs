using SE171089_BusinessObjects;

namespace SE171089_Daos.AccountDao
{
    public interface IAccountDao : IDao<Account>
    {
        Task<Account?> GetAccountByEmail(string email);
        Task<List<Account>> GetAccountsByRoleId(int roleId);
        Task<List<Account>> GetActiveAccounts();
        Task<List<Account>> SearchAccounts(string keyword);
    }
}
