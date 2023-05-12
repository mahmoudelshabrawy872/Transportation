using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportationAPI.Data;

namespace TransportationAPI.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class SearchController : ControllerBase
    {
        [HttpGet]

        public List<object> SearchAllDbSets(ApplicationDbContext dbContext, string searchWord)
        {
            List<string> tableNames = new()
            {
                "Cars",
                "Drivers",
            };
            List<object> results = new List<object>();

            // Get all DbSet properties in the DbContext that match the specified table names
            var dbSetProperties = dbContext.GetType().GetProperties()
                .Where(p => p.PropertyType.IsGenericType
                            && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)
                            && tableNames.Contains(p.Name));

            // Loop through each DbSet and retrieve all records
            foreach (var dbSetProperty in dbSetProperties)
            {
                var dbSet = dbSetProperty.GetValue(dbContext);
                var records = ((IEnumerable<object>)dbSet).ToList();

                // Use LINQ to filter records based on search word using LIKE operator
                var filteredRecords = records.Where(record =>
                {
                    var recordProperties = record.GetType().GetProperties();
                    foreach (var property in recordProperties)
                    {
                        if (property.PropertyType == typeof(string))
                        {
                            var value = property.GetValue(record) as string;
                            if (!string.IsNullOrEmpty(value) && value.ToLower().Contains(searchWord.ToLower()))
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                });

                results.AddRange(filteredRecords);
            }

            return results;
        }
    }
}
