using System.Linq;
using HotChocolate.Types;
using HotChocolateWithEfDemo.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HotChocolateWithEfDemo.GraphqlApi
{
    [ExtendObjectType("Query")]
    public class DevelopersQuery
    {
        public IQueryable<Developer> Developers([FromServices] ProjectsDbContext db,DeveloperSearchCriteria filter)
        {
            var query = db.Developers.AsQueryable();

            if (filter?.NameLike != null)
            {
                query = query.Where(d => d.Name.StartsWith(filter.NameLike));
            }

            if (filter!=null && filter.HourlyRateFrom.HasValue)
            {
                query = query.Where(d => d.HourlyRate >= filter.HourlyRateFrom.Value);
            }
            
            if (filter!=null && filter.HourlyRateTo.HasValue)
            {
                query = query.Where(d => d.HourlyRate <= filter.HourlyRateTo.Value);
            }
            
            return query;
        }
    }

    public class DeveloperSearchCriteria
    {
        public string NameLike { get; set; }
        public decimal? HourlyRateFrom  { get; set; }
        public decimal? HourlyRateTo  { get; set; }
    }
}