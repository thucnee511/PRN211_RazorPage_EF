using SE171089_BusinessObjects;
using SE171089_Daos.RentDetailDao;

namespace SE171089_Repositories.RentDetailRepository
{
    public class RentDetailRepository : IRentDetailRepository
    {
        private static RentDetailRepository? instance;
        private readonly IRentDetailDao rentDetailDao;
        private RentDetailRepository()
        {
            rentDetailDao = RentDetailDao.Instance;
        }
        public static RentDetailRepository Instance
        {
            get
            {
                instance ??= new RentDetailRepository();
                return instance;
            }
        }

        public async Task<RentDetail?> Add(RentDetail obj)
        {
            return await rentDetailDao.Add(obj);
        }

        public async Task<RentDetail?> Delete(int id)
        {
            return await rentDetailDao.Delete(id);
        }

        public async Task<List<RentDetail>> GetDetailsByRentId(int rentId)
        {
            return await rentDetailDao.GetDetailsByRentId(rentId);
        }

        public async Task<List<RentDetail>> GetList()
        {
            return await rentDetailDao.GetList();
        }

        public async Task<RentDetail?> GetOne(int id)
        {
            return await rentDetailDao.GetOne(id);
        }

        public async Task<RentDetail?> Update(RentDetail obj)
        {
            return await rentDetailDao.Update(obj);
        }
    }
}
