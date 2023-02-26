using System.Data;

namespace CaaS.DTO
{
    public class AdoMapDTO {                

        public static BestSellerStatsDTO MapRowToBestSellerStats(IDataRecord row) =>
        new(
            id: (string)row["product_id"],
            totalQuantity: (double)row["AmountSold"]
        ); 
        
        public static RevenueStatsDTO MapRowToRevenueStats(IDataRecord row) =>
        new(
            totalRevenue: (double)row["total_revenue"],
            totalQuantity: (double)row["total_quantity"]
        );
        
        public static CartsStatsDTO MapRowToCartStats(IDataRecord row) =>
        new(
            totalOpenCart: (double)row["total_open_carts"],
            totalClosedCart: (double)row["total_closed_carts"]
        );

    }
}
