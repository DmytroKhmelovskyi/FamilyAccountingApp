using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FamilyAccounting.DAL.Repositories
{
    class PersonsRepository:IPersonsRepository
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
    }
}
