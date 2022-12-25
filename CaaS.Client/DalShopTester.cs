using CaaS.Dal.Interfaces;
using CaaS.Domain;
using System.Transactions;

namespace CaaS.Client;

internal class DalShopTester
{
    

    // null prüfen
    private readonly IBaseDao<Shop> shopDao;
    private readonly string table ="Shops";
    private readonly string id1="shop-1";
    private readonly string id2="shop-2";

    public DalShopTester(IBaseDao<Shop> shopDao)
    {
        this.shopDao = shopDao;
    }

    public async Task TestFindAllAsync()
    {
        (await shopDao.FindAllAsync(table))
            .ToList()
            .ForEach(p => Console.WriteLine($"{p.Id,9} | {p.Name,20} | {p.Address,5} |"));
    }

    public async Task TestFindByIdAsync()
    {
        Shop? shop1 = await shopDao.FindByIdAsync(id1,table);
        Console.WriteLine($"FindById {id1} -> {shop1?.ToString() ?? "<null>"}");

        Shop? shop2 = await shopDao.FindByIdAsync(id2,table);
        Console.WriteLine($"FindById {id2} -> {shop2?.ToString() ?? "<null>"}");
    }

    public async Task TestUpdateAsync()
    {
        Shop? shop = await shopDao.FindByIdAsync(id1,table);
        Console.WriteLine($"before update: Shop -> {shop?.ToString() ?? "<null>"}");
        if (shop is null)
        {
            return;
        }

        shop.Address= "addr-sh5";
        await shopDao.UpdateAsync(shop,table);
        Console.WriteLine($"after update: Shop -> {shop?.ToString() ?? "<null>"}");
        (await shopDao.FindAllAsync(table))
            .ToList()
            .ForEach(p => Console.WriteLine($"{p.Id,9} | {p.Name,20} | {p.Address,5} |"));


    }


    public async Task TestDeleteByIdAsync()
    {
        Shop? shop = await shopDao.FindByIdAsync(id2, table);
        Console.WriteLine($"before delete: Shop -> {shop?.ToString() ?? "<null>"}");
        if (shop is null)
        {
            return;
        }
        Console.WriteLine($"Deleting: ");
        await shopDao.DeleteByIdAsync(id1, table);
        await shopDao.DeleteByIdAsync(shop.Id, table);
        await shopDao.DeleteByIdAsync("shop-3", table);

        Console.WriteLine($"after delete: ");
        (await shopDao.FindAllAsync(table))
            .ToList()
            .ForEach(p => Console.WriteLine($"{p.Id,9} | {p.Name,20} | {p.Address,5} |"));
    }

    public async Task TestStoreAsync()
    {
        Shop? shop = new Shop("shop-1", "LoveRead", "books", "mandant-1", "addr-sh1","ak-s1");
        await shopDao.StoreAsync(shop, table);
        Console.WriteLine($"before update: Shop -> {shop?.ToString() ?? "<null>"}");
        shop = new Shop("shop-2", "DrugLinz", "Medicaments", "mandant-2", "addr-sh2","ak-s2");
        await shopDao.StoreAsync(shop, table);

        shop = new Shop("shop-3","ScienceBooks", "Books mostly about science", "mandant-3", "addr-sh3","ak-s3");        
        await shopDao.StoreAsync(shop, table);
        Console.WriteLine($"after update: ");
        (await shopDao.FindAllAsync(table))
           .ToList()
           .ForEach(p => Console.WriteLine($"{p.Id,9} | {p.Name,20} | {p.Address,5} |"));

    }

    public async Task TestTransactionsAsync()
    {
        Shop? shop1 = await shopDao.FindByIdAsync(id1,table);
        Shop? shop2 = await shopDao.FindByIdAsync(id2,table);
        if (shop1 is null || shop2 is null)
        {
            Console.WriteLine("Cannot perfom test because Shops with id 1 and 2 are required");
            return;
        }

        string oldAddress1 = shop1.Address;
        string oldAddress2 = shop2.Address;
        string newAddress1 = "addr-sh3";
        string newAddress2 = "addr-sh4";

        try
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                shop1.Address=((newAddress1 = (oldAddress1 )));
                shop2.Address=((newAddress2 = (oldAddress2 )));
                await shopDao.UpdateAsync(shop1,table);
                //throw new ArgumentException(); // comment this out to rollback transaction
                await shopDao.UpdateAsync(shop2,table);
                scope.Complete();
            }
        }
        catch
        {
        }

        shop1 = await shopDao.FindByIdAsync(id1,table);
        shop2 = await shopDao.FindByIdAsync(id2,table);
        if (shop1 is null || shop2 is null)
        {
            Console.WriteLine("Cannot perfom test because Shops with id 1 and 2 are required");
            return;
        }

        if (oldAddress1 == shop1.Address && oldAddress2 == shop2.Address)
            Console.WriteLine("Transaction was ROLLED BACK.");
        else if (newAddress1 == shop1.Address && newAddress2 == shop2.Address)
            Console.WriteLine("Transaction was COMMITTED.");
        else
            Console.WriteLine("No Transaction.");
    }
}
