using System.ComponentModel.DataAnnotations;

namespace FamilyAccounting.Web.Models
{
    public class CardViewModel
    {
        public int WalletId { get; set; }
        [StringLength(16, ErrorMessage = "Invalid card number")]
        public string Number { get; set; }
        public string Description { get; set; }
    }
}
