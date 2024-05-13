using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenDev.Persistence.Constants
{
    public static class ConnectionString
    {
        public static string connectionString { get; set; } = "Data Source=127.0.0.1\\ZenDev_DB,1433;database=ZenDev.Dev;User ID=SA;Password=ThreeAWithTAnd1J;TrustServerCertificate=true;";
    }
}
