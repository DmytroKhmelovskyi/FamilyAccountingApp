using System.Collections.Generic;

namespace FamilyAccounting.Web.Models
{
    public class PersonViewModel
    {       
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int WalletsCount { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<WalletViewModel> Wallets { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public bool IsActive { get; set; }
    }
}
