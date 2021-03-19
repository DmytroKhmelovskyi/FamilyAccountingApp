using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyAccounting.BL.DTO
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int WalletsCount { get; set; }
        public decimal Balance { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public IEnumerable<WalletDTO> Wallets { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public bool IsActive { get; set; }
    }
}
