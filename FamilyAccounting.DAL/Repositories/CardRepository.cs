using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FamilyAccounting.DAL.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly string connectionString;
        public CardRepository(DbConfig dbConfig)
        {
            connectionString = dbConfig.ConnectionString;
        }

        public async Task<Card> Create(Card card)
        {
            string sqlExpression = $"EXEC PR_Cards_Create {card.WalletId}, '{card.Number}', '{card.Description}'";

            using (var sql = new SqlConnection(connectionString))
            {
                sql.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sql);
                await command.ExecuteNonQueryAsync();
                sql.Close();
            }

            return card;
        }
        public async Task<int> Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PR_Cards_Delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_id_wallet", id);
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
                await cmd.ExecuteNonQueryAsync();
                int deleteStatus = (int)cmd.Parameters["@_status"].Value;
                return deleteStatus;
            }
        }
        public async Task<Card> Get(int id)
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

                using (var dr = await cmd.ExecuteReaderAsync())
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
        public async Task<Card> Update(int id, Card card)
        {
            string sqlExpression = $"EXEC PR_Cards_Update {id}, '{card.Number}','{card.Description}'";
            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                sql.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sql);
                await command.ExecuteNonQueryAsync();
                sql.Close();
            }
            return card;
        }
    }
}
