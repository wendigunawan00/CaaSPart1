  using Dal.Common;
//using Microsoft.Data.SqlClient;
using CaaS.Dal.Interface;
using CaaS.Domain;
using System.Data;
using static CaaS.Dal.Ado.AdoMapDao;
using System;

namespace CaaS.Dal.Ado;

public class AdoProductDao : IProductDao
{
    private readonly AdoTemplate template;

    private Product? FindByIdSync(string id, string table)
    {
        return template.QuerySingleSync($"select * from {table} where product_id=@id",
            MapRowToProduct,
            new QueryParameter("@id", id));
    }
        
    public AdoProductDao(IConnectionFactory connectionFactory)
    {
        template = Util.createAdoTemplate(connectionFactory) ?? throw new ArgumentNullException(nameof(connectionFactory));
        //this.template = new AdoTemplate(connectionFactory);
    }

    public async Task<IEnumerable<Product>> FindAllAsync(string table)
    {
        return await template.QueryAsync($"select * from {table}", MapRowToProduct);
    }

    public async Task<Product?> FindByIdAsync(string id,string table)
    {
        return await template.QuerySingleAsync($"select * from {table} where product_id=@id",
            MapRowToProduct,
            new QueryParameter("@id", id));
    }
   
    public async Task<bool> UpdateAsync(Product product,string table)
    {
        Product? p = await FindByIdAsync(product.Id, table);

        if (p is not null)
        {
            string sqlcmd = $"update {table} set product_name=@name, price = @price," +
            $"amount_desc=@amountDesc, product_desc=@productDesc,download_link=@downloadLink  where product_id=@id";
            // array für die Parameter erstellen
            return await template.ExecuteAsync(@sqlcmd,
                new QueryParameter("@id", product.Id),
                new QueryParameter("@name", product.Name),
                new QueryParameter("@price", product.Price),
                new QueryParameter("@amountDesc", product.AmountDesc),
                new QueryParameter("@productDesc", product.ProductDesc),
                new QueryParameter("@downloadLink", product.DownloadLink)
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
            return await template.ExecuteAsync(@sqlcmd,
                   new QueryParameter("@id", id)) == 1;
        }
        return false;
    }

    public async Task<bool> StoreAsync(Product product, string table)
    {
        Product? p = await FindByIdAsync(product.Id, table);

        if (p is null)
        {
            string sqlcmd = $"insert into {table} (product_id,product_name, price, amount_desc, product_desc, download_link ) " +
           "values (@id,@name, @price,@amountDesc,@productDesc,@downloadLink) ";

            return await template.ExecuteAsync(@sqlcmd,
                new QueryParameter("@id", product.Id),
                new QueryParameter("@name", product.Name),
                new QueryParameter("@price", product.Price),
                new QueryParameter("@amountDesc", product.AmountDesc),
                new QueryParameter("@productDesc", product.ProductDesc),
                new QueryParameter("@downloadLink", product.DownloadLink)
                ) == 1;
        }
        return await UpdateAsync(product, table);
    }
}
