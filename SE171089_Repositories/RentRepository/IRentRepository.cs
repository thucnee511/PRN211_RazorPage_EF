using SE171089_BusinessObjects;

namespace SE171089_Repositories.RentRepository
{
    public interface IRentRepository : IRepository<Rent>
    {
        Task<List<Rent>> GetRentsByAccountId(int accountId);
        Task<List<Rent>> GetRentingRents();
        Task<List<Rent>> GetReturnedRents();
        Task<List<Rent>> GetRentsInTime(DateTime from, DateTime to);
        Task<List<Rent>> GetAccountRentsInTime(int accountId, DateTime from, DateTime to);
    }
}
