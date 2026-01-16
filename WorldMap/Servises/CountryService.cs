using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WorldMap.Helpers;
using WorldMap.Models;

namespace WorldMap.Services
{
    public class CountryService
    {

        public static List<Country> GetAllForCombo()
        {
            var list = new List<Country>();
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                const string sql = "SELECT CountryID, Name FROM Countries ORDER BY Name;";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Country
                            {
                                CountryID = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
            }
            return list;
        }
        public static DataTable GetAll()
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                const string sql = @"
SELECT c.CountryID, c.Name, c.ISOCode, ct.Name AS Continent, c.Currency, c.Population, c.AreaKm2
FROM Countries c
JOIN Continents ct ON ct.ContinentID = c.ContinentID
ORDER BY c.Name;";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static void Add(string name, string iso, int continentId, string currency, long? population, double? areaKm2)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                const string sql = @"
INSERT INTO Countries(Name, ISOCode, ContinentID, Currency, Population, AreaKm2)
VALUES(@name, @iso, @cid, @cur, @pop, @area);";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar, 150).Value = name;
                    cmd.Parameters.Add("@iso", SqlDbType.Char, 3).Value = iso;
                    cmd.Parameters.Add("@cid", SqlDbType.Int).Value = continentId;
                    cmd.Parameters.Add("@cur", SqlDbType.NVarChar, 50).Value = (object)currency ?? DBNull.Value;
                    cmd.Parameters.Add("@pop", SqlDbType.BigInt).Value = (object)population ?? DBNull.Value;
                    cmd.Parameters.Add("@area", SqlDbType.Float).Value = (object)areaKm2 ?? DBNull.Value;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Update(int id, string name, string iso, int continentId, string currency, long? population, double? areaKm2)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                const string sql = @"
UPDATE Countries
SET Name=@name, ISOCode=@iso, ContinentID=@cid, Currency=@cur, Population=@pop, AreaKm2=@area
WHERE CountryID=@id;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar, 150).Value = name;
                    cmd.Parameters.Add("@iso", SqlDbType.Char, 3).Value = iso;
                    cmd.Parameters.Add("@cid", SqlDbType.Int).Value = continentId;
                    cmd.Parameters.Add("@cur", SqlDbType.NVarChar, 50).Value = (object)currency ?? DBNull.Value;
                    cmd.Parameters.Add("@pop", SqlDbType.BigInt).Value = (object)population ?? DBNull.Value;
                    cmd.Parameters.Add("@area", SqlDbType.Float).Value = (object)areaKm2 ?? DBNull.Value;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(int id)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                const string sql = "DELETE FROM Countries WHERE CountryID=@id;";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
