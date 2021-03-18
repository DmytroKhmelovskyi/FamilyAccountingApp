using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FamilyAccounting.DAL.Repositories
{
    class PersonsRepository : IPersonsRepository
    {
        private readonly string connectionString;
        public PersonsRepository(DbConfig dbConfig)
        {
            connectionString = dbConfig.ConnectionString;
        }

        public IEnumerable<Person> GetListOfPersons()
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
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2)
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
    }
}
