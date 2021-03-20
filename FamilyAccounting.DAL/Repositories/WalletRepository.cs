﻿using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FamilyAccounting.DAL.Repositories
{
    class WalletRepository : IWalletRepository
    {
        private readonly string connectionString;
        public WalletRepository(DbConfig dbConfig)
        {
            connectionString = dbConfig.ConnectionString;
        }
        public IEnumerable<Wallet> Get()
        {
            string sqlProcedure = "PR_Wallets_Read";
            List<Wallet> table = new List<Wallet>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlProcedure, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Wallet wallet = new Wallet
                        {
                            Id = reader.GetInt32(0),
                            Description = reader.GetString(4),
                            IsCash = reader.GetBoolean(5),
                            Balance = reader.GetDecimal(9)
                        };
                        table.Add(wallet);
                    }
                }
                reader.Close();
            }
            return table;
        }
        public Wallet Get(int id)
        {
            Wallet wallet = new Wallet();
            using (var con = new SqlConnection(connectionString))
            {
                string sqlProcedure = "PR_Wallets_Read";
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = id
                };
                cmd.Parameters.Add(idParam);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Wallet w = new Wallet
                        {
                            Description = dr.GetString("description"),
                            Balance = dr.GetDecimal("balance"),
                            IsActive = dr.GetBoolean("inactive"),
                            Income = dr.GetDecimal("total_income"),
                            Expense = dr.GetDecimal("total_expense")
                        };
                        wallet = w;
                    }
                }
                return wallet;
            }
        }
    }
}
