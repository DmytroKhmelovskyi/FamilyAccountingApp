using System;

namespace FamilyAccounting.BL.DTO
{
    public class AuditPersonDTO
    {
        public PersonDTO Person { get; set; }
        public string Type { get; set; }
        public DateTime Time { get; set; }
    }
}
