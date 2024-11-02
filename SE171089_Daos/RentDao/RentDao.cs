using Microsoft.EntityFrameworkCore;
using SE171089_BusinessObjects;

namespace SE171089_Daos.RentDao
{
    public class RentDao : IRentDao
    {
        private static RentDao? instance;
        private readonly LibraryManagementContext context;
        private RentDao()
        {
            context = new();
        }
        public static RentDao Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<Rent?> Add(Rent obj)
        {
            context.Rents.Add(obj);
            await context.SaveChangesAsync();
            Rent? rent = await context.Rents.OrderByDescending(r => r.Id).FirstOrDefaultAsync();
            return rent;
        }

        public async Task<Rent?> Delete(int id)
        {
            Rent? rent = await GetOne(id) ?? throw new KeyNotFoundException("Rent not found");
            rent.Status = "removed";
            rent = await Update(rent);
            return rent;
        }

        public async Task<List<Rent>> GetAccountRentsInTime(int accountId, DateTime from, DateTime to)
        {
            List<Rent> rents = await context.Rents
                .Where(r => r.UserId == accountId && r.RentTime >= from && r.RentTime <= to)
                .ToListAsync();
            return rents;
        }

        public async Task<List<Rent>> GetList()
        {
            List<Rent> rents = await context.Rents.ToListAsync();
            return rents;
        }

        public async Task<Rent?> GetOne(int id)
        {
            Rent? rent = await context.Rents
                .FirstOrDefaultAsync(r => r.Id == id);
            return rent;
        }

        public async Task<List<Rent>> GetRentingRents()
        {
            List<Rent> rents = await context.Rents
                .Where(r => r.Status.ToLower() == "renting")
                .ToListAsync();
            return rents;
        }

        public async Task<List<Rent>> GetRentsByAccountId(int accountId)
        {
            List<Rent> rents = await context.Rents
                .Where(r => r.UserId == accountId)
                .ToListAsync();
            return rents;
        }

        public async Task<List<Rent>> GetRentsInTime(DateTime from, DateTime to)
        {
            List<Rent> rents = await context.Rents
                .Where(r => r.RentTime >= from && r.RentTime <= to)
                .ToListAsync();
            return rents;
        }

        public async Task<List<Rent>> GetReturnedRents()
        {
            List<Rent> rents = await context.Rents
                .Where(r => r.Status.ToLower() == "returned")
                .ToListAsync();
            return rents;
        }

        public async Task<Rent?> Update(Rent obj)
        {
            context.Rents.Update(obj);
            await context.SaveChangesAsync();
            Rent? rent = await GetOne(obj.Id);
            return rent;
        }
    }
}
