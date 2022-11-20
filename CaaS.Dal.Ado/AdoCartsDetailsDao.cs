  using Dal.Common;
//using Microsoft.Data.SqlClient;
using CaaS.Dal.Interface;
using CaaS.Domain;
using static CaaS.Dal.Ado.AdoMapDao;
using System.Text.RegularExpressions;

namespace CaaS.Dal.Ado;

public class AdoCartDetailsDao : ICartDetailsDao
{
    private readonly AdoTemplate template;
    private readonly AdoProductDao productDao;
    private readonly AdoCartDao cartDao;

    public AdoCartDetailsDao(IConnectionFactory connectionFactory)
    {
        this.template = new AdoTemplate(connectionFactory);
        this.cartDao = new AdoCartDao(connectionFactory);
        this.productDao = new AdoProductDao(connectionFactory);
    }

    public async Task<IEnumerable<CartDetails>> FindAllAsync(string table)
    {
        return await template.QueryAsync($"select * from {table}", MapRowToCartDetails);
    }

    public async Task<CartDetails?> FindByIdAsync(string id,string table)
    {
        return await template.QuerySingleAsync($"select * from {table} where cart_details_id=@id",
            MapRowToCartDetails,
            new QueryParameter("@id", id));
    }

    public async Task<bool> UpdateAsync(CartDetails cartDetails,string table)
    {
        CartDetails? cd = await FindByIdAsync(cartDetails.Id, table);

        if (cd is not null)
        {
            string sqlcmd = $"update {table} set cart_id=@cartId, product_id=@productId, qty=@quantity where cart_details_id=@id";
            // array für die Parameter erstellen
            return await template.ExecuteAsync(@sqlcmd,
                new QueryParameter("@id", cartDetails.Id),
                new QueryParameter("@cartId", cartDetails.CartId),
                new QueryParameter("@productId", cartDetails.ProductId),
                new QueryParameter("@quantity", cartDetails.Quantity)) == 1;
        }
        return false;
    }

    public async Task<bool> DeleteByIdAsync(string id, string table)
    {
        CartDetails? cd = await FindByIdAsync(id, table);

        if (cd is not null)
        {
            string sqlcmd = $"delete from {table} where cart_details_id=@id";
            return await template.ExecuteAsync(@sqlcmd,
                   new QueryParameter("@id", id)) == 1;
        }
        return false;
    }

    public async Task<bool>StoreAsync(CartDetails cartDetails, string table)
    {
        CartDetails? cd = await FindByIdAsync(cartDetails.Id, table);

        if (cd is null)
        {
            string sqlcmd = $"insert into {table} ( cart_details_id,cart_id, product_id, qty ) " +
            "values (@id,@cartId, @productId,@quantity)";

            return await template.ExecuteAsync(@sqlcmd,
                new QueryParameter("@id", cartDetails.Id),
                new QueryParameter("@cartId", cartDetails.CartId),
                new QueryParameter("@productId", cartDetails.ProductId),
                new QueryParameter("@quantity", cartDetails.Quantity)
                ) == 1;
        }
        return await UpdateAsync(cartDetails, table);
    }
}
