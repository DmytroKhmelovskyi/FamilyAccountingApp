using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FamilyAccounting.DAL.Repositories
{
    class WalletRepository : IWalletRepository
    {
        private readonly string connectionString;
        public WalletRepository(DbConfig dbConfig)
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
                            Id = reader.GetInt32("id"),
                            Description = reader.GetString("description"),
                            IsActive = reader.GetBoolean("inactive"),
                            IsCash = reader.GetBoolean("is_cash"),
                            Balance = reader.GetDecimal("positive_bal"),
                            Income = reader.GetDecimal("total_income"),
                            Expense = reader.GetDecimal("total_expence")
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
