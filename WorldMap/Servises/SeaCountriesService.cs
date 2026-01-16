using System.Collections.Generic;
using System.Data.SqlClient;
using WorldMap.Helpers;

namespace WorldMap.Services
{
    public class SeaCountriesService
    {
        public static List<int> GetCountryIdsBySea(int seaId)
        {
            var ids = new List<int>();

            using (SqlConnection conn = DbHelper.GetConnection())
            {
                const string sql = "SELECT CountryID FROM SeasCountries WHERE SeaID=@sid;";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@sid", seaId);
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

        public static void Save(int seaId, List<int> countryIds)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();

                using (SqlCommand del = new SqlCommand("DELETE FROM SeasCountries WHERE SeaID=@sid;", conn))
                {
                    del.Parameters.AddWithValue("@sid", seaId);
                    del.ExecuteNonQuery();
                }

                foreach (var cid in countryIds)
                {
                    using (SqlCommand ins = new SqlCommand(
                        "INSERT INTO SeasCountries(SeaID, CountryID) VALUES(@sid, @cid);", conn))
                    {
                        ins.Parameters.AddWithValue("@sid", seaId);
                        ins.Parameters.AddWithValue("@cid", cid);
                        ins.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
