using System;

namespace FamilyAccounting.BL.DTO
{
    public class AuditActionDTO
    {
        public TransactionDTO Transaction { get; set; }
        public string Type { get; set; }
        public DateTime Time { get; set; }
    }
}
