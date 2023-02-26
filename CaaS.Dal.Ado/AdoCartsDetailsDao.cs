  using Dal.Common;
using CaaS.Dal.Interfaces;
using CaaS.Domain;
using static CaaS.Dal.Ado.AdoMapDao;

namespace CaaS.Dal.Ado;

public class AdoCartDetailsDao : AdoGenericDao<CartDetails>,IBaseDao<CartDetails>
{   

    public AdoCartDetailsDao(IConnectionFactory connectionFactory): base(connectionFactory)
    {
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
    
    public async Task<CartDetails?> FindOpenCartDetailsByCartIdAndProductId(string openCartId, string productId,string table)
    {
        string sqlcmd = $"select * from {table} where product_id=@productId and cart_id=@cartId";
        return await template.QuerySingleAsync(@sqlcmd, MapRowToCartDetails,
            new QueryParameter("@productId", productId),
            new QueryParameter("@cartId", openCartId));  
    }
    
    public async Task<IEnumerable<CartDetails?>> FindOpenCartDetailsByCartId(string openCartId, string table)
    {
        string sqlcmd = $"select * from {table} where cart_id=@cartId";
        return await template.QueryAsync(@sqlcmd,MapRowToCartDetails,           
            new QueryParameter("@cartId", openCartId));  
    }
    
    public async Task<CartDetails?> CreateCartDetails(string openCartId, string productId, double quantity, string table)
    {
        string sqlcmd = $"select count (*) from {table} where cart_id=@cartId";
        var nrCartDetails = await template.ExecuteCountAsync(@sqlcmd, new QueryParameter("@cartId", openCartId));
        sqlcmd = $"select * from Products where product_id=@productId";
        var product = await template.QuerySingleAsync(@sqlcmd, MapRowToProduct, new QueryParameter("@productId", productId));
        var newCartDet = new CartDetails($"cartDet{nrCartDetails + 1}-{openCartId}-{product.ShopId}", 
            openCartId, productId,quantity, product.ShopId);
        await StoreAsync(newCartDet, table);
        return newCartDet;
    }

    public async Task<bool> DeleteCartDetailsByCartId(string cartId,string table)
    {
        string sqlcmd = $"delete from {table} where cart_id=@cartId";
        return await template.ExecuteAsync(@sqlcmd,
                   new QueryParameter("@cartId", cartId)) == 1;      
    }

    public async Task<bool>StoreAsync(CartDetails cartDetails, string table)
    {
        CartDetails? cd = await FindByIdAsync(cartDetails.Id, table);

        if (cd is null)
        {
            string sqlcmd = $"insert into {table} ( cart_details_id,cart_id, product_id, qty,shop_id ) " +
            "values (@id,@cartId, @productId,@quantity,@shopId)";

            return await template.ExecuteAsync(@sqlcmd,
                new QueryParameter("@id", cartDetails.Id),
                new QueryParameter("@cartId", cartDetails.CartId),
                new QueryParameter("@productId", cartDetails.ProductId),
                new QueryParameter("@quantity", cartDetails.Quantity),
                new QueryParameter("@shopId", cartDetails.ShopId)
                ) == 1;
        }
        return await UpdateAsync(cartDetails, table);
    }
}
