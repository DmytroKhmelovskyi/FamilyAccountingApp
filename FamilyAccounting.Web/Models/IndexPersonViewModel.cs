﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyAccounting.Web.Models
{
    public class IndexPersonViewModel
    {
        public IEnumerable<PersonViewModel> Persons { get; set; }
    }
}
