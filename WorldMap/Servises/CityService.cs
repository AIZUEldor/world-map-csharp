using System;
using System.Data;
using System.Data.SqlClient;
using WorldMap.Helpers;

namespace WorldMap.Services
{
    public class CityService
    {
        public static DataTable GetAll()
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                const string sql = @"
SELECT ci.CityID, ci.Name,
       co.CountryID, co.Name AS Country,
       ci.Population
FROM Cities ci
JOIN Countries co ON co.CountryID = ci.CountryID
ORDER BY ci.Name;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static void Add(string name, int countryId, long? population)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                const string sql = @"
INSERT INTO Cities(Name, CountryID, Population)
VALUES(@name, @cid, @pop);";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar, 150).Value = name;
                    cmd.Parameters.Add("@cid", SqlDbType.Int).Value = countryId;
                    cmd.Parameters.Add("@pop", SqlDbType.BigInt).Value = (object)population ?? DBNull.Value;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Update(int cityId, string name, int countryId, long? population)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                const string sql = @"
UPDATE Cities
SET Name=@name, CountryID=@cid, Population=@pop
WHERE CityID=@id;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = cityId;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar, 150).Value = name;
                    cmd.Parameters.Add("@cid", SqlDbType.Int).Value = countryId;
                    cmd.Parameters.Add("@pop", SqlDbType.BigInt).Value = (object)population ?? DBNull.Value;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(int cityId)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                const string sql = "DELETE FROM Cities WHERE CityID=@id;";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = cityId;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
