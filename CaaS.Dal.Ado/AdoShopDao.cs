  using Dal.Common;
//using Microsoft.Data.SqlClient;
using CaaS.Dal.Interface;
using CaaS.Domain;
using static CaaS.Dal.Ado.AdoMapDao;


namespace CaaS.Dal.Ado;

public class AdoShopDao : IShopDao
{
    private readonly AdoTemplate template;
    

    public AdoShopDao(IConnectionFactory connectionFactory)
    {
        this.template = new AdoTemplate(connectionFactory);
    }

    public async Task<IEnumerable<Shop>> FindAllAsync(string table)
    {
        return await template.QueryAsync($"select * from {table}", MapRowToShop);
    }

    public async Task<Shop?> FindByIdAsync(string id, string table)
    {
        return await template.QuerySingleAsync($"select * from {table} where shop_id=@id",
            MapRowToShop,
            new QueryParameter("@id", id));        

    }      

    public async Task<bool> UpdateAsync(Shop shop,string table)
    {
        Shop? s = await FindByIdAsync(shop.Id, table);
        if (s is not  null)
        {
            string sqlcmd = $"update {table} set shop_name=@name, field_descriptions=@fieldDesc, mandant_id=@mandantId , app_key=@appKey where shop_id=@id";
            // array für die Parameter erstellen
            return await template.ExecuteAsync(@sqlcmd,
                new QueryParameter("@id", shop.Id),
                new QueryParameter("@name", shop.Name),
                new QueryParameter("@fieldDesc", shop.FieldDesc),
                new QueryParameter("@mandantId", shop.MandantId),
                new QueryParameter("@appKey", shop.AppKey)
                ) == 1;
        }
        return false;
    }

    public async Task<bool> DeleteByIdAsync(string id, string table)
    {
        Shop? s = await FindByIdAsync(id, table);
        if (s is not null)
        {
            string sqlcmd = $"delete from {table} where shop_id=@id";
            return await template.ExecuteAsync(@sqlcmd,
                   new QueryParameter("@id", id)) == 1;
        }
        return false;
    }

    public async Task<bool> StoreAsync(Shop shop, string table)
    {
        Shop? s = await FindByIdAsync(shop.Id, table);
        if (s is null)
        {
             string sqlcmd = $"insert into {table} ( shop_id,shop_name, field_descriptions, mandant_id, address,app_key) " +
            "values (@id,@name, @fieldDesc,@mandantId,@address,@appKey) ";            
            

            return await template.ExecuteAsync(@sqlcmd,
            new QueryParameter("@id", shop.Id),
            new QueryParameter("@name", shop.Name),
            new QueryParameter("@fieldDesc", shop.FieldDesc),
            new QueryParameter("@mandantId", shop.MandantId),
            new QueryParameter("@address", shop.Address),
            new QueryParameter("@appKey", shop.AppKey)
            ) == 1;

        }
        return await UpdateAsync(shop, table);
    }
}
