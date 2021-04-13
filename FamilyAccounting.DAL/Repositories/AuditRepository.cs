using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FamilyAccounting.DAL.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        private readonly string connectionString;
        public AuditRepository(DbConfig dbConfig)
        {
            connectionString = dbConfig.ConnectionString;
        }
        public IEnumerable<AuditAction> GetActions()
        {
            string sqlProcedure = "PR_Audit_Actions_Read";
            List<AuditAction> table = new List<AuditAction>();
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
                        int sourceId = reader.IsDBNull("id_wallet_source") ? 0 : reader.GetInt32("id_wallet_source");
                        int targetId = reader.IsDBNull("id_wallet_target") ? 0 : reader.GetInt32("id_wallet_target");
                        int categoryId = reader.IsDBNull("id_category") ? 0 : reader.GetInt32("id_category");
                        string description = reader.IsDBNull("description") ? "" : reader.GetString("description");

                        Transaction transaction = new Transaction
                        {
                            Id = reader.GetInt32("id"),
                            SourceWalletId = sourceId,
                            TargetWalletId = targetId,
                            CategoryId = categoryId,
                            Amount = reader.GetDecimal("amount"),
                            TimeStamp = reader.GetDateTime("timestamp"),
                            State = reader.GetBoolean("success"),
                            Description = description
                        };
                        AuditAction audit = new AuditAction
                        {
                            Transaction = transaction,
                            Type = reader.GetString("crud_type"),
                            Time = reader.GetDateTime("crud_time")
                        };
                        table.Add(audit);
                    }
                }
                reader.Close();
            }
            return table;
        }

        public IEnumerable<AuditWallet> GetWallets()
        {
            string sqlProcedure = "PR_Audit_Wallets_Read";
            List<AuditWallet> table = new List<AuditWallet>();
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
                            PersonId = reader.GetInt32("id_person"),
                            Description = reader.GetString("description"),
                            IsActive = reader.GetBoolean("inactive")
                        };
                        AuditWallet audit = new AuditWallet
                        {
                            Wallet = wallet,
                            Type = reader.GetString("crud_type"),
                            Time = reader.GetDateTime("crud_time")
                        };
                        table.Add(audit);
                    }
                }
                reader.Close();
            }
            return table;
        }

        public IEnumerable<AuditPerson> GetPersons()
        {
            string sqlProcedure = "PR_Audit_Persons_Read";
            List<AuditPerson> table = new List<AuditPerson>();
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
                        Person person = new Person
                        {
                            Id = reader.GetInt32("id"),
                            FirstName = reader.GetString("name"),
                            LastName = reader.GetString("surname"),
                            Email = reader.GetString("email"),
                            Phone = reader.GetString("phone"),
                            IsActive = !reader.GetBoolean("inactive")
                        };
                        AuditPerson audit = new AuditPerson
                        {
                            Person = person,
                            Type = reader.GetString("crud_type"),
                            Time = reader.GetDateTime("crud_time")
                        };
                        table.Add(audit);
                    }
                }
                reader.Close();
            }
            return table;
        }

        public async Task<IEnumerable<AuditAction>> GetActionsAsync()
        {
            string sqlProcedure = "PR_Audit_Actions_Read";
            List<AuditAction> table = new List<AuditAction>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand(sqlProcedure, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        int sourceId = reader.IsDBNull("id_wallet_source") ? 0 : reader.GetInt32("id_wallet_source");
                        int targetId = reader.IsDBNull("id_wallet_target") ? 0 : reader.GetInt32("id_wallet_target");
                        int categoryId = reader.IsDBNull("id_category") ? 0 : reader.GetInt32("id_category");
                        string description = reader.IsDBNull("description") ? "" : reader.GetString("description");

                        Transaction transaction = new Transaction
                        {
                            Id = reader.GetInt32("id"),
                            SourceWalletId = sourceId,
                            TargetWalletId = targetId,
                            CategoryId = categoryId,
                            Amount = reader.GetDecimal("amount"),
                            TimeStamp = reader.GetDateTime("timestamp"),
                            State = reader.GetBoolean("success"),
                            Description = description
                        };
                        AuditAction audit = new AuditAction
                        {
                            Transaction = transaction,
                            Type = reader.GetString("crud_type"),
                            Time = reader.GetDateTime("crud_time")
                        };
                        table.Add(audit);
                    }
                }
                await reader.CloseAsync();
            }
            return table;
        }

        public async Task<IEnumerable<AuditWallet>> GetWalletsAsync()
        {
            string sqlProcedure = "PR_Audit_Wallets_Read";
            List<AuditWallet> table = new List<AuditWallet>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand(sqlProcedure, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        Wallet wallet = new Wallet
                        {
                            Id = reader.GetInt32("id"),
                            PersonId = reader.GetInt32("id_person"),
                            Description = reader.GetString("description"),
                            IsActive = reader.GetBoolean("inactive")
                        };
                        AuditWallet audit = new AuditWallet
                        {
                            Wallet = wallet,
                            Type = reader.GetString("crud_type"),
                            Time = reader.GetDateTime("crud_time")
                        };
                        table.Add(audit);
                    }
                }
                await reader.CloseAsync();
            }
            return table;
        }

        public async Task<IEnumerable<AuditPerson>> GetPersonsAsync()
        {
            string sqlProcedure = "PR_Audit_Persons_Read";
            List<AuditPerson> table = new List<AuditPerson>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand(sqlProcedure, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        Person person = new Person
                        {
                            Id = reader.GetInt32("id"),
                            FirstName = reader.GetString("name"),
                            LastName = reader.GetString("surname"),
                            Email = reader.GetString("email"),
                            Phone = reader.GetString("phone"),
                            IsActive = !reader.GetBoolean("inactive")
                        };
                        AuditPerson audit = new AuditPerson
                        {
                            Person = person,
                            Type = reader.GetString("crud_type"),
                            Time = reader.GetDateTime("crud_time")
                        };
                        table.Add(audit);
                    }
                }
                await reader.CloseAsync();
            }
            return table;
        }
    }
}
