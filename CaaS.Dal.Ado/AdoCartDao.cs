  using Dal.Common;
using CaaS.Dal.Interfaces;
using CaaS.Domain;
using static CaaS.Dal.Ado.AdoMapDao;

namespace CaaS.Dal.Ado;

public class AdoCartDao : AdoGenericDao<Cart>,IBaseDao<Cart>
{
    private readonly IBaseDao<Person> personDao;


    public AdoCartDao(IConnectionFactory connectionFactory) : base(connectionFactory) => this.personDao = new AdoPersonDao(connectionFactory);
       
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
    
    public async Task<IEnumerable<Cart>> FindTByStatus(string status, string table)
    {
        string sqlcmd = $"select * from {table} where status = @status";
        return await base.template.QueryAsync(@sqlcmd,MapRowToCart,
                   new QueryParameter("@status", status));
    } 
    
    public async Task<Cart> CreateCart(string custId, string table)
    {
        string sqlcmd = $"select count(*) from {table} where cust_id = @custId";
        int lastCartId = await base.template.ExecuteCountAsync(@sqlcmd,new QueryParameter("@custId", custId));
        var newCart = new Cart($"cart{lastCartId +1}-{custId}",custId,"open");
        var xtrue = await StoreAsync(newCart,table);
        return newCart;
    } 
    
    public async Task<IEnumerable<Cart>> FindTByStatusAndId(string status, string id, string table)
    {
        string sqlcmd = $"select * from {table} where status = @status And cust_id=@cust_id";
        return await base.template.QueryAsync(@sqlcmd,MapRowToCart,
                   new QueryParameter("@status", status),
                   new QueryParameter("@cust_id", id)
                   );
    }

    public async Task<bool> StoreAsync(Cart cart, string table)
    {
        Cart? c = await FindByIdAsync(cart.Id, table);
        
        Person? cust = await personDao.FindByIdAsync(cart.CustId, "Customers");
        

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
