using FamilyAccounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyAccounting.Web.Interfaces
{
    public interface ITransactionWebService
    {
        public TransactionViewModel MakeExpense(TransactionViewModel transaction);
        public TransactionViewModel Update(int id, TransactionViewModel transaction);
        public TransactionViewModel Get(int id);
    }
}
