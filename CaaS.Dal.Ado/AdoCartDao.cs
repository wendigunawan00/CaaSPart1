  using Dal.Common;
using CaaS.Dal.Interfaces;
using CaaS.Domain;
using static CaaS.Dal.Ado.AdoMapDao;
using System.Text.RegularExpressions;

namespace CaaS.Dal.Ado;

public class AdoCartDao : AdoBaseDao,IBaseDao<Cart>
{
    private readonly IBaseDao<Person> personDao;


    public AdoCartDao(IConnectionFactory connectionFactory) : base(connectionFactory) => this.personDao = new AdoPersonDao(connectionFactory);

    public async Task<IEnumerable<Cart>> FindAllAsync(string table)
    {
        return await base.template.QueryAsync($"select * from {table}", MapRowToCart);
    }

    public async Task<Cart?> FindByIdAsync(string id,string table)
    {
        return await base.template.QuerySingleAsync($"select * from {table} where cart_id=@id",
            MapRowToCart,
            new QueryParameter("@id", id));
    }
   
    public async Task<bool> UpdateAsync(Cart cart,string table)
    {
        Cart? c = await FindByIdAsync(cart.Id, table);
        if (c is not null)
        {
            string sqlcmd = $"update {table} set cust_id=@custId, status=@status where cart_id=@id";
            // array für die Parameter erstellen
            return await base.template.ExecuteAsync(@sqlcmd,
                new QueryParameter("@id", cart.Id),
                new QueryParameter("@custId", cart.CustId),
                new QueryParameter("@status", cart.Status)
                ) == 1;
        }
        return false;
    }

    public async Task<bool> DeleteByIdAsync(string id, string table)
    {
        Cart? c = await FindByIdAsync(id, table);
        if (c is not null)
        {
            string sqlcmd = $"delete from {table} where cart_id=@id";
            return await base.template.ExecuteAsync(@sqlcmd,
                   new QueryParameter("@id", id)) == 1;
        }
        return false;
    }

    public async Task<bool> StoreAsync(Cart cart, string table)
    {
        Cart? c = await FindByIdAsync(cart.Id, table);
        string[] substrings = Regex.Split(table, "Carts");
        string customershoptable = "Customers" + substrings[substrings.Length - 1];
        Person? cust = await personDao.FindByIdAsync(cart.CustId, customershoptable);
        //if (cust is not null)
        //    await personDao.StoreAsync(cust, customershoptable);

        if (c is null )
        {
            if (cust is not null)
            {
                string sqlcmd = $"insert into {table} (cart_id,cust_id, status) " +
               "values (@id,@custId, @status) ";
                return await base.template.ExecuteAsync(@sqlcmd,
                    new QueryParameter("@id", cart.Id),
                    new QueryParameter("@custId", cart.CustId),
                    new QueryParameter("@status", cart.Status)
                    ) == 1;
            }
            else
                return false;
        }
        return await UpdateAsync(cart, table);
    }
}
