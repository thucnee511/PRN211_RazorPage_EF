using SE171089_BusinessObjects;

namespace SE171089_Services.AccountService
{
    public interface IAccountService
    {
        Task<Account?> Delete(int id);
        Task<Account?> GetAccountById(int v);
        Task<List<Account>> getActiveAccounts();
        Task<Account?> Login(string email, string password);
        Task<Account?> Update(Account account);
        Task<Role?> GetRole(int id);
        Task<Account?> Insert(Account account);
        Task<List<Account>> Search(string keyword);
    }
}
