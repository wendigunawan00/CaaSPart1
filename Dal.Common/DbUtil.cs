namespace Dal.Common;

using System.Data.Common;

public static class DbUtil
{
  public static void RegisterAdoProviders()
  {
    // Use new Implementation of MS SQL Provider
    DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", Microsoft.Data.SqlClient.SqlClientFactory.Instance);
    // DbProviderFactories.RegisterFactory("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);
    DbProviderFactories.RegisterFactory("MySql.Data.MySqlClient", MySql.Data.MySqlClient.MySqlClientFactory.Instance);
  }

  public static T ConvertFromDBVal<T>(object obj)
  {
    if (obj == null || obj == DBNull.Value)
    {
        return default(T); // returns the default value for the type
    }
    else
    {
        return (T)obj;
    }
  }

}
