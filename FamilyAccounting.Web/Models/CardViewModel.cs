﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyAccounting.Web.Models
{
    public class CardViewModel
    {
        public int WalletId { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
    }
}
