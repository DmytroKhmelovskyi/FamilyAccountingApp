using System;

namespace FamilyAccounting.Web.Models
{
    public class AuditActionViewModel
    {
        public TransactionViewModel Transaction { get; set; }
        public string Type { get; set; }
        public DateTime Time { get; set; }
    }
}
