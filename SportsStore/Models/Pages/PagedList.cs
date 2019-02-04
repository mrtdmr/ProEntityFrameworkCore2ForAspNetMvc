using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SportsStore.Models.Pages
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IQueryable<T> query, QueryOption option = null)
        {
            PageSize = option.PageSize;
            Options = option;
            if (option != null)
            {
                if (!string.IsNullOrEmpty(option.OrderPropertyName))
                {
                    query = Order(query, option.OrderPropertyName,
                    option.DescendingOrder);
                }
                if (!string.IsNullOrEmpty(option.SearchPropertyName)
&& !string.IsNullOrEmpty(option.SearchTerm))
                {
                    query = Search(query, option.SearchPropertyName,
                    option.SearchTerm);
                }
            }
            var tPage = query.Count() / PageSize;
            TotalPage = tPage < 1 ? 1 : tPage;
            CurrentPage = option.CurrentPage < 1 ? 1 : option.CurrentPage > TotalPage ? TotalPage : option.CurrentPage;
            AddRange(query.Skip((CurrentPage - 1) * PageSize).Take(PageSize));
        }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public QueryOption Options { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPage;
        private static IQueryable<T> Search(IQueryable<T> query, string propertyName, string searchTerm)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var source = propertyName.Split('.').Aggregate((Expression)parameter, Expression.Property);
            var body = Expression.Call(source, "Contains", Type.EmptyTypes, Expression.Constant(searchTerm, typeof(string)));
            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
            return query.Where(lambda);
        }
        private static IQueryable<T> Order(IQueryable<T> query, string propertyName,
bool desc)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var source = propertyName.Split('.').Aggregate((Expression)parameter,
            Expression.Property);
            var lambda = Expression.Lambda(typeof(Func<,>).MakeGenericType(typeof(T),
            source.Type), source, parameter);
            return typeof(Queryable).GetMethods().Single(
            method => method.Name == (desc ? "OrderByDescending"
            : "OrderBy")
            && method.IsGenericMethodDefinition
            && method.GetGenericArguments().Length == 2
            && method.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(T), source.Type)
            .Invoke(null, new object[] { query, lambda }) as IQueryable<T>;
        }
    }
}
