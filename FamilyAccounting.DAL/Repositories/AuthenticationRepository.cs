using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAccounting.DAL.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly string connectionString;
        public AuthenticationRepository(DbConfig dbConfig)
        {
            connectionString = dbConfig.ConnectionString;
        }

        public User Login(string password, string login)
        {
            var user = new User();
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = $"EXEC PR_Auth_Read '{login}', '{password}' ";

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        user.Id = dr.GetInt32("id_user");
                        user.Email = dr.GetString("email");
                        user.Password = dr.GetString("password");
                        user.RoleId = dr.GetInt32("id_role");
                        user.RoleName = dr.GetString("role_name");
                    }
                }
            }
            return user;
        }
    }
}
