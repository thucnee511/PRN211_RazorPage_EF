using SE171089_BusinessObjects;

namespace SE171089_Repositories.RentDetailRepository
{
    public interface IRentDetailRepository : IRepository<RentDetail>
    {
        Task<List<RentDetail>> GetDetailsByRentId(int rentId);
    }
}
