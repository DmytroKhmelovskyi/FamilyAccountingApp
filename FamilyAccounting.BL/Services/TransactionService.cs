using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System;

namespace FamilyAccounting.BL.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IMapper mapper;
        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
        {
            this.transactionRepository = transactionRepository;
            this.mapper = mapper;
        }

        public TransactionDTO Get(int walletId, int transactionId)
        {
            try
            {
                Transaction transaction = transactionRepository.Get(walletId, transactionId);
                return mapper.Map<TransactionDTO>(transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

        public TransactionDTO MakeTransfer(TransactionDTO transaction)
        {
            try
            {
                Transaction _transaction = transactionRepository.MakeTransfer(mapper.Map<Transaction>(transaction));
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

        public TransactionDTO SetInitialBalance(TransactionDTO transaction)
        {
            try
            {
                Transaction _transaction = transactionRepository.SetInitialBalance(mapper.Map<Transaction>(transaction));
                return mapper.Map<TransactionDTO>(_transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TransactionDTO MakeIncome(TransactionDTO transaction)
        {
            try
            {
                Transaction _transaction = transactionRepository.MakeIncome(mapper.Map<Transaction>(transaction));
                return mapper.Map<TransactionDTO>(_transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
