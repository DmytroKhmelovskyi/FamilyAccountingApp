using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace FamilyAccounting.DAL.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly string connectionString;
        public CardRepository(DbConfig dbConfig)
        {
            connectionString = dbConfig.ConnectionString;
        }

        public Card Create(Card card)
        {
            string sqlExpression = $"EXEC PR_Cards_Create {card.WalletId}, '{card.Number}', '{card.Description}'";

            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();
            SqlCommand command = new SqlCommand(sqlExpression, sql);
            command.ExecuteNonQuery();
            sql.Close();
            return card;
        }
    }
}
