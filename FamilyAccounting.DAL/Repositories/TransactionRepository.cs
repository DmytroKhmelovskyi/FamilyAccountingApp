﻿using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace FamilyAccounting.DAL.Repositories
{
    class TransactionRepository : ITransactionRepository
    {
        private readonly string connectionString;
        public TransactionRepository(DbConfig dbConfig)
        {
            connectionString = dbConfig.ConnectionString;
        }
        public Transaction MakeExpense(Transaction transaction)
        {
            string sqlExpression = "PR_Wallets_Update_MakeExpense";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlExpression, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@_id_wallet", transaction.SourceWallet);
                command.Parameters.AddWithValue("@_amount", transaction.Amount);
                command.Parameters.AddWithValue("@_id_category", transaction.Category);
                command.Parameters.AddWithValue("@_description", transaction.Description);
                SqlParameter output = new SqlParameter
                {
                    ParameterName = "@_success",
                    SqlDbType = SqlDbType.Int
                };
                output.Direction = ParameterDirection.Output;
                command.Parameters.Add(output);
                transaction.State = (bool)command.Parameters["@_success"].Value;
                command.ExecuteNonQuery();
            }
            return transaction;
        }
    }
}