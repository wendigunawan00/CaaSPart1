  using Dal.Common;
//using Microsoft.Data.SqlClient;
using CaaS.Dal.Interface;
using CaaS.Domain;
using static CaaS.Dal.Ado.AdoMapDao;



namespace CaaS.Dal.Ado;

public class AdoPersonDao : IPersonDao
{
    private readonly AdoTemplate template;
    private readonly AdoAddressDao addressDao;

    private bool IsAddressByIdSync(string addressId, string addresstable)
    {
        return template.QuerySingleSync($"select * from {addresstable} where address_id=@id",
           MapRowToAddress,
           new QueryParameter("@id", addressId)) is not null;
    }

    public AdoPersonDao(IConnectionFactory connectionFactory)
    {
        this.template = new AdoTemplate(connectionFactory);
        this.addressDao = new AdoAddressDao(connectionFactory); 
    }

    public async Task<IEnumerable<Person>> FindAllAsync(string table)
    {
        return await template.QueryAsync($"select * from {table}", MapRowToPerson);
    }

    public async Task<Person?> FindByIdAsync(string id,string table)
    {
        return await template.QuerySingleAsync($"select * from {table} where person_id=@id",
            MapRowToPerson,
            new QueryParameter("@id", id));
    }

    public async Task<bool> UpdateAsync(Person person,string table)
    {
        Person? p = await FindByIdAsync(person.Id, table);
        if (p is not null)
        {
            string sqlcmd = $"update {table} set first_name=@fn, last_name=@ln, dob=@dob where person_id=@id";
            // array für die Parameter erstellen
            return await template.ExecuteAsync(@sqlcmd,
                new QueryParameter("@id", person.Id),
                new QueryParameter("@fn", person.FirstName),
                new QueryParameter("@ln", person.LastName),
                new QueryParameter("@dob", person.DateOfBirth)) == 1;
        }
        return false;
    }

    public async Task<bool> DeleteByIdAsync(string id, string table)
    {
        Person? p = await FindByIdAsync(id,table);
        if (p is not null)        
        {
            string sqlcmd = $"delete from {table} where person_id=@id";
            return await template.ExecuteAsync(@sqlcmd,
                   new QueryParameter("@id", id)) == 1;
        }
        return false;
    }

    public async Task<bool>StoreAsync(Person person, string table)
    {
        Person? p = await FindByIdAsync(person.Id, table);
        if (p is null)
        {
            string sqlcmd = $"insert into {table} ( person_id,first_name, last_name, dob, email, address, status ) " +
            "values (@id,@fn, @ln,@dob,@email,@address,@status) ";
            //string addresstable = "Addresses" + table;
            //Address? addr = await addressDao.FindByIdAsync(person.AddressId, addresstable);
            //await addressDao.StoreAsync(addr, addresstable);


            return await template.ExecuteAsync(@sqlcmd,
                new QueryParameter("@id", person.Id),
                new QueryParameter("@fn", person.FirstName),
                new QueryParameter("@ln", person.LastName),
                new QueryParameter("@dob", person.DateOfBirth),
                new QueryParameter("@email", person.Email),
                new QueryParameter("@address", person.AddressId),
                new QueryParameter("@status", person.Status)) == 1;
        }
        return await UpdateAsync(person, table); 
    }
        
}
