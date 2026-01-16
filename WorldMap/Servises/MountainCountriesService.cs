using System.Collections.Generic;
using System.Data.SqlClient;
using WorldMap.Helpers;

namespace WorldMap.Services
{
    public class MountainCountriesService
    {
        public static List<int> GetCountryIdsByMountain(int mountainId)
        {
            var ids = new List<int>();

            using (SqlConnection conn = DbHelper.GetConnection())
            {
                const string sql =
                    "SELECT CountryID FROM MountainsCountries WHERE MountainID=@mid;";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@mid", mountainId);
                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                            ids.Add((int)r["CountryID"]);
                    }
                }
            }
            return ids;
        }

        public static void Save(int mountainId, List<int> countryIds)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();

                using (SqlCommand del = new SqlCommand(
                    "DELETE FROM MountainsCountries WHERE MountainID=@mid;", conn))
                {
                    del.Parameters.AddWithValue("@mid", mountainId);
                    del.ExecuteNonQuery();
                }

                foreach (var cid in countryIds)
                {
                    using (SqlCommand ins = new SqlCommand(
                        "INSERT INTO MountainsCountries(MountainID, CountryID) VALUES(@mid, @cid);", conn))
                    {
                        ins.Parameters.AddWithValue("@mid", mountainId);
                        ins.Parameters.AddWithValue("@cid", cid);
                        ins.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
