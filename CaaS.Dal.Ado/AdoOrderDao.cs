  using Dal.Common;
//using Microsoft.Data.SqlClient;
using CaaS.Dal.Interface;
using CaaS.Domain;
using static CaaS.Dal.Ado.AdoMapDao;
using System;
using System.Text.RegularExpressions;

namespace CaaS.Dal.Ado;

public class AdoOrderDao : IOrderDao
{
    private readonly AdoTemplate template;
    private readonly AdoPersonDao personDao;
    private readonly AdoCartDao cartDao;

            
    public AdoOrderDao(IConnectionFactory connectionFactory)
    {
        template = Util.createAdoTemplate(connectionFactory) ?? throw new ArgumentNullException(nameof(connectionFactory));

        //this.template = new AdoTemplate(connectionFactory);
        this.personDao = new AdoPersonDao(connectionFactory);
        this.cartDao = new AdoCartDao(connectionFactory);
    }

    public async Task<IEnumerable<Order>> FindAllAsync(string table)
    {
        return await template.QueryAsync($"select * from {table}", MapRowToOrder);
    }

    public async Task<Order?> FindByIdAsync(string id,string table)
    {
        return await template.QuerySingleAsync($"select * from {table} where order_id=@id",
            MapRowToOrder,
            new QueryParameter("@id", id));
    }

    public async Task<bool> UpdateAsync(Order order,string table)
    {
        Order? o = await FindByIdAsync(order.Id, table);

        if (o is not null)
        {
            string sqlcmd = $"update {table} set cust_id=@custId, cart_id=@cartId, order_date=@order_date where order_id=@id";
            // array für die Parameter erstellen
            return await template.ExecuteAsync(@sqlcmd,
                new QueryParameter("@id", order.Id),
                new QueryParameter("@custId", order.CustId),
                new QueryParameter("@cartId", order.CartId),
                new QueryParameter("@order_date", order.OrderDate)) == 1;
        }
        return false;
    }

    public async Task<bool> DeleteByIdAsync(string id, string table)
    {
        Order? o = await FindByIdAsync(id, table);

        if (o is not null)
        {
            string sqlcmd = $"delete from {table} where order_id=@id";
            return await template.ExecuteAsync(@sqlcmd,
                   new QueryParameter("@id", id)) == 1;
        }
        return false;
    }

    public async Task<bool>StoreAsync(Order order, string table)
    {
        Order? o = await FindByIdAsync(order.Id, table);

        if (o is null)
        {
            string sqlcmd = $"insert into {table} ( order_id,cust_id, cart_id, order_date) " +
            "values (@id,@custId, @cartId,@order_date) ";
            //string[] substrings = Regex.Split(table, "Orders");
            //string customershoptable = "Customers"+substrings[substrings.Length - 1];
            //string cartshoptable = "Carts"+substrings[substrings.Length - 1];
            //Person? cust = await personDao.FindByIdAsync(order.CustId, customershoptable);
            //await personDao.StoreAsync(cust, customershoptable);
            //Cart? cart = await cartDao.FindByIdAsync(order.CustId, cartshoptable);
            //await cartDao.StoreAsync(cart, cartshoptable);

            return await template.ExecuteAsync(@sqlcmd,
                new QueryParameter("@id", order.Id),
                new QueryParameter("@custId", order.CustId),
                new QueryParameter("@cartId", order.CartId),
                new QueryParameter("@order_date", order.OrderDate)
                ) == 1;
        }
        return await UpdateAsync(order, table);
    }
}
