using Advertising.Adverts;
using Advertising.Application.Dto;
using Advertising.Domain;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advertising.Data.Repositories
{
    public class AdvertRepository : RepositoryBase<Advert>, IAdvertRepository
    {
        public AdvertRepository(IDataContext dbContext) : base(dbContext)
        {

        }

        public async Task<PagingResultDto<Advert>> GetAll(int? categoryId, decimal? minPrice, decimal? maxPrice, string gear, string fuel, string sort, string sortType, int pageSize, int page)
        {
            if (string.IsNullOrEmpty(sort)) sort = "date";
            if (string.IsNullOrEmpty(sortType)) sortType = "desc";

            var builder = new SqlBuilder();
            builder.AddParameters(new { limit = pageSize, offset = (page - 1) * pageSize });
            builder.OrderBy($"{sort} {sortType}");

            var filters = new Dictionary<string, object>();

            if (minPrice.HasValue && maxPrice.HasValue && minPrice != default(decimal) && maxPrice != default(decimal))
                filters.Add("price >= @minPrice AND price <= @maxPrice", new { minPrice, maxPrice });
            else if (minPrice.HasValue && minPrice != default(decimal))
                filters.Add("price >= @minPrice", new { minPrice });
            else if (maxPrice.HasValue && maxPrice != default(decimal))
                filters.Add("price <= @maxPrice", new { minPrice, maxPrice });

            if (categoryId.HasValue && categoryId != default(int))
                filters.Add($"category_id = @categoryId", new { categoryId });

            if (!string.IsNullOrEmpty(gear))
                filters.Add($"gear = @gear", new { gear });

            if (!string.IsNullOrEmpty(fuel))
                filters.Add($"fuel = @fuel", new { fuel });

            SqlBuilder.Template sqlTemplate = null;
            if (filters.Count > 0)
            {
                foreach (var filter in filters)
                    builder.Where(filter.Key, filter.Value);

                sqlTemplate = builder.AddTemplate("SELECT COUNT(id) FROM adverts /**where**/; SELECT * FROM adverts /**where**/ /**orderby**/ LIMIT @limit OFFSET @offset;");
            }
            else
            {
                sqlTemplate = builder.AddTemplate("SELECT COUNT(id) FROM adverts; SELECT * FROM adverts /**orderby**/ LIMIT @limit OFFSET @offset;");
            }

            var queryTask = DbContext.Connection.QueryMultipleAsync(sqlTemplate.RawSql, sqlTemplate.Parameters);
            var reader = await queryTask;

            var totalCount = reader.Read<int>().Single();
            var data = reader.Read<Advert>().ToList();

            return new PagingResultDto<Advert>(pageSize, page, totalCount, data);
        }

        public async Task<Advert> GetById(int id)
        {
            var sql = "SELECT * FROM adverts WHERE id = @id";
            return await QueryFirstOrDefaultAsync<Advert>(sql, new { id });
        }
    }
}
