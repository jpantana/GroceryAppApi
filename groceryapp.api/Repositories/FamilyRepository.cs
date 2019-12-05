using Dapper;
using groceryapp.api.DataModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace groceryapp.api.Repositories
{
    public class FamilyRepository : IFamilyRepository
    {
        string _connectionString = "Server=localhost;Database=GroceriesDb;Trusted_Connection=True;";

        public IEnumerable<Family> GetAllFamily()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var families = db.Query<Family>("SELECT * FROM Family");

                return families;
            }
        }
    }
}
