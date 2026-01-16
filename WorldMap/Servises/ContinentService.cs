using System.Collections.Generic;
using System.Data.SqlClient;
using WorldMap.Helpers;
using WorldMap.Models;

namespace WorldMap.Services
{
    public class ContinentService
    {
        public static List<Continent> GetAll()
        {
            var list = new List<Continent>();

            using (SqlConnection conn = DbHelper.GetConnection())
            {
                string sql = "SELECT ContinentID, Name FROM Continents ORDER BY Name";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Continent
                            {
                                ContinentID = (int)r["ContinentID"],
                                Name = r["Name"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
