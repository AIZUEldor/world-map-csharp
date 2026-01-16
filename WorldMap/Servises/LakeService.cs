using System;
using System.Data;
using System.Data.SqlClient;
using WorldMap.Helpers;

namespace WorldMap.Services
{
    public class LakeService
    {
        public static DataTable GetAll()
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                const string sql = @"
SELECT LakeID, Name, AreaKm2
FROM Lakes
ORDER BY Name;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static void Add(string name, double? areaKm2)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                const string sql = @"
INSERT INTO Lakes(Name, AreaKm2)
VALUES(@name, @area);";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@area", (object)areaKm2 ?? DBNull.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Update(int id, string name, double? areaKm2)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                const string sql = @"
UPDATE Lakes
SET Name=@name, AreaKm2=@area
WHERE LakeID=@id;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@area", (object)areaKm2 ?? DBNull.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(int id)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();

                using (SqlCommand cmd1 = new SqlCommand("DELETE FROM LakesCountries WHERE LakeID=@id;", conn))
                {
                    cmd1.Parameters.AddWithValue("@id", id);
                    cmd1.ExecuteNonQuery();
                }

                using (SqlCommand cmd2 = new SqlCommand("DELETE FROM Lakes WHERE LakeID=@id;", conn))
                {
                    cmd2.Parameters.AddWithValue("@id", id);
                    cmd2.ExecuteNonQuery();
                }
            }
        }

        public static string GetNameById(int id)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                const string sql = "SELECT Name FROM Lakes WHERE LakeID=@id;";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    return cmd.ExecuteScalar()?.ToString() ?? "";
                }
            }
        }
    }
}
