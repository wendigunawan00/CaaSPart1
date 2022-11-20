
using Dal.Common;
using Microsoft.Extensions.Configuration;
using CaaS.Client;
using CaaS.Dal.Ado;

static void PrintTitle(string text = "", int length = 60, char ch = '-')
{
  int preLen = (length - (text.Length + 2)) / 2;
  int postLen = length - (preLen + text.Length + 2);
  Console.WriteLine($"{new String(ch, preLen)} {text} {new String(ch, postLen)}");
}



IConfiguration configuration = ConfigurationUtil.GetConfiguration();
IConnectionFactory connectionFactory =
    DefaultConnectionFactory.FromConfiguration(configuration, "CaaSDbConnection");
var tester2 = new DalPersonTester(new AdoPersonDao(connectionFactory));
PrintTitle("AdoPersonDao.FindAll", 50);
await tester2.TestFindAllAsync();

PrintTitle("AdoPersonDao.FindById", 50);
await tester2.TestFindByIdAsync();

PrintTitle("AdoPersonDao.Update", 50);
await tester2.TestUpdateAsync();

PrintTitle("AdoPersonDao.Delete", 50);
await tester2.TestDeleteByIdAsync();

PrintTitle("AdoPersonDao.Store", 50);
await tester2.TestStoreAsync();
PrintTitle("Transactions", 50);
await tester2.TestFindAllAsync();
await tester2.TestTransactionsAsync();
await tester2.TestFindAllAsync();

//====================================================================================
var tester3 = new DalProductTester(new AdoProductDao(connectionFactory));
PrintTitle("AdoProductDao.FindAll", 50);
await tester3.TestFindAllAsync();

PrintTitle("AdoProductDao.FindById", 50);
await tester3.TestFindByIdAsync();

PrintTitle("AdoProductDao.Update", 50);
await tester3.TestUpdateAsync();

PrintTitle("AdoProductDao.Delete", 50);
await tester3.TestDeleteByIdAsync();

PrintTitle("AdoProductDao.Store", 50);
await tester3.TestStoreAsync();
PrintTitle("Transactions", 50);
await tester3.TestFindAllAsync();
await tester3.TestTransactionsAsync();
await tester3.TestFindAllAsync();


//====================================================================================
var tester4 = new DalShopTester(new AdoShopDao(connectionFactory));
PrintTitle("AdoShopDao.FindAll", 50);
await tester4.TestFindAllAsync();

PrintTitle("AdoShopDao.FindById", 50);
await tester4.TestFindByIdAsync();

PrintTitle("AdoShopDao.Update", 50);
await tester4.TestUpdateAsync();

PrintTitle("AdoShopDao.Delete", 50);
await tester4.TestDeleteByIdAsync();

PrintTitle("AdoShopDao.Store", 50);
await tester4.TestStoreAsync();
PrintTitle("Transactions", 50);
await tester4.TestFindAllAsync();
await tester4.TestTransactionsAsync();
await tester4.TestFindAllAsync();