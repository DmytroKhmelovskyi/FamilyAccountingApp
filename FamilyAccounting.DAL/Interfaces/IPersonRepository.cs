﻿using FamilyAccounting.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyAccounting.DAL.Interfaces
{
    public interface IPersonRepository
    {
        public Person Add(Person person);
        public Person Update(int id, Person person);
        IEnumerable<Person> Get();
        public Person Get(int id);
    }
}