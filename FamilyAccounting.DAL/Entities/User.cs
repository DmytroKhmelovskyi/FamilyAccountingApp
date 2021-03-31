﻿namespace FamilyAccounting.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int? RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
