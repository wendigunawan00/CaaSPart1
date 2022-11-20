using Dal.Common;
using CaaS.Domain;
using CaaS.Dal.Interface;
using static CaaS.Dal.Ado.AdoMapDao;
using System;
using Org.BouncyCastle.Asn1.X509;
using System.Text.RegularExpressions;

namespace CaaS.Dal.Ado;

public class AdoAddressDao : IAddressDao
{
    private readonly AdoTemplate template;


    public AdoAddressDao(IConnectionFactory connectionFactory)
    {
        this.template = new AdoTemplate(connectionFactory);

    }

    public async Task<IEnumerable<Address>> FindAllAsync(string table)
    {
        return await template.QueryAsync($"select * from {table}", MapRowToAddress);
    }

    public async Task<Address?> FindByIdAsync(string id, string table)
    {
        return await template.QuerySingleAsync($"select * from {table} where address_id=@id",
            MapRowToAddress,
            new QueryParameter("@id", id));
    }

    public async Task<bool> UpdateAsync(Address address, string table)
    {
        Address? addr = await FindByIdAsync(address.Id, table);

        if (addr is not null)
        {
            string sqlcmd = $"update {table} set street=@street, floor=@floor, postal_code=@postal_code, city=@city, province=@province,country=@country where address_id=@id";
            // array für die Parameter erstellen
            return await template.ExecuteAsync(@sqlcmd,
                new QueryParameter("@id", address.Id),
                new QueryParameter("@street", address.Street),
                new QueryParameter("@floor", address.Floor),
                new QueryParameter("@postal_code", address.PostalCode),
                new QueryParameter("@city", address.City),
                new QueryParameter("@province", address.Province),
                new QueryParameter("@country", address.Country)) == 1;
        }
        return false;
    }

    public async Task<bool> DeleteByIdAsync(string id, string table)
    {

        Address? addr = await FindByIdAsync(id, table);
        if (addr is not null)
        {
            string sqlcmd = $"delete from {table} where address_id=@id";
            return await template.ExecuteAsync(@sqlcmd,
                   new QueryParameter("@id", id)) == 1;
        }
        return false;
    }

    public async Task<bool> StoreAsync(Address address, string table)
    {
        Address? addr = await FindByIdAsync(address.Id, table);

        if (addr is null)
        {
            string sqlcmd = $"insert into {table} ( address_id,street, floor, postal_code, city, province, country ) " +
            "values (@id,@street, @floor,@postal_code,@city,@province,@country) ";

            return await template.ExecuteAsync(@sqlcmd,
                new QueryParameter("@id", address.Id),
                new QueryParameter("@street", address.Street),
                new QueryParameter("@floor", address.Floor),
                new QueryParameter("@postal_code", address.PostalCode),
                new QueryParameter("@city", address.City),
                new QueryParameter("@province", address.Province),
                new QueryParameter("@country",address.Country)) == 1;
        }
        return await UpdateAsync(address, table);
    }
}
