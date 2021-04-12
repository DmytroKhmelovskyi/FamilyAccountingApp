using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<TransactionViewModel> Get(int walletId, int transactionId)
        {
            try
            {
                TransactionDTO transaction = await transactionService.Get(walletId, transactionId);
                return mapper.Map<TransactionViewModel>(transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<CategoryViewModel>> GetExpenseCategories()
        {
            IEnumerable<CategoryDTO> categories = await transactionService.GetExpenseCategories();
            return mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        }

        public async Task<IEnumerable<CategoryViewModel>> GetIncomeCategories()
        {
            IEnumerable<CategoryDTO> categories = await transactionService.GetIncomeCategories();
            return mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        }

        public async Task<TransactionViewModel> MakeExpense(TransactionViewModel transaction)
        {
            try
            {
                TransactionDTO _transaction = await transactionService.MakeExpense(mapper.Map<TransactionDTO>(transaction));
                return mapper.Map<TransactionViewModel>(_transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TransactionViewModel> MakeIncome(TransactionViewModel transaction)
        {
            try
            {
                TransactionDTO _transaction = await transactionService.MakeIncome(mapper.Map<TransactionDTO>(transaction));
                return mapper.Map<TransactionViewModel>(_transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TransactionViewModel> MakeTransfer(TransactionViewModel transaction)
        {
            try
            {
                TransactionDTO _transaction = await transactionService.MakeTransfer(mapper.Map<TransactionDTO>(transaction));
                return mapper.Map<TransactionViewModel>(_transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TransactionViewModel> SetInitialBalance(TransactionViewModel transaction)
        {
            try
            {
                TransactionDTO _transaction =await transactionService.SetInitialBalance(mapper.Map<TransactionDTO>(transaction));
                return mapper.Map<TransactionViewModel>(_transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TransactionViewModel> Update(int id, TransactionViewModel transaction)
        {
            try
            {
                TransactionDTO newTransaction = mapper.Map<TransactionDTO>(transaction);
                TransactionDTO updatedTransaction = await transactionService.Update(id, newTransaction);
                return mapper.Map<TransactionViewModel>(updatedTransaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
