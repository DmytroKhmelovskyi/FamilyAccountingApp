using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FamilyAccounting.Web.Models
{
    public class PersonViewModel
    {       
        public int Id { get; set; }
        [StringLength(20, MinimumLength = 2)]
        [Required(ErrorMessage = "Please fill in this field")]
        public string FirstName { get; set; }
        [StringLength(25, MinimumLength = 2)]
        [Required(ErrorMessage = "Please fill in this field")]
        public string LastName { get; set; }
        [RegularExpression(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$", ErrorMessage = "Invalid Phone Number")]
        [Required(ErrorMessage = "Please fill in this field")]
        public string Phone { get; set; }
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        [Required(ErrorMessage = "Please fill in this field")]
        public string Email { get; set; }
        public int WalletsCount { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<WalletViewModel> Wallets { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public bool IsActive { get; set; }
    }
}
