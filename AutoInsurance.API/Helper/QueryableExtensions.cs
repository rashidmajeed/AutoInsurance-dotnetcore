using System.Linq;
using AutoInsurance.API.DTOs;

namespace AutoInsurance.API.Helper
{
    public static class QueryableExtensions
    {
         public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO pagination)
        {
            return queryable
                .Skip((pagination.Page - 1) * pagination.RecordsPerPage)
                .Take(pagination.RecordsPerPage);
        }
    }
}