  using Dal.Common;
//using Microsoft.Data.SqlClient;
using CaaS.Dal.Interfaces;
using CaaS.Domain;
using static CaaS.Dal.Ado.AdoMapDao;
using Org.BouncyCastle.Asn1.X509;
using System.Text.RegularExpressions;

namespace CaaS.Dal.Ado;

public class AdoOrderDetailsDao : AdoGenericDao<OrderDetails>,IBaseDao<OrderDetails>
{
       
    public AdoOrderDetailsDao(IConnectionFactory connectionFactory): base(connectionFactory)
    {
       // template = Util.createAdoTemplate(connectionFactory) ?? throw new ArgumentNullException(nameof(connectionFactory));
        //this.template = new AdoTemplate(connectionFactory);        
    }

    public async Task<bool> UpdateAsync(OrderDetails orderdetails,string table)
    {
        OrderDetails? o = await FindByIdAsync(orderdetails.Id, table);

        if (o is not null)
        {
            string sqlcmd = $"update {table} set order_id=@orderId, product_id=@productId,unit_price=@unitPrice," +
                $"qty=@quantity, discount=@discount where order_details_id=@id";
            // array für die Parameter erstellen
            return await base.template.ExecuteAsync(@sqlcmd,
                new QueryParameter("@id", orderdetails.Id),
                new QueryParameter("@orderId", orderdetails.OrderId),
                new QueryParameter("@productId", orderdetails.ProductId),
                new QueryParameter("@unitPrice", orderdetails.UnitPrice),
                new QueryParameter("@quantity", orderdetails.Quantity),
                new QueryParameter("@discount", orderdetails.Discount)
                ) == 1;
        }
        return false;
    }

    public async Task<bool> DeleteByIdAsync(string id, string table)
    {
        OrderDetails? o = await FindByIdAsync(id ,table);

        if (o is not null)
        {
            string sqlcmd = $"delete from {table} where order_details_id=@id";
            return await base.template.ExecuteAsync(@sqlcmd,
                   new QueryParameter("@id", id)) == 1;
        }
        return false;
    }

    public async Task<bool> StoreAsync(OrderDetails orderdetails, string table)
    {
        OrderDetails? od = await FindByIdAsync(orderdetails.Id, table);

        if (od is null)
        {
            string sqlcmd = $"insert into {table} (order_details_id,order_id, product_id,unit_price,qty,discount,shop_id) " +
           "values (@id,@orderId, @productId,@unitPrice, @quantity,@discount,@shopId) ";
            //string[] substrings = Regex.Split(table, "OrdersDetails");
            //string ordersshoptable = "Orders" + substrings[substrings.Length - 1];
            //string productshoptable = "Products" + substrings[substrings.Length - 1];
            //Order? o = await orderDao.FindByIdAsync(orderdetails.OrderId, ordersshoptable);
            //await orderDao.StoreAsync(o, ordersshoptable);
            //Product? prod = await productDao.FindByIdAsync(orderdetails.ProductId, productshoptable);
            //await productDao.StoreAsync(prod, productshoptable);


            return await base.template.ExecuteAsync(@sqlcmd,
                new QueryParameter("@id", orderdetails.Id),
                new QueryParameter("@orderId", orderdetails.OrderId),
                new QueryParameter("@productId", orderdetails.ProductId),
                new QueryParameter("@unitPrice", orderdetails.UnitPrice),
                new QueryParameter("@quantity", orderdetails.Quantity),
                new QueryParameter("@discount", orderdetails.Discount),
                new QueryParameter("@shopId", orderdetails.ShopId)
                ) == 1;
        }
        return await UpdateAsync(orderdetails, table);
    }

    
}
