  using Dal.Common;
//using Microsoft.Data.SqlClient;
using CaaS.Dal.Interfaces;
using static CaaS.Dal.Ado.AdoMapDao;
using CaaS.Domain;

namespace CaaS.Dal.Ado;

public class AdoGenericDao<T> : IGenericDao<T>
{
    protected readonly AdoTemplate template;

    
    Dictionary<Type, Delegate> domainMapperDict = new Dictionary<Type, Delegate>{
    { typeof(Cart), (RowMapper<Cart>) (row => MapRowToCart(row))},
    { typeof(Product), (RowMapper<Product>) (row => MapRowToProduct(row)) },
    { typeof(Person), (RowMapper<Person>) (row => MapRowToPerson(row)) },
    { typeof(CartDetails), (RowMapper<CartDetails>) (row => MapRowToCartDetails(row)) },
    { typeof(Order), (RowMapper<Order>) (row => MapRowToOrder(row)) },
    { typeof(OrderDetails), (RowMapper<OrderDetails>) (row => MapRowToOrderDetails(row)) },
    { typeof(Shop), (RowMapper<Shop>) (row => MapRowToShop(row)) },
    { typeof(Address), (RowMapper<Address>) (row => MapRowToAddress(row)) },
    { typeof(AppKey), (RowMapper<AppKey>) (row => MapRowToAppKey(row)) }
    };
    
    Dictionary<Type, string> domainIdDict = new Dictionary<Type,string>{
    { typeof(Cart), "cart_id" },
    { typeof(Product), "product_id" },
    { typeof(Person), "person_id" },
    { typeof(CartDetails), "cart_details_id" },
    { typeof(Order), "order_id" },
    { typeof(OrderDetails), "order_details_id"},
    { typeof(Shop), "shop_id" },
    { typeof(Address), "address_id"},
    { typeof(AppKey), "app_key" }
    };

    public AdoGenericDao(IConnectionFactory connectionFactory)
    {
        template = Util.createAdoTemplate(connectionFactory) ?? throw new ArgumentNullException(nameof(connectionFactory));
        // not recommended
        //this.template = new AdoTemplate(connectionFactory);
    }

    public async Task<IEnumerable<T>> FindAllAsync(string table)
    {
        Delegate domainMapper;
        if (domainMapperDict.TryGetValue(typeof(T), out domainMapper))
        {
            var MapRowToT = (RowMapper<T>)domainMapper;
            return await template.QueryAsync(@$"select * from {table}", MapRowToT);            
        }
        return Enumerable.Empty<T>();
    }
    
    public async Task<long> CountAllElements(string table)
    {          
        return await template.ExecuteCountAsync(@$"select count(*) from {table}");       
    }

    public async Task<T?> FindByIdAsync(string id, string table)
    {
        Delegate domainMapper;
        string domainId;
        if (domainMapperDict.TryGetValue(typeof(T), out domainMapper)&&
            domainIdDict.TryGetValue(typeof(T), out domainId))
        {
            var MapRowToT = (RowMapper<T>)domainMapper;
            
            return await template.QuerySingleAsync(@$"select * from {table} where {domainId}=@id",
            MapRowToT,
            new QueryParameter("@id", id));
        }
        return default;
    }
    
    public async Task<T?> FindLastAsync(string table)
    {
        Delegate domainMapper;
        string domainId;
        if (domainMapperDict.TryGetValue(typeof(T), out domainMapper) &&
            domainIdDict.TryGetValue(typeof(T), out domainId))
        {
            var MapRowToT = (RowMapper<T>)domainMapper;
            return await template.QuerySingleAsync(@$"select * from {table} where {domainId}=(SELECT max({domainId}) FROM {table})"
                , MapRowToT);
        }
        return default;
    }

    public async Task<IEnumerable<T?>> FindTByX(string shopId, string table)
    {
        Delegate domainMapper;
        if (domainMapperDict.TryGetValue(typeof(T), out domainMapper))
        {
            var MapRowToT = (RowMapper<T>)domainMapper;
            return await template.QueryAsync<T>(@$"select * from {table} where shop_id= @shopId", 
                MapRowToT,new QueryParameter("@shopId",shopId));
        }
        return Enumerable.Empty<T>();  
    }
}
