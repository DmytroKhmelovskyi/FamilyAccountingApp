using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FamilyAccounting.DAL.Repositories
{
    class WalletsRepository : IWalletsRepository
    {
        private readonly string connectionString;
        public WalletsRepository(DbConfig dbConfig)
        {
            connectionString = dbConfig.ConnectionString;
        }
        public IEnumerable<Wallet> Get()
        {
            string sqlProcedure = "PR_Wallets_Read";
            List<Wallet> table = new List<Wallet>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlProcedure, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Wallet wallet = new Wallet
                        {
                            Id = reader.GetInt32(0),
                            Description = reader.GetString(4),
                            IsCash = reader.GetBoolean(5),
                            Balance = reader.GetDecimal(9)
                        };
                        table.Add(wallet);
                    }
                }
                reader.Close();
            }
            return table;
        }
    }
}
