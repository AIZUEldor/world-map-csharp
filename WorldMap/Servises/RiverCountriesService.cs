using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WorldMap.Helpers;

namespace WorldMap.Services
{
    public class RiverCountriesService
    {
        public static List<int> GetCountryIdsByRiver(int riverId)
        {
            var ids = new List<int>();

            using (SqlConnection conn = DbHelper.GetConnection())
            {
                const string sql = "SELECT CountryID FROM RiversCountries WHERE RiverID=@rid;";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@rid", riverId);
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

        public static void Save(int riverId, List<int> countryIds)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();

                using (SqlCommand del = new SqlCommand("DELETE FROM RiversCountries WHERE RiverID=@rid;", conn))
                {
                    del.Parameters.AddWithValue("@rid", riverId);
                    del.ExecuteNonQuery();
                }

                foreach (var cid in countryIds)
                {
                    using (SqlCommand ins = new SqlCommand(
                        "INSERT INTO RiversCountries(RiverID, CountryID) VALUES(@rid, @cid);", conn))
                    {
                        ins.Parameters.AddWithValue("@rid", riverId);
                        ins.Parameters.AddWithValue("@cid", cid);
                        ins.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
