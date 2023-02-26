using CaaS.DTO;
using AutoMapper;


namespace CaaS.Features
{
    public interface IAnalytic
    {
        //statistic
        public Task<IEnumerable<BestSellerStatsDTO>> GetBestSeller(DateTime startDate, DateTime endDate, int n);
        public Task<RevenueStatsDTO?> GetTotalRevenue(DateTime startDate, DateTime endDate);
        public Task<CartsStatsDTO> GetCartsStats(DateTime startDate);
    }
}
