using AutoMapper;
using CaaS.Domain;
using CaaS.DTO;
using Dal.Common;

namespace CaaS.Features
{
    public class StatsAnalytic : IAnalytic
    {
        private readonly AdoTemplate template;


        public StatsAnalytic(IConnectionFactory connectionFactory)
        {
            template = Util.createAdoTemplate(connectionFactory) ?? throw new ArgumentNullException(nameof(connectionFactory));
        }
        

        public async Task<IEnumerable<BestSellerStatsDTO>> GetBestSeller(DateTime startDate, DateTime endDate, int n)
        {
            string sqlcmd = $"Select TOP ({n}) product_id, Sum(qty) AS AmountSold FROM (SELECT TOP(1000) A.*, " +
                $"B.cust_id, B.cart_id, B.order_date  FROM OrdersDetails AS A INNER JOIN Orders AS B ON " +
                $"B.order_id = A.order_id  WHERE B.order_date <= @endDate AND B.order_date >= @startDate)" +
                $" AS TblA Group By product_id Order By AmountSold Desc";

            return await template.QueryAsync(@sqlcmd, AdoMapDTO.MapRowToBestSellerStats,
                new QueryParameter("@endDate", endDate),
                new QueryParameter("@startDate", startDate));

        }        

        public async Task<CartsStatsDTO> GetCartsStats(DateTime startDate)
        {
            string sqlcmd = $"select count(*) as total_open_carts From Carts Where status = @status";
            var totalOpenCart= await template.ExecuteCountAsync(@sqlcmd, new QueryParameter("@status", "open"));
            string sqlcmd2 = $"select count(*) as total_closed_carts From Orders Where order_date = @startDate";
            var totalClosedCart= await template.ExecuteCountAsync(@sqlcmd2, new QueryParameter("@startDate", startDate));
            return new CartsStatsDTO(totalOpenCart, totalClosedCart);
        }
               

        public async Task<RevenueStatsDTO?> GetTotalRevenue(DateTime startDate, DateTime endDate)
        {
            string sqlcmd = $"Select Sum(revenue) as total_revenue ,Sum(qty) as total_quantity " +
               $"From(Select  A.product_id, A.qty, A.unit_price * A.qty as revenue, B.cust_id," +
               $" B.cart_id, B.order_date  From OrdersDetails As A INNER JOIN Orders As B On " +
               $"B.order_id = A.order_id  Where B.order_date <= @endDate AND B.order_date >" +
               $"= @startDate) TblA";
            return await template.QuerySingleAsync(@sqlcmd, AdoMapDTO.MapRowToRevenueStats,
               new QueryParameter("@endDate", endDate),
               new QueryParameter("@startDate", startDate));
        }
    }
}
