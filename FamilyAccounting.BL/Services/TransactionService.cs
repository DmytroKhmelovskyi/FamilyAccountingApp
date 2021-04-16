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

        public async Task<TransactionDTO> GetAsync(int walletId, int transactionId)
        {
            Transaction transaction = await transactionRepository.GetAsync(walletId, transactionId);
            return mapper.Map<TransactionDTO>(transaction);
        }

        public async Task<TransactionDTO> MakeExpenseAsync(TransactionDTO transaction)
        {
            Transaction _transaction = await transactionRepository.MakeExpenseAsync(mapper.Map<Transaction>(transaction));
            return mapper.Map<TransactionDTO>(_transaction);
        }

        public async Task<TransactionDTO> MakeTransferAsync(TransactionDTO transaction)
        {
            Transaction _transaction = await transactionRepository.MakeTransferAsync(mapper.Map<Transaction>(transaction));
            return mapper.Map<TransactionDTO>(_transaction);
        }

        public async Task<TransactionDTO> UpdateAsync(int id, TransactionDTO transaction)
        {
            Transaction newTransaction = mapper.Map<Transaction>(transaction);
            Transaction updatedTransaction = await transactionRepository.UpdateAsync(id, newTransaction);
            return mapper.Map<TransactionDTO>(updatedTransaction);
        }

        public async Task<TransactionDTO> SetInitialBalanceAsync(TransactionDTO transaction)
        {
            Transaction _transaction = await transactionRepository.SetInitialBalanceAsync(mapper.Map<Transaction>(transaction));
            return mapper.Map<TransactionDTO>(_transaction);
        }

        public async Task<TransactionDTO> MakeIncomeAsync(TransactionDTO transaction)
        {
            Transaction _transaction = await transactionRepository.MakeIncomeAsync(mapper.Map<Transaction>(transaction));
            return mapper.Map<TransactionDTO>(_transaction);
        }

        public async Task<IEnumerable<CategoryDTO>> GetExpenseCategoriesAsync()
        {
            IEnumerable<Category> categories = await transactionRepository.GetExpenseCategoriesAsync();
            return mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<IEnumerable<CategoryDTO>> GetIncomeCategoriesAsync()
        {
            IEnumerable<Category> categories = await transactionRepository.GetIncomeCategoriesAsync();
            return mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }
        public TransactionDTO Get(int walletId, int transactionId)
        {
            Transaction transaction = transactionRepository.Get(walletId, transactionId);
            return mapper.Map<TransactionDTO>(transaction);
        }

        public TransactionDTO MakeExpense(TransactionDTO transaction)
        {
            Transaction _transaction = transactionRepository.MakeExpense(mapper.Map<Transaction>(transaction));
            return mapper.Map<TransactionDTO>(_transaction);
        }

        public TransactionDTO MakeTransfer(TransactionDTO transaction)
        {
            Transaction _transaction = transactionRepository.MakeTransfer(mapper.Map<Transaction>(transaction));
            return mapper.Map<TransactionDTO>(_transaction);
        }

        public TransactionDTO Update(int id, TransactionDTO transaction)
        {
            Transaction newTransaction = mapper.Map<Transaction>(transaction);
            Transaction updatedTransaction = transactionRepository.Update(id, newTransaction);
            return mapper.Map<TransactionDTO>(updatedTransaction);
        }

        public TransactionDTO SetInitialBalance(TransactionDTO transaction)
        {
            Transaction _transaction = transactionRepository.SetInitialBalance(mapper.Map<Transaction>(transaction));
            return mapper.Map<TransactionDTO>(_transaction);
        }

        public TransactionDTO MakeIncome(TransactionDTO transaction)
        {
            Transaction _transaction = transactionRepository.MakeIncome(mapper.Map<Transaction>(transaction));
            return mapper.Map<TransactionDTO>(_transaction);
        }

        public IEnumerable<CategoryDTO> GetExpenseCategories()
        {
            IEnumerable<Category> categories = transactionRepository.GetExpenseCategories();
            return mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public IEnumerable<CategoryDTO> GetIncomeCategories()
        {
            IEnumerable<Category> categories = transactionRepository.GetIncomeCategories();
            return mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }
    }
}
