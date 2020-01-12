using Dapper;
using groceryapp.api.Commands;
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
        // string _connectionString = "Server=localhost;Database=GroceriesDb2;Trusted_Connection=True;";
        string _connectionString = "Server=tcp:groceryappserver.database.windows.net,1433;Initial Catalog=GroceryDb;Persist Security Info=False;User ID=jpantana;Password=GroceryAppPwd1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public IEnumerable<Family> GetAllFamily()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var families = db.Query<Family>("SELECT * FROM Family");

                return families;
            }
        }

        public Family Add(CreateFamilyCommand newFamilyCommand)
        {

            using (var db = new SqlConnection(_connectionString))
            {
                string sqlTimeAsString = newFamilyCommand.DateCreated.ToString("yyyy-MM-ddTHH:mm:ss.fff");

                db.Open();

                var sql = @"
                        INSERT INTO [Family]
                                    ([Id]
                                    ,[Name]
                                    ,[DateCreated]
                                    )
	                        OUTPUT inserted.*
                                VALUES
                                    (@id
                                    ,@name
                                    ,@dateCreated
                                    )";

                return db.QueryFirst<Family>(sql, newFamilyCommand);
            }
        }

        public IEnumerable<Family> GetSingleFamily(Guid familyId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var sql = @"Select * From [Family] Where [Family].Id = @familyId";

                var family = db.Query<Family>(sql, new { familyId });

                return family;
            }
        }

    }
}
