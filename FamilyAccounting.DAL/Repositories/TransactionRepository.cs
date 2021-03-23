using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace FamilyAccounting.DAL.Repositories
{
    class TransactionRepository : ITransactionRepository
    {
        private readonly string connectionString;
        public TransactionRepository(DbConfig dbConfig)
        {
            connectionString = dbConfig.ConnectionString;
        }
        public Transaction MakeExpense(Transaction transaction)
        {
            string sqlExpression = "PR_Wallets_Update_MakeExpense";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlExpression, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@_id_wallet", transaction.SourceWallet.Id);
                command.Parameters.AddWithValue("@_amount", transaction.Amount);
                command.Parameters.AddWithValue("@_id_category", transaction.Category.Id);
                command.Parameters.AddWithValue("@_description", transaction.Description);
                SqlParameter output = new SqlParameter
                {
                    ParameterName = "@_success",
                    SqlDbType = SqlDbType.Int
                };
                output.Direction = ParameterDirection.Output;
                command.Parameters.Add(output);
                command.ExecuteNonQuery();
                int successStatus = (int)command.Parameters["@_success"].Value;
            }
            return transaction;
        }

        public Transaction Update(int id, Transaction transaction)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlExpression = $"EXEC PR_Actions_Update {id}, '{transaction.Category.Id}', '{transaction.Description}'";
                SqlConnection sql = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sqlExpression, sql);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                command.ExecuteNonQuery();
            }
            return transaction;
        }

    }
}
