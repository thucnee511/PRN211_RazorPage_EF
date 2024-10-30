using SE171089_BusinessObjects;
using SE171089_Daos.RentDao;

namespace SE171089_Repositories.RentRepository
{
    public class RentRepository : IRentRepository
    {
        private static RentRepository? insatnce;
        private readonly IRentDao rentDao;
        private RentRepository()
        {
            rentDao = RentDao.Instance;
        }
        public static RentRepository Instance
        {
            get
            {
                insatnce ??= new RentRepository();
                return insatnce;
            }
        }

        public async Task<Rent?> Add(Rent obj)
        {
            return await rentDao.Add(obj);
        }

        public async Task<Rent?> Delete(int id)
        {
            return await rentDao.Delete(id);
        }

        public async Task<List<Rent>> GetAccountRentsInTime(int accountId, DateTime from, DateTime to)
        {
            return await rentDao.GetAccountRentsInTime(accountId, from, to);
        }

        public async Task<List<Rent>> GetList()
        {
            return await rentDao.GetList();
        }

        public async Task<Rent?> GetOne(int id)
        {
            return await rentDao.GetOne(id);
        }

        public async Task<List<Rent>> GetRentingRents()
        {
            return await rentDao.GetRentingRents();
        }

        public async Task<List<Rent>> GetRentsByAccountId(int accountId)
        {
            return await rentDao.GetRentsByAccountId(accountId);
        }

        public async Task<List<Rent>> GetRentsInTime(DateTime from, DateTime to)
        {
            return await rentDao.GetRentsInTime(from, to);
        }

        public async Task<List<Rent>> GetReturnedRents()
        {
            return await rentDao.GetReturnedRents();
        }

        public async Task<Rent?> Update(Rent obj)
        {
            return await rentDao.Update(obj);
        }
    }
}
