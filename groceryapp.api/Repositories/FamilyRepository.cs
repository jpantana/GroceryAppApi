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
        // FOR MAC
        string _connectionString = "Server=localhost;Database=GroceriesDb2;User Id=sa; Password=reallyStrongPwd123";
        // FOR PC
        // string _connectionString = "Server=localhost;Database=GroceriesDb2;Trusted_Connection=True;";

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
