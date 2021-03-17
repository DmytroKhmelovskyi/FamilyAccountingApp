using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Interfaces;
using System;
using System.Collections.Generic;
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

    }
}
