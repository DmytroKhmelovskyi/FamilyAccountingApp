using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FamilyAccounting.DAL.Repositories
{

    class PersonRepository : IPersonRepository
    {
        private readonly string connectionString;
        public PersonRepository(DbConfig dbConfig)
        {
            connectionString = dbConfig.ConnectionString;
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

            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            command.ExecuteNonQuery();

            sql.Close();

            return person;
        }
        public Person Update(int id, Person person)
        {
            string sqlExpression = $"EXEC PR_Persons_Update {id}, '{person.FirstName}', '{person.LastName}', '{person.Email}', '{person.Phone}'";
            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();
            SqlCommand command = new SqlCommand(sqlExpression, sql);
            command.ExecuteNonQuery();
            sql.Close();
            return person;
        }

        public Person Get(int id)
        {
            var person = new Person();
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = $"EXEC PR_Persons_Read @_Id = {id}";

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
                        p.IsActive = dr.GetBoolean("inactive");
                        p.Balance = dr.GetDecimal("active_balance");
                        p.TotalIncome = dr.GetDecimal("total_income");
                        //p.TotalExpense = dr.GetDecimal("total_expence");
                        person = p;
                    }
                }
            }
            return person;
        }

        public void Delete(int id)
        {
            string sqlExpression = $"EXEC PR_Persons_Delete {id}";

            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            command.ExecuteNonQuery();

            sql.Close();
        }
    }
}
