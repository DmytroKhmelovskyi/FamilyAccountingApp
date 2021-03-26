using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System.Collections.Generic;
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

        public Transaction Get(int walletId, int transactionId)
        {
            var transaction = new Transaction();
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = $"EXEC PR_ActionsWallets_Read {walletId}, {transactionId}";

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int sourceId = dr.IsDBNull("id_wallet_source") ? 0 : dr.GetInt32("id_wallet_source");
                        string sourceDescription = dr.IsDBNull("wallet_src_desc") ? "" : dr.GetString("wallet_src_desc");
                        int targetId = dr.IsDBNull("id_wallet_target") ? 0 : dr.GetInt32("id_wallet_target");
                        string targetDescription = dr.IsDBNull("wallet_trg_desc") ? "" : dr.GetString("wallet_trg_desc");
                        int categoryId = dr.IsDBNull("id_category") ? 0 : dr.GetInt32("id_category");
                        string categoryDescription = dr.IsDBNull("category_desc") ? "" : dr.GetString("category_desc");

                        transaction = new Transaction
                        {
                            Id = dr.GetInt32("id"),
                            SourceWallet = sourceDescription,
                            SourceWalletId = sourceId,
                            TargetWallet = targetDescription,
                            TargetWalletId = targetId,
                            Category = categoryDescription,
                            CategoryId = categoryId,
                            Amount = dr.GetDecimal("amount"),
                            TimeStamp = dr.GetDateTime("timestamp"),
                            State = dr.GetBoolean("success"),
                            Description = dr.GetString("description"),
                            TransactionType = (TransactionType)dr.GetInt32("type"),
                            BalanceBefore = dr.GetDecimal("balance_prev"),
                            BalanceAfter = dr.GetDecimal("balance")
                        };
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

        public IEnumerable<Category> GetExpenseCategories()
        {
            string sqlProcedure = "PR_Categories_Read_Expenses";
            List<Category> table = new List<Category>();
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
                        Category category = new Category
                        {
                            Id = reader.GetInt32("id"),
                            Description = reader.GetString("description"),
                            Amount = 0,
                            Type = true
                        };
                        table.Add(category);
                    }
                }
                reader.Close();
            }
            return table;
        }

        public IEnumerable<Category> GetIncomeCategories()
        {
            string sqlProcedure = "PR_Categories_Read_Incomes";
            List<Category> table = new List<Category>();
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
                        Category category = new Category
                        {
                            Id = reader.GetInt32("id"),
                            Description = reader.GetString("description"),
                            Amount = reader.GetDecimal("total")
                        };
                        table.Add(category);
                    }
                }
                reader.Close();
            }
            return table;
        }
    }
}
