using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAccounting.DAL.Connection
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string connection = ConfigurationManager.ConnectionStrings["TestDatabase"].ConnectionString;

        public SqlConnection CreateSqlConnection()
        {
            SqlConnection sqlConnection = new SqlConnection(connection);
            return sqlConnection;
        }
    }
}
