using SE171089_BusinessObjects;

namespace SE171089_Daos.RentDao
{
    public interface IRentDao : IDao<Rent>
    {
        Task<List<Rent>> GetRentsByAccountId(int accountId);
        Task<List<Rent>> GetRentingRents();
        Task<List<Rent>> GetReturnedRents();
        Task<List<Rent>> GetRentsInTime(DateTime from, DateTime to);
        Task<List<Rent>> GetAccountRentsInTime(int accountId, DateTime from, DateTime to);
    }
}
