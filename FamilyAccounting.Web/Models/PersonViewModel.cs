using FamilyAccounting.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyAccounting.Web.Models
{
    public class PersonViewModel
    {
       
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; set; }
       // public IEnumerable<WalletDTO> Wallets { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public bool IsActive { get; set; }
       public IEnumerable<PersonDTO> Persons { get; set; }
    }
}
