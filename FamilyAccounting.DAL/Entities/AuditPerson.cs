using System;

namespace FamilyAccounting.DAL.Entities
{
    public class AuditPerson
    {
        public Person Person { get; set; }
        public string Type { get; set; }
        public DateTime Time { get; set; }
    }
}
