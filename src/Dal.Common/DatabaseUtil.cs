using System;
using System.Data.Common;

namespace Dal.Common
{
    public static class DatabaseUtil
    {
        public static DbProviderFactory GetDbProviderFactory(string providerName)
        {
            switch (providerName)
            {
                case "Microsoft.Data.SqlClient":
                    return Microsoft.Data.SqlClient.SqlClientFactory.Instance;
                default:
                    throw new ArgumentException("Invalid provider name \"{providerName}\"");
            }
        }
    }
}