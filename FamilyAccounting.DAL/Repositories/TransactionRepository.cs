using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace FamilyAccounting.DAL.Repositories
{
    public class TransactionRepository : ITransactionRepository
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
                command.Parameters.AddWithValue("@_id_wallet", transaction.SourceWalletId);
                command.Parameters.AddWithValue("@_amount", transaction.Amount);
                command.Parameters.AddWithValue("@_id_category", transaction.CategoryId);
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

        public Transaction MakeIncome(Transaction transaction)
        {
            string sqlExpression = "PR_Wallets_Update_MakeIncome";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlExpression, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@_id_wallet", transaction.TargetWalletId);
                command.Parameters.AddWithValue("@_amount", transaction.Amount);
                command.Parameters.AddWithValue("@_id_category", transaction.CategoryId);
                command.Parameters.AddWithValue("@_description", transaction.Description);
                command.ExecuteNonQuery();
            }
            return transaction;
        }

        public Transaction MakeTransfer(Transaction transaction)
        {
            string sqlExpression = "PR_Wallets_Update_MakeTransfer";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlExpression, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@_id_wallet_source", transaction.SourceWalletId);
                command.Parameters.AddWithValue("@_id_wallet_target", transaction.TargetWalletId);
                command.Parameters.AddWithValue("@_amount", transaction.Amount);
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
                string sqlExpression = $"EXEC PR_Actions_Update {id}, '{transaction.CategoryId}', '{transaction.Description}'";
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

        public Transaction Get(int id)
        {
            var transaction = new Transaction();
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = $"EXEC PR_Actions_Read {id}";

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var t = new Transaction();
                        t.Id = dr.GetInt32("id");
                        t.CategoryId = dr.GetInt32("id_category");
                        t.Description = dr.GetString("description");
                        transaction = t;
                    }
                }
            }
            return transaction;
        }

        public Transaction SetInitialBalance(Transaction transaction)
        {
            string sqlExpression = "PR_Wallets_Update_SetInitialBalance";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlExpression, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@_id_wallet", transaction.SourceWalletId);
                command.Parameters.AddWithValue("@_initial_balance", transaction.Amount);
                command.ExecuteNonQuery();
            }
            return transaction;
        }
    }
}
