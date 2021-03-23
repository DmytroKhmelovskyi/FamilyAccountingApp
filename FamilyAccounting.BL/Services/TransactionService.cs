using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System;

namespace FamilyAccounting.BL.Services
{
    class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IMapper mapper;
        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
        {
            this.transactionRepository = transactionRepository;
            this.mapper = mapper;
        }

        public TransactionDTO Get(int id)
        {

            Transaction transaction = transactionRepository.Get(id);
            return mapper.Map<TransactionDTO>(transaction);

        }

        public TransactionDTO MakeExpense(TransactionDTO transaction)
        {
            try
            {
                Transaction _transaction = transactionRepository.MakeExpense(mapper.Map<Transaction>(transaction));
                return mapper.Map<TransactionDTO>(_transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TransactionDTO Update(int id, TransactionDTO transaction)
        {
            try
            {
                Transaction newTransaction = mapper.Map<Transaction>(transaction);
                Transaction updatedTransaction = transactionRepository.Update(id, newTransaction);
                return mapper.Map<TransactionDTO>(updatedTransaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
