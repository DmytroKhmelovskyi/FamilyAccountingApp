using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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
            //Selects every author from a table
            string sqlProcedure = "PR_Persons_Read";
            List<Person> table = new List<Person>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Person person = new Person
                        {
                            FirstName = reader.GetString(1),
                            LastName=reader.GetString(2)
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
            Person newPerson = new Person
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                Phone = person.Phone
            };

            string sqlExpression = $"EXEC PR_Persons_Create '{person.FirstName}', '{person.LastName}', '{person.Email}', '{person.Phone}'";

            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            command.ExecuteNonQuery();

            sql.Close();

            return newPerson;
        }
    }
}
