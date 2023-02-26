  using Dal.Common;
//using Microsoft.Data.SqlClient;
using CaaS.Dal.Interfaces;
using CaaS.Domain;
using System.Data;
using static CaaS.Dal.Ado.AdoMapDao;
using System;

namespace CaaS.Dal.Ado;

public class AdoProductDao : AdoGenericDao<Product>, IBaseDao<Product>
{
            
    public AdoProductDao(IConnectionFactory connectionFactory) : base(connectionFactory)
    {       
    }

    public async Task<bool> UpdateAsync(Product product,string table)
    {
        Product? p = await FindByIdAsync(product.Id, table);

        if (p is not null)
        {
            string sqlcmd = $"update {table} set product_name=@name, price = @price," +
            $"amount_desc=@amountDesc, product_desc=@productDesc,download_link=@downloadLink,shop_id=@shopId where product_id=@id";
            // array für die Parameter erstellen
            return await base.template.ExecuteAsync(@sqlcmd,
                new QueryParameter("@id", product.Id),
                new QueryParameter("@name", product.Name),
                new QueryParameter("@price", product.Price),
                new QueryParameter("@amountDesc", product.AmountDesc),
                new QueryParameter("@productDesc", product.ProductDesc),
                new QueryParameter("@downloadLink", product.DownloadLink),
                new QueryParameter("@shopId", product.ShopId)
                ) == 1;
        }
        return false;
    }

    public async Task<bool> DeleteByIdAsync(string id, string table)
    {
        Product? p = await FindByIdAsync(id, table);

        if (p is not null)
        {
            string sqlcmd = $"delete from {table} where product_id=@id";
            return await base.template.ExecuteAsync(@sqlcmd,
                   new QueryParameter("@id", id)) == 1;
        }
        return false;
    }

    public async Task<bool> StoreAsync(Product product, string table)
    {
        Product? p = await FindByIdAsync(product.Id, table);

        if (p is null)
        {
            string sqlcmd = $"insert into {table} (product_id,product_name, price, amount_desc, product_desc, download_link, shop_id) " +
           "values (@id,@name, @price,@amountDesc,@productDesc,@downloadLink,@shopId) ";

            return await base.template.ExecuteAsync(@sqlcmd,
                new QueryParameter("@id", product.Id),
                new QueryParameter("@name", product.Name),
                new QueryParameter("@price", product.Price),
                new QueryParameter("@amountDesc", product.AmountDesc),
                new QueryParameter("@productDesc", product.ProductDesc),
                new QueryParameter("@downloadLink", product.DownloadLink),
                new QueryParameter("@shopId", product.ShopId)
                ) == 1;
        }
        return await UpdateAsync(product, table);
    }

    public async Task<IEnumerable<Product?>>FindTByXAndY(string prodName, string prodDesc, string table)
    {
        string sqlcmd = $"select * from {table} where product_name like '%{prodName}%' or product_desc like '%{prodDesc}%' ";
        return await base.template.QueryAsync(@sqlcmd, MapRowToProduct);
    }
}
