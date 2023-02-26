using CaaS.Dal.Interfaces;
using CaaS.Domain;
using System.Transactions;

namespace CaaS.Client;

internal class DalProductTester
{
    

    // null prüfen
    private readonly IBaseDao<Product> ProductDao;
    private readonly string table ="Products";
    private readonly string id1="arz-1";
    private readonly string id2="arz-2";

    public DalProductTester(IBaseDao<Product> ProductDao)
    {
        this.ProductDao = ProductDao;
    }

    public async Task TestFindAllAsync()
    {
        (await ProductDao.FindAllAsync(table))
            .ToList()
            .ForEach(p => Console.WriteLine($"{p.Id,9} | {p.Name,80} | {p.Price,5} |"));
    }

    public async Task TestFindByIdAsync()
    {
        Product? product1 = await ProductDao.FindByIdAsync(id1,table);
        Console.WriteLine($"FindById{id1} -> {product1?.ToString() ?? "<null>"}");

        Product? product2 = await ProductDao.FindByIdAsync(id2,table);
        Console.WriteLine($"FindById{id2} -> {product2?.ToString() ?? "<null>"}");
    }

    public async Task TestUpdateAsync()
    {
        Product? product = await ProductDao.FindByIdAsync(id1,table);
        Console.WriteLine($"before updating: Product -> {product?.ToString() ?? "<null>"}");
        if (product is null)
        {
            return;
        }

        product.Price = 4.5;
        await ProductDao.UpdateAsync(product,table);

        product = await ProductDao.FindByIdAsync(id1,table);
        Console.WriteLine($"after updating:  Product -> {product?.ToString() ?? "<null>"}");
    }


    public async Task TestDeleteByIdAsync()
    {
        Product? product = await ProductDao.FindByIdAsync(id2, table);
        Console.WriteLine($"before deleting: Product -> {product?.ToString() ?? "<null>"}");
       
        
        await ProductDao.DeleteByIdAsync(product.Id, table);
        product = await ProductDao.FindByIdAsync(id2, table);
        Console.WriteLine($"after deleting:  Product -> {product?.ToString() ?? "<null>"}");
        
        
        
    }

    public async Task TestStoreAsync()
    {
        Product? product = await ProductDao.FindByIdAsync(id2, table);
        Console.WriteLine($"before storing: Product -> {product?.ToString() ?? "<null>"}");
        if (product == null)
        {
            product = new Product(id2, "(BAXTER) GLUCOSE 10% w/v ***", 11, "250 ml x 30 Viaflo Bags", "not yet", "download-link","sh1");
            await ProductDao.StoreAsync(product, table);
        }
        product = await ProductDao.FindByIdAsync(id2, table);
        Console.WriteLine($"after storing:  Product -> {product?.ToString() ?? "<null>"}");
    }

    public async Task TestTransactionsAsync()
    {
        Product? product1 = await ProductDao.FindByIdAsync(id1,table);
        Product? product2 = await ProductDao.FindByIdAsync(id2,table);
        if (product1 is null || product2 is null)
        {
            Console.WriteLine("Cannot perfom test because Products with id 1 and 2 are required");
            return;
        }

        double oldPrice1 = product1.Price*1.5;
        double oldPrice2 = product2.Price*1.8;
        double newPrice1 = product1.Price/2;
        double newPrice2 = product2.Price/2;

        try
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                product1.Price=((double)(newPrice1 = (double)(oldPrice1)));
                product2.Price=((double)(newPrice2 = (double)(oldPrice2)));
                await ProductDao.UpdateAsync(product1,table);
                //throw new ArgumentException(); // comment this out to rollback transaction
                await ProductDao.UpdateAsync(product2,table);
                scope.Complete();
            }
        }
        catch
        {
        }

        product1 = await ProductDao.FindByIdAsync(id1,table);
        product2 = await ProductDao.FindByIdAsync(id2,table);
        if (product1 is null || product2 is null)
        {
            Console.WriteLine("Cannot perfom test because Products with id 1 and 2 are required");
            return;
        }

        if (oldPrice1 == product1.Price && oldPrice2 == product2.Price)
            Console.WriteLine("Transaction was ROLLED BACK.");
        else if (newPrice1 == product1.Price && newPrice2 == product2.Price)
            Console.WriteLine("Transaction was COMMITTED.");
        else
            Console.WriteLine("No Transaction.");
    }
}
