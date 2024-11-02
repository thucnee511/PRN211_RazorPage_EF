using Microsoft.EntityFrameworkCore;
using SE171089_BusinessObjects;

namespace SE171089_Daos.RentDetailDao
{
    public class RentDetailDao : IRentDetailDao
    {
        private static RentDetailDao? instance;
        private readonly LibraryManagementContext context;
        private RentDetailDao()
        {
            context = new();
        }
        public static RentDetailDao Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<RentDetail?> Add(RentDetail obj)
        {
            context.RentDetails.Add(obj);
            RentDetail? rentDetail = await context.RentDetails
                .OrderByDescending(rd => rd.Id).FirstOrDefaultAsync();
            return rentDetail;
        }

        public async Task<RentDetail?> Delete(int id)
        {
            RentDetail? rentDetail = await GetOne(id) ?? throw new KeyNotFoundException("RentDetail not found");
            context.RentDetails.Remove(rentDetail);
            await context.SaveChangesAsync();
            return rentDetail;
        }

        public async Task<List<RentDetail>> GetDetailsByRentId(int rentId)
        {
            List<RentDetail> list = await context.RentDetails
                .Where(rd => rd.RentId == rentId).ToListAsync();
            return list;
        }

        public async Task<List<RentDetail>> GetList()
        {
            List<RentDetail> list = await context.RentDetails.ToListAsync();
            return list;
        }

        public async Task<RentDetail?> GetOne(int id)
        {
            RentDetail? rentDetail = await context.RentDetails
                .FirstOrDefaultAsync(rd => rd.Id == id);
            return rentDetail;
        }

        public async Task<RentDetail?> Update(RentDetail obj)
        {
            context.RentDetails.Update(obj);
            await context.SaveChangesAsync();
            RentDetail? rentDetail = await GetOne(obj.Id);
            return rentDetail;
        }
    }
}
