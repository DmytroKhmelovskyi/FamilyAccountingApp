﻿using FamilyAccounting.DAL.Entities;

namespace FamilyAccounting.DAL.Interfaces
{
    public interface ITransactionRepository
    {
        Transaction MakeExpense(Transaction transaction);
    }
}