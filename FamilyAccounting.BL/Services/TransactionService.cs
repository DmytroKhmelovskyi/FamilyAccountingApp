using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<TransactionDTO> Get(int walletId, int transactionId)
        {
            try
            {
                Transaction transaction = await transactionRepository.Get(walletId, transactionId);
                return mapper.Map<TransactionDTO>(transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TransactionDTO> MakeExpense(TransactionDTO transaction)
        {
            try
            {
                Transaction _transaction = await transactionRepository.MakeExpense(mapper.Map<Transaction>(transaction));
                return mapper.Map<TransactionDTO>(_transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TransactionDTO> MakeTransfer(TransactionDTO transaction)
        {
            try
            {
                Transaction _transaction = await transactionRepository.MakeTransfer(mapper.Map<Transaction>(transaction));
                return mapper.Map<TransactionDTO>(_transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TransactionDTO> Update(int id, TransactionDTO transaction)
        {
            try
            {
                Transaction newTransaction = mapper.Map<Transaction>(transaction);
                Transaction updatedTransaction = await transactionRepository.Update(id, newTransaction);
                return mapper.Map<TransactionDTO>(updatedTransaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TransactionDTO> SetInitialBalance(TransactionDTO transaction)
        {
            try
            {
                Transaction _transaction = await transactionRepository.SetInitialBalance(mapper.Map<Transaction>(transaction));
                return mapper.Map<TransactionDTO>(_transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TransactionDTO> MakeIncome(TransactionDTO transaction)
        {
            try
            {

                Transaction _transaction = await transactionRepository.MakeIncome(mapper.Map<Transaction>(transaction));
                return mapper.Map<TransactionDTO>(_transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<CategoryDTO>> GetExpenseCategories()
        {
            IEnumerable<Category> categories = await transactionRepository.GetExpenseCategories();
            return mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<IEnumerable<CategoryDTO>> GetIncomeCategories()
        {
            IEnumerable<Category> categories = await transactionRepository.GetIncomeCategories();
            return mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }
    }
}
