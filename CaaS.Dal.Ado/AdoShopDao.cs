  using Dal.Common;
//using Microsoft.Data.SqlClient;
using CaaS.Dal.Interfaces;
using CaaS.Domain;
using static CaaS.Dal.Ado.AdoMapDao;


namespace CaaS.Dal.Ado;

public class AdoShopDao : AdoGenericDao<Shop>,IBaseDao<Shop>
{
    public AdoShopDao(IConnectionFactory connectionFactory): base(connectionFactory)
    {
        
    }

    public async Task<bool> UpdateAsync(Shop shop,string table)
    {
        Shop? s = await FindByIdAsync(shop.Id, table);
        if (s is not  null)
        {
            string sqlcmd = $"update {table} set shop_name=@name, field_descriptions=@fieldDesc, address=@address where shop_id=@id";
            // array für die Parameter erstellen
            return await base.template.ExecuteAsync(@sqlcmd,
                new QueryParameter("@id", shop.Id),
                new QueryParameter("@name", shop.Name),
                new QueryParameter("@fieldDesc", shop.FieldDesc),
                new QueryParameter("@address", shop.Address)
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
            return await base.template.ExecuteAsync(@sqlcmd,
                   new QueryParameter("@id", id)) == 1;
        }
        return false;
    }

    public async Task<bool> StoreAsync(Shop shop, string table)
    {
        Shop? s = await FindByIdAsync(shop.Id, table);
        if (s is null)
        {
             string sqlcmd = $"insert into {table} ( shop_id,shop_name, field_descriptions, address) " +
            "values (@id,@name, @fieldDesc,@address) ";            
            

            return await base.template.ExecuteAsync(@sqlcmd,
            new QueryParameter("@id", shop.Id),
            new QueryParameter("@name", shop.Name),
            new QueryParameter("@fieldDesc", shop.FieldDesc),
            new QueryParameter("@address", shop.Address)
            ) == 1;

        }
        return await UpdateAsync(shop, table);
    }

    
}
