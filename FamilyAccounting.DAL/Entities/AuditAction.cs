using System;

namespace FamilyAccounting.DAL.Entities
{
    public class AuditAction
    {
        public Transaction Transaction { get; set; }
        public string Type { get; set; }
        public DateTime Time { get; set; }
    }
}
