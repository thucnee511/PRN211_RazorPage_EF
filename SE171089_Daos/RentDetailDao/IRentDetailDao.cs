using SE171089_BusinessObjects;

namespace SE171089_Daos.RentDetailDao
{
    public interface IRentDetailDao : IDao<RentDetail>
    {
        Task<List<RentDetail>> GetDetailsByRentId(int rentId);
    }
}
