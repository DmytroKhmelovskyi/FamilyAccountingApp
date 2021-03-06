using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FamilyAccounting.DAL.Repositories
{

    public class PersonRepository : IPersonRepository
    {
        private readonly string connectionString;
        public PersonRepository(DbConfig dbConfig)
        {
            connectionString = dbConfig.ConnectionString;
        }

        public async Task<IEnumerable<Person>> GetAsync()
        {
            string sqlProcedure = "PR_Persons_Read";
            List<Person> table = new List<Person>();
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
                        Person person = new Person
                        {
                            Id = reader.GetInt32("id"),
                            FirstName = reader.GetString("name"),
                            LastName = reader.GetString("surname"),
                            IsActive = !reader.GetBoolean("inactive"),
                            WalletsCount = reader.GetInt32("active_wallets")
                        };
                        table.Add(person);
                    }
                }
                reader.Close();
            }
            return table;
        }

        public async Task<Person> AddAsync(Person person)
        {
            string sqlExpression = $"EXEC PR_Persons_Create '{person.FirstName}', '{person.LastName}', '{person.Email}', '{person.Phone}'";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlExpression, con);
                await command.ExecuteNonQueryAsync();

                con.Close();
            }

            return person;
        }
        public async Task<Person> UpdateAsync(int id, Person person)
        {
            string sqlExpression = $"EXEC PR_Persons_Update {id}, '{person.FirstName}', '{person.LastName}', '{person.Email}', '{person.Phone}'";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlExpression, con);
                await command.ExecuteNonQueryAsync();
                con.Close();
            }

            return person;
        }

        public async Task<Person> GetAsync(int id)
        {
            var person = new Person();
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = $"EXEC PR_Persons_Read {id}";

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (dr.Read())
                    {
                        var p = new Person();
                        p.Id = dr.GetInt32("id");
                        p.FirstName = dr.GetString("name");
                        p.LastName = dr.GetString("surname");
                        p.Email = dr.GetString("email");
                        p.Phone = dr.GetString("phone");
                        p.WalletsCount = dr.GetInt32("active_wallets");
                        p.IsActive = !dr.GetBoolean("inactive");
                        p.Balance = dr.GetDecimal("active_balance");
                        p.TotalIncome = dr.GetDecimal("total_income");
                        p.TotalExpense = dr.GetDecimal("total_expense");
                        person = p;
                    }
                }
            }
            return person;
        }

        public async Task<int> DeleteAsync(int id)
        {
            string sqlExpression = "PR_Persons_Delete";

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(sqlExpression, conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@_id", id);

                SqlParameter output = new SqlParameter
                {
                    ParameterName = "@_status",
                    SqlDbType = SqlDbType.Int
                };
                output.Direction = ParameterDirection.Output;
                command.Parameters.Add(output);

                await command.ExecuteNonQueryAsync();

                int deleteStatus = (int)command.Parameters["@_status"].Value;

                conn.Close();
                return deleteStatus;
            }
        }

        public async Task<IEnumerable<Wallet>> GetWalletsAsync(int id)
        {
            string sqlProcedure = $"EXEC PR_WalletsPersons_Read {id}";
            List<Wallet> table = new List<Wallet>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlProcedure, con);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
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
                            Balance = reader.GetDecimal("balance")
                        };
                        table.Add(wallet);
                    }
                }
                reader.Close();
            }
            return table;
        }
        public IEnumerable<Person> Get()
        {
            string sqlProcedure = "PR_Persons_Read";
            List<Person> table = new List<Person>();
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
                            IsActive = !reader.GetBoolean("inactive"),
                            WalletsCount = reader.GetInt32("active_wallets")
                        };
                        table.Add(person);
                    }
                }
                reader.Close();
            }
            return table;
        }

        public Person Add(Person person)
        {
            string sqlExpression = $"EXEC PR_Persons_Create '{person.FirstName}', '{person.LastName}', '{person.Email}', '{person.Phone}'";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlExpression, con);
                command.ExecuteNonQuery();

                con.Close();
            }

            return person;
        }
        public Person Update(int id, Person person)
        {
            string sqlExpression = $"EXEC PR_Persons_Update {id}, '{person.FirstName}', '{person.LastName}', '{person.Email}', '{person.Phone}'";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlExpression, con);
                command.ExecuteNonQuery();
                con.Close();
            }

            return person;
        }

        public Person Get(int id)
        {
            var person = new Person();
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = $"EXEC PR_Persons_Read {id}";

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var p = new Person();
                        p.Id = dr.GetInt32("id");
                        p.FirstName = dr.GetString("name");
                        p.LastName = dr.GetString("surname");
                        p.Email = dr.GetString("email");
                        p.Phone = dr.GetString("phone");
                        p.WalletsCount = dr.GetInt32("active_wallets");
                        p.IsActive = !dr.GetBoolean("inactive");
                        p.Balance = dr.GetDecimal("active_balance");
                        p.TotalIncome = dr.GetDecimal("total_income");
                        p.TotalExpense = dr.GetDecimal("total_expense");
                        person = p;
                    }
                }
            }
            return person;
        }

        public  int Delete(int id)
        {
            string sqlExpression = "PR_Persons_Delete";

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(sqlExpression, conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@_id", id);

                SqlParameter output = new SqlParameter
                {
                    ParameterName = "@_status",
                    SqlDbType = SqlDbType.Int
                };
                output.Direction = ParameterDirection.Output;
                command.Parameters.Add(output);

                command.ExecuteNonQuery();

                int deleteStatus = (int)command.Parameters["@_status"].Value;

                conn.Close();
                return deleteStatus;
            }
        }

        public IEnumerable<Wallet> GetWallets(int id)
        {
            string sqlProcedure = $"EXEC PR_WalletsPersons_Read {id}";
            List<Wallet> table = new List<Wallet>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlProcedure, con);
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
                            Balance = reader.GetDecimal("balance")
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
