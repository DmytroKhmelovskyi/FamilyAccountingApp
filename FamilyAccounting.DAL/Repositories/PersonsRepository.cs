using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
