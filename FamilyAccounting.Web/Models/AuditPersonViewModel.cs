using System;

namespace FamilyAccounting.Web.Models
{
    public class AuditPersonViewModel
    {
        public PersonViewModel Person { get; set; }
        public string Type { get; set; }
        public DateTime Time { get; set; }
    }
}
