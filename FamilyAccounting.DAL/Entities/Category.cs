﻿namespace FamilyAccounting.DAL.Entities
{
    public class Category
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public bool Type { get; set; } //0 - income, 1 - expense
        public decimal Amount { get; set; } 
    }
}
