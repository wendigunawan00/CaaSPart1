using Dal.Common;
using CaaS.Dal.Interfaces;
using CaaS.Domain;
using static CaaS.Dal.Ado.AdoMapDao;

namespace CaaS.Dal.Ado;

public class AdoAppKeyDao : AdoBaseDao,IBaseDao<AppKey>
{

    public AdoAppKeyDao(IConnectionFactory connectionFactory): base(connectionFactory)
    {        
    }

    public async Task<IEnumerable<AppKey>> FindAllAsync(string table)
    {
        return await template.QueryAsync($"select * from {table}", MapRowToAppKey);
    }

    public async Task<AppKey?> FindByIdAsync(string id, string table)
    {
        return await template.QuerySingleAsync($"select * from {table} where app_key=@id",
            MapRowToAppKey,
            new QueryParameter("@id", id));
    }

    public async Task<bool> UpdateAsync(AppKey appKey, string table)
    {
        AppKey? apk = await FindByIdAsync(appKey.Id, table);
        if (apk is not null)
        {
            string sqlcmd = $"update {table} set app_key_name=@appKeyName, app_key_password =@appKeyPassword" +
                $" where app_key=@id";
            // array für die Parameter erstellen
            return await template.ExecuteAsync(@sqlcmd,
                new QueryParameter("@id", appKey.Id),
                new QueryParameter("@appKeyName", appKey.AppKeyName), 
                new QueryParameter("@appKeyPassword", appKey.AppKeyPassword)
                ) == 1;
        }
        return false;
    }

    public async Task<bool> DeleteByIdAsync(string id, string table)
    {
        AppKey? apk = await FindByIdAsync(id, table);

        if (apk is not null)
        {
            string sqlcmd = $"delete from {table} where app_key=@id";
            return await template.ExecuteAsync(@sqlcmd,
                   new QueryParameter("@id", id)) == 1;
        }
        return false;
    }

    public async Task<bool> StoreAsync(AppKey appKey, string table)
    {
        AppKey? apk = await FindByIdAsync(appKey.Id, table);
        if (apk is null)
        {
            
            string sqlcmd = $"insert into {table} (app_key,app_key_name,app_key_password) " +
                $"values (@id,@appKeyName,@appKeyPassword)";
            return await template.ExecuteAsync(@sqlcmd,
                new QueryParameter("@id", appKey.Id),
                new QueryParameter("@appKeyName", appKey.AppKeyName),
                new QueryParameter("@appKeyPassword", appKey.AppKeyPassword)
                ) == 1;
            
        }
        return await UpdateAsync(appKey, table);
    }
}
