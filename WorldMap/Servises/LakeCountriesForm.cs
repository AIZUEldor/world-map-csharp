using System.Collections.Generic;
using System.Data.SqlClient;
using WorldMap.Helpers;

namespace WorldMap.Services
{
    public class LakeCountriesService
    {
        public static List<int> GetCountryIdsByLake(int lakeId)
        {
            var ids = new List<int>();

            using (SqlConnection conn = DbHelper.GetConnection())
            {
                const string sql = "SELECT CountryID FROM LakesCountries WHERE LakeID=@lid;";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@lid", lakeId);
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

        public static void Save(int lakeId, List<int> countryIds)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();

                using (SqlCommand del = new SqlCommand("DELETE FROM LakesCountries WHERE LakeID=@lid;", conn))
                {
                    del.Parameters.AddWithValue("@lid", lakeId);
                    del.ExecuteNonQuery();
                }

                foreach (var cid in countryIds)
                {
                    using (SqlCommand ins = new SqlCommand(
                        "INSERT INTO LakesCountries(LakeID, CountryID) VALUES(@lid, @cid);", conn))
                    {
                        ins.Parameters.AddWithValue("@lid", lakeId);
                        ins.Parameters.AddWithValue("@cid", cid);
                        ins.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
