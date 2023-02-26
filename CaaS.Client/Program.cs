
using Dal.Common;
using Microsoft.Extensions.Configuration;
using CaaS.Client;
using CaaS.Dal.Ado;
using MySqlX.XDevAPI.Relational;
using CaaS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;

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


//Console.WriteLine("Testing Create");
//string Sql = $"CREATE TABLE Customers(person_id varchar2(50),first_name,last_name char(50),Address char(50),City char(50),Country char(25),Birth_Date datetime);";
//string createCustomerTbl = $"CREATE TABLE Customers2("
//   + "person_id  VARCHAR(40) NOT NULL,first_name VARCHAR (40) NOT NULL, last_name VARCHAR(40) NOT NULL,dob DATETIME NOT NULL,"
//   + "email VARCHAR (60) NOT NULL, address VARCHAR (40) NOT NULL, status VARCHAR(16) NULL);";

//string datetime = DateTime.Now.ToString("yyyyMMddHHmmss");
//string LogFolder = @"C:\Users\Administrator\Documents\Semester5\SWK\CaaSV2\Log";
//Console.WriteLine("Blabla");
//try
//{

//    //Declare Variables and provide values
//    string SourceFolderPath = @"C:\Users\Administrator\Documents\Semester5\SWK\CaaSV2\EntityAlsExcelfürSSMS";
//    string FileExtension = ".xlsx";
//    string FileDelimiter = " ";
//    string ArchiveFolder = @"C:\Users\Administrator\Documents\Semester5\SWK\CaaSV2\Archive";
//    string ColumnsDataType = "NVARCHAR(4000)";
//    string SchemaName = "dbo";


//    //Get files from folder
//    string[] fileEntries = Directory.GetFiles(SourceFolderPath, "*" + FileExtension);
//    foreach (string fileName in fileEntries)
//    {

//        //Create Connection to Sql Server in which you would like to create tables and load data
//        SqlConnection Connection = new SqlConnection();
//        Connection.ConnectionString = "Data Source = .\\SQLEXPRESS01; Initial Catalog =DbCaaS00Testing2; "
//           + "Integrated Security=True;Trusted_Connection=True";

//        //Writing Data of File Into Table
//        string TableName = "";
//        int counter = 0;
//        string line;
//        string ColumnList = "";

//        //StreamReader SourceFile =  new StreamReader(new FileStream(fileName, FileMode.Open), Encoding.UTF8);
//        StreamReader SourceFile =  new StreamReader(fileName);

//        Connection.Open();
//        while ((line = SourceFile.ReadLine()) != null)
//        {
//            if (counter == 0)
//            {

//                //Read the header and prepare Create Table Statement
//                ColumnList = "[" + line.Replace(FileDelimiter, "],[") + "]";
//                TableName = (((fileName.Replace(SourceFolderPath, "")).Replace(FileExtension, "")).Replace("\\", ""));
//                string CreateTableStatement = "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[" + SchemaName + "].";
//                CreateTableStatement += "[" + TableName + "]')";
//                CreateTableStatement += " AND type in (N'U'))DROP TABLE [" + SchemaName + "].";
//                CreateTableStatement += "[" + TableName + "]  Create Table " + SchemaName + ".[" + TableName + "]";
//                CreateTableStatement += "([" + line.Replace(FileDelimiter, "] " + ColumnsDataType + ",[") + "] " + ColumnsDataType + ")";
//                SqlCommand CreateTableCmd = new SqlCommand(CreateTableStatement, Connection);
//                CreateTableCmd.ExecuteNonQuery();

//            }
//            else
//            {

//                //Prepare Insert Statement and execute to insert data
//                string query = "Insert into " + SchemaName + ".[" + TableName + "] (" + ColumnList + ") ";
//                //query += "VALUES('" + line.Replace(FileDelimiter, "','") + "')";
//                query += "VALUES('" + line.Replace("'", "''").Replace(FileDelimiter, "','") + "')";


//                SqlCommand SqlCmd = new SqlCommand(query, Connection);
//                SqlCmd.ExecuteNonQuery();
//            }

//            counter++;
//        }

//        SourceFile.Close();
//        Connection.Close();
//        //move the file to archive folder after adding datetime to it
//        File.Move(fileName, ArchiveFolder + "\\" + (fileName.Replace(SourceFolderPath, "")).Replace(FileExtension, "") + "_" + datetime + FileExtension);

//    }
//}
//catch (Exception exception)
//{
//    // Create Log File for Errors
//    using (StreamWriter sw = File.CreateText(LogFolder
//        + "\\" + "ErrorLog_" + datetime + ".log"))
//    {
//        sw.WriteLine(exception.ToString());

//    }

//}