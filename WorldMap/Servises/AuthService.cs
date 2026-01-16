using System;
using System.Data;
using System.Data.SqlClient;
using WorldMap.Helpers;

namespace WorldMap.Services
{
    public class AuthService
    {
        public static bool Login(string username, string password)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                const string query = @"
SELECT COUNT(1)
FROM Users
WHERE Username = @u AND PasswordHash = @p;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@u", SqlDbType.NVarChar, 50).Value = username;
                    cmd.Parameters.Add("@p", SqlDbType.NVarChar, 256).Value = PasswordHelper.Hash(password);

                    conn.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count == 1;
                }
            }
        }
    }
}
