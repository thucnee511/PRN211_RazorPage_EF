using SE171089_BusinessObjects;

namespace SE171089_Services.RentService
{
    public interface IRentService
    {
        Task<List<Rent>> GetRents();
        Task<List<Rent>> GetRentsByAccount(int accountId);
    }
}
