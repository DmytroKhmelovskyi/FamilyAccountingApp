using System.Collections.Generic;

namespace FamilyAccounting.Web.Models
{
    public class IndexPersonViewModel
    {
        public IEnumerable<PersonViewModel> Persons { get; set; }
    }
}
