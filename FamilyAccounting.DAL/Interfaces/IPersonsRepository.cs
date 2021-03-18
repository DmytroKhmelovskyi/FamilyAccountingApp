using FamilyAccounting.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyAccounting.DAL.Interfaces
{
    interface IPersonsRepository
    {
        public Person Add(Person person);
    }
}
