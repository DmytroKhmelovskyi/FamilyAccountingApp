using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using System;

namespace FamilyAccounting.Web.Services
{
    public class TransactionWebService : ITransactionWebService
    {
        private readonly ITransactionService transactionService;
        private readonly IMapper mapper;
        public TransactionWebService(IMapper mapper, ITransactionService transactionService)
        {
            this.transactionService = transactionService;
            this.mapper = mapper;
        }

        public TransactionViewModel Get(int id)
        {

            TransactionDTO transaction = transactionService.Get(id);
            return mapper.Map<TransactionViewModel>(transaction);

        }

        public TransactionViewModel MakeExpense(TransactionViewModel transaction)
        {
            try
            {
                TransactionDTO _transaction = transactionService.MakeExpense(mapper.Map<TransactionDTO>(transaction));
                return mapper.Map<TransactionViewModel>(_transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TransactionViewModel SetInitialBalance(TransactionViewModel transaction)
        {
            throw new NotImplementedException();
        }

        public TransactionViewModel Update(int id, TransactionViewModel transaction)
        {
            try
            {
                TransactionDTO newTransaction = mapper.Map<TransactionDTO>(transaction);
                TransactionDTO updatedTransaction = transactionService.Update(id, newTransaction);
                return mapper.Map<TransactionViewModel>(updatedTransaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
