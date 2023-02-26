  using Dal.Common;
//using Microsoft.Data.SqlClient;
using CaaS.Dal.Interfaces;
using CaaS.Domain;
using static CaaS.Dal.Ado.AdoMapDao;


namespace CaaS.Dal.Ado;

public class AdoPersonDao : AdoGenericDao<Person>, IBaseDao<Person>
{
   // private readonly AdoTemplate template;

    public AdoPersonDao(IConnectionFactory connectionFactory) : base(connectionFactory)
    {     
       // not recommended
       //this.template = new AdoTemplate(connectionFactory);
    }

    public async Task<bool> UpdateAsync(Person person,string table)
    {
        Person? p = await FindByIdAsync(person.Id, table);
        if (p is not null)
        {
            string sqlcmd = $"update {table} set first_name=@fn, last_name=@ln, dob=@dob, email=@email, status=@status,password=@password where person_id=@id";
            // array für die Parameter erstellen
            return await base.template.ExecuteAsync(@sqlcmd,
                new QueryParameter("@id", person.Id),
                new QueryParameter("@fn", person.FirstName),
                new QueryParameter("@ln", person.LastName),
                new QueryParameter("@dob", person.DateOfBirth),
                new QueryParameter("@email", person.Email),
                new QueryParameter("@status", person.Status),
                new QueryParameter("@password", person.Password)
                ) == 1;
        }
        return false;
    }

    public async Task<bool> DeleteByIdAsync(string id, string table)
    {
        Person? p = await FindByIdAsync(id,table);
        if (p is not null)        
        {
            string sqlcmd = $"delete from {table} where person_id=@id";
            return await base.template.ExecuteAsync(@sqlcmd,
                   new QueryParameter("@id", id)) == 1;
        }
        return false;
    }

    public async Task<bool>StoreAsync(Person person, string table)
    {
        Person? p = await FindByIdAsync(person.Id, table);
        if (p is null)
        {
            string sqlcmd = $"insert into {table} ( person_id,first_name, last_name, dob, email, address, status, password ) " +
            "values (@id,@fn, @ln,@dob,@email,@address,@status,@password) ";
            //string addresstable = "Addresses" + table;
            //Address? addr = await addressDao.FindByIdAsync(person.AddressId, addresstable);
            //await addressDao.StoreAsync(addr, addresstable);


            return await base.template.ExecuteAsync(@sqlcmd,
                new QueryParameter("@id", person.Id),
                new QueryParameter("@fn", person.FirstName),
                new QueryParameter("@ln", person.LastName),
                new QueryParameter("@dob", person.DateOfBirth),
                new QueryParameter("@email", person.Email),
                new QueryParameter("@address", person.AddressId),
                new QueryParameter("@status", person.Status),
                new QueryParameter("@password", person.Password)) == 1;
        }
        return await UpdateAsync(person, table); 
    }

    public async Task<IEnumerable<Person?>> FindTByX(string email, string table)
    {
        string sqlcmd = $"select * from {table} where email= @email";
        return await template.QueryAsync(@sqlcmd,MapRowToPerson, new QueryParameter("@email", email));      
    }

}
