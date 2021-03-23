using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FamilyAccounting.DAL.Repositories
{
    public class WalletRepository : IWalletRepository
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

        public Wallet Get(int id)
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
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Person person = new Person
                        {
                            Id = dr.GetInt32("id_person")
                        };
                        Wallet w = new Wallet
                        {
                            Id = dr.GetInt32("id"),
                            Description = dr.GetString("description"),
                            Balance = dr.GetDecimal("balance"),
                            IsActive = dr.GetBoolean("inactive"),
                            Income = dr.GetDecimal("total_income"),
                            Expense = dr.GetDecimal("total_expense"),
                            Person = person
                        };
                        wallet = w;
                    }
                }
                return wallet;
            }
        }

        public Wallet Update(int id, Wallet wallet)
        {
            string sqlExpression = $"EXEC PR_Wallets_Update {id}, '{wallet.Description}'";

            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                sql.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sql);
                command.ExecuteNonQuery();
                sql.Close();
            }

            return wallet;
        }

        public int Delete(int id)
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
                cmd.ExecuteNonQuery();
                int deleteStatus = (int)cmd.Parameters["@_status"].Value;
                return deleteStatus;
            }
        }

        public Wallet Create(Wallet wallet)
        {
            string sqlExpression = $"EXEC PR_Wallets_Create '{wallet.Person.Id}', '{wallet.Description}', '{wallet.Balance}'";

            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                sql.Open();

                SqlCommand command = new SqlCommand(sqlExpression, sql);
                command.ExecuteNonQuery();

                sql.Close();
            }

            return wallet;
        }

        public IEnumerable<Transaction> GetTransactions(int walletId)
        {
            string sqlExpression = $"EXEC PR_ActionsWallets_Read {walletId}";

            List<Transaction> transactions = new List<Transaction>();

            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                sql.Open();

                SqlCommand command = new SqlCommand(sqlExpression, sql);
                SqlDataReader reader = command.ExecuteReader();

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

                        Wallet sourceWallet = new Wallet
                        {
                            Id = sourceId,
                            Description = sourceDescription
                        };

                        Wallet targetWallet = new Wallet
                        {
                            Id = targetId,
                            Description = targetDescription
                        };
                        Category category = new Category
                        {
                            Id = categoryId,
                            Description = categoryDescription
                        };

                        Transaction transaction = new Transaction
                        {
                            Id = reader.GetInt32("id"),
                            SourceWallet = sourceWallet,
                            TargetWallet = targetWallet,
                            Category = category,
                            Amount = reader.GetDecimal("amount"),
                            TimeStamp = reader.GetDateTime("timestamp"),
                            State = reader.GetBoolean("success"),
                            Description = reader.GetString("description"),
                            TransactionType = (TransactionType)reader.GetInt32("type")
                        };

                        transactions.Add(transaction);
                    }
                }
                sql.Close();
            }
            return transactions;
        }
    }
}
