using System;
using System.Data;
using System.Data.SqlClient;
using WorldMap.Helpers;

namespace WorldMap.Services
{
    public class RegionService
    {
        public static DataTable GetAll()
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                const string sql = @"
SELECT r.RegionID, r.Name,
       c.CountryID, c.Name AS Country
FROM Regions r
JOIN Countries c ON c.CountryID = r.CountryID
ORDER BY c.Name, r.Name;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static void Add(string name, int countryId)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                const string sql = @"
INSERT INTO Regions(Name, CountryID)
VALUES(@name, @cid);";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@cid", countryId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Update(int regionId, string name, int countryId)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                const string sql = @"
UPDATE Regions
SET Name=@name, CountryID=@cid
WHERE RegionID=@id;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", regionId);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@cid", countryId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(int regionId)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                const string sql = "DELETE FROM Regions WHERE RegionID=@id;";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", regionId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
