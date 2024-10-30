using SE171089_Repositories.RentDetailRepository;
using SE171089_Repositories.RentRepository;

namespace SE171089_Services.RentService
{
    public class RentService : IRentService
    {
        private static RentService instance;
        private readonly IRentRepository rentRepository;
        private readonly IRentDetailRepository rentDetailRepository;
        private RentService()
        {
            rentRepository = RentRepository.Instance;
            rentDetailRepository = RentDetailRepository.Instance;
        }
        public static RentService Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }
    }
}
