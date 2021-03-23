using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System.Data;
using System.Data.SqlClient;

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

            using (var sql = new SqlConnection(connectionString))
            {
                sql.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sql);
                command.ExecuteNonQuery();
                sql.Close();
            }

            return card;
        }
        public int Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PR_Cards_Delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_id", id);
                SqlParameter output = new SqlParameter
                {
                    ParameterName = "@_status",
                    SqlDbType = SqlDbType.Int
                };
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
                int deleteStatus = (int)cmd.Parameters["@_status"].Value;
                return deleteStatus;
            }
        }
        public Card Get(int id)
        {
            var card = new Card();
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = $"EXEC PR_CardsWallets_Read {id}";

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var c = new Card();
                        c.WalletId = dr.GetInt32("id_wallet");
                        c.Description = dr.GetString("description");
                        c.Number = dr.GetString("card_number");
                        card = c;
                    }
                }
            }
            return card;
        }
        public Card Update(int id, Card card)
        {
            string sqlExpression = $"EXEC PR_Cards_Update {id}, '{card.Number}','{card.Description}'";
            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                sql.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sql);
                command.ExecuteNonQuery();
                sql.Close();
            }
            return card;
        }
    }
}
