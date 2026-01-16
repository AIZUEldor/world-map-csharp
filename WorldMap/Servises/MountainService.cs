using System.Data.SqlClient;
using System.Data;
using System;
using WorldMap.Helpers;

public class MountainService
{
    public static DataTable GetAll()
    {
        using (SqlConnection conn = DbHelper.GetConnection())
        {
            const string sql = @"
SELECT MountainID, Name, HeightM
FROM Mountains
ORDER BY Name;";

            using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
            {
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }

    public static void Add(string name, int? height)
    {
        using (SqlConnection conn = DbHelper.GetConnection())
        {
            const string sql = @"
INSERT INTO Mountains(Name, HeightM)
VALUES(@name, @height);";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@height", (object)height ?? DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void Update(int id, string name, int? height)
    {
        using (SqlConnection conn = DbHelper.GetConnection())
        {
            const string sql = @"
UPDATE Mountains
SET Name=@name, HeightM=@height
WHERE MountainID=@id;";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@height", (object)height ?? DBNull.Value);
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

            using (SqlCommand cmd1 = new SqlCommand(
                "DELETE FROM MountainsCountries WHERE MountainID=@id;", conn))
            {
                cmd1.Parameters.AddWithValue("@id", id);
                cmd1.ExecuteNonQuery();
            }

            using (SqlCommand cmd2 = new SqlCommand(
                "DELETE FROM Mountains WHERE MountainID=@id;", conn))
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
            const string sql = "SELECT Name FROM Mountains WHERE MountainID=@id;";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                return cmd.ExecuteScalar()?.ToString() ?? "";
            }
        }
    }
}
