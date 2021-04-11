using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FamilyAccounting.DAL.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly string connectionString;
        public WalletRepository(DbConfig dbConfig)
        {
            connectionString = dbConfig.ConnectionString;
        }

        public async Task<IEnumerable<Wallet>> Get()
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
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Wallet wallet = new Wallet
                        {
                            Id = reader.GetInt32("id"),
                            Description = reader.GetString("description"),
                            PersonName = reader.GetString("fullname"),
                            IsActive = true,
                            IsCash = true,
                            Balance = 0,
                            Income = 0,
                            Expense = 0,
                            PersonId = 0,
                            Transactions = null

                        };
                        table.Add(wallet);
                    }
                }
                reader.Close();
            }
            return table;
        }

        public async Task<Wallet> Get(int id)
        {
            Wallet wallet = new Wallet();
            using (var con = new SqlConnection(connectionString))
            {
                string sqlProcedure = "PR_Wallets_Read";
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "_id",
                    Value = id
                };
                cmd.Parameters.Add(idParam);
                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (dr.Read())
                    {
                        Wallet w = new Wallet
                        {
                            Id = dr.GetInt32("id"),
                            Description = dr.GetString("description"),
                            Balance = dr.GetDecimal("balance"),
                            IsActive = !dr.GetBoolean("inactive"),
                            Income = dr.GetDecimal("total_income"),
                            Expense = dr.GetDecimal("total_expense"),
                            PersonId = dr.GetInt32("id_person"),
                            IsCash = dr.GetBoolean("is_cash"),
                        };
                        wallet = w;
                    }
                }
                return wallet;
            }
        }

        public async Task<Wallet> Update(int id, Wallet wallet)
        {
            string sqlExpression = $"EXEC PR_Wallets_Update {id}, '{wallet.Description}'";

            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                sql.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sql);
                await command.ExecuteNonQueryAsync();
                sql.Close();
            }

            return wallet;
        }

        public async Task<int> Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PR_Wallets_Delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_id", id);
                SqlParameter output = new SqlParameter
                {
                    ParameterName = "@_status",
                    SqlDbType = SqlDbType.Int
                };
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                await cmd.ExecuteNonQueryAsync();
                int deleteStatus = (int)cmd.Parameters["@_status"].Value;
                return deleteStatus;
            }
        }

        public async Task<Wallet> Create(Wallet wallet)
        {
            string sqlExpression = $"EXEC PR_Wallets_Create '{wallet.PersonId}', '{wallet.Description}', '{wallet.Balance}'";

            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                sql.Open();

                SqlCommand command = new SqlCommand(sqlExpression, sql);
                await command.ExecuteNonQueryAsync();

                sql.Close();
            }

            return wallet;
        }

        public async Task<IEnumerable<Transaction>> GetTransactions(int walletId)
        {
            string sqlExpression = $"EXEC PR_ActionsWallets_Read {walletId}";

            List<Transaction> transactions = new List<Transaction>();

            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                sql.Open();

                SqlCommand command = new SqlCommand(sqlExpression, sql);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int sourceId = reader.IsDBNull("id_wallet_source") ? 0 : reader.GetInt32("id_wallet_source");
                        string sourceDescription = reader.IsDBNull("wallet_src_desc") ? "" : reader.GetString("wallet_src_desc");
                        int targetId = reader.IsDBNull("id_wallet_target") ? 0 : reader.GetInt32("id_wallet_target");
                        string targetDescription = reader.IsDBNull("wallet_trg_desc") ? "" : reader.GetString("wallet_trg_desc");
                        int categoryId = reader.IsDBNull("id_category") ? 0 : reader.GetInt32("id_category");
                        string categoryDescription = reader.IsDBNull("category_desc") ? "" : reader.GetString("category_desc");

                        Transaction transaction = new Transaction
                        {
                            Id = reader.GetInt32("id"),
                            SourceWallet = sourceDescription,
                            SourceWalletId = sourceId,
                            TargetWallet = targetDescription,
                            TargetWalletId = targetId,
                            Category = categoryDescription,
                            CategoryId = categoryId,
                            Amount = reader.GetDecimal("amount"),
                            TimeStamp = reader.GetDateTime("timestamp"),
                            State = reader.GetBoolean("success"),
                            Description = reader.GetString("description"),
                            TransactionType = (TransactionType)reader.GetInt32("type"),
                            BalanceBefore = reader.GetDecimal("balance_prev"),
                            BalanceAfter = reader.GetDecimal("balance")
                        };

                        transactions.Add(transaction);
                    }
                }
                sql.Close();
            }
            return transactions;
        }

        public async Task<IEnumerable<Transaction>> GetTransactions(int walletId, DateTime from, DateTime to)
        {
            //DateTime fromDate = DateTime.Parse(from);
            //DateTime toDate = DateTime.Parse(to);
            //string sqlExpression = $"EXEC PR_ActionsWallets_Read {walletId}, NULL, '{fromDate}', '{toDate}'";
            string sqlExpression = $"PR_ActionsWallets_Read";

            List<Transaction> transactions = new List<Transaction>();

            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                sql.Open();

                SqlCommand command = new SqlCommand(sqlExpression, sql);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter id = new SqlParameter
                {
                    ParameterName = "@_id_wallet",
                    Value = walletId
                };
                command.Parameters.Add(id);
                SqlParameter fromDateParam = new SqlParameter
                {
                    ParameterName = "@_time_from",
                    Value = from
                };
                command.Parameters.Add(fromDateParam);
                SqlParameter toDateParam = new SqlParameter
                {
                    ParameterName = "@_time_to",
                    Value = to
                };
                command.Parameters.Add(toDateParam);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int sourceId = reader.IsDBNull("id_wallet_source") ? 0 : reader.GetInt32("id_wallet_source");
                        string sourceDescription = reader.IsDBNull("wallet_src_desc") ? "" : reader.GetString("wallet_src_desc");
                        int targetId = reader.IsDBNull("id_wallet_target") ? 0 : reader.GetInt32("id_wallet_target");
                        string targetDescription = reader.IsDBNull("wallet_trg_desc") ? "" : reader.GetString("wallet_trg_desc");
                        int categoryId = reader.IsDBNull("id_category") ? 0 : reader.GetInt32("id_category");
                        string categoryDescription = reader.IsDBNull("category_desc") ? "" : reader.GetString("category_desc");

                        Transaction transaction = new Transaction
                        {
                            Id = reader.GetInt32("id"),
                            SourceWallet = sourceDescription,
                            SourceWalletId = sourceId,
                            TargetWallet = targetDescription,
                            TargetWalletId = targetId,
                            Category = categoryDescription,
                            CategoryId = categoryId,
                            Amount = reader.GetDecimal("amount"),
                            TimeStamp = reader.GetDateTime("timestamp"),
                            State = reader.GetBoolean("success"),
                            Description = reader.GetString("description"),
                            TransactionType = (TransactionType)reader.GetInt32("type"),
                            BalanceBefore = reader.GetDecimal("balance_prev"),
                            BalanceAfter = reader.GetDecimal("balance")
                        };

                        transactions.Add(transaction);
                    }
                }
                sql.Close();
            }
            return transactions;
        }
        public async Task<Wallet> MakeActive(int id)
        {
            Wallet wallet = new Wallet();
            using (var con = new SqlConnection(connectionString))
            {
                string sqlProcedure = "PR_Wallets_Update_Activate";
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "_id",
                    Value = id
                };
                cmd.Parameters.Add(idParam);
                await cmd.ExecuteNonQueryAsync();
                return wallet;
            }
        }
    }
}
