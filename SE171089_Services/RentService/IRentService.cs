using SE171089_BusinessObjects;

namespace SE171089_Services.RentService
{
    public interface IRentService
    {
        Task<Rent?> GetRentById(int value);
        Task<List<Rent>> GetRents();
        Task<List<Rent>> GetRentsByAccount(int accountId);
        Task<List<RentDetail>> GetRentDetails(int rentId);
        Task<Rent?> MarkReturn(Rent rent);
        Task<Rent?> Remove(Rent rent);
        Task<Rent?> RentBook(int accountId,int total, List<RentDetail> rentDetails);
    }
}
