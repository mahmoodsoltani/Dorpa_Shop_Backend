using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Ecommerce.Infrastructure.src.Database;
using Ecommerce.Model.src;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Repository;
using Ecommerce.Model.src.Shared.ValueObject;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.src.Repository.Shared
{
    public class BaseRepo<T> : IBaseRepo<T>
        where T : BaseEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepo(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<T> CreateAsync(
            T entity,
            Expression<Func<T, bool>>? checkDuplicate = null,
            Expression<Func<T, object>>[] includes = null
        )
        {
            if (checkDuplicate != null)
            {
                IQueryable<T> query = _dbSet;
                var result = await query.AnyAsync(checkDuplicate);
                if (result)
                {
                    throw new DuplicateEntityException();
                }
            }
            ArgumentNullException.ThrowIfNull(entity);
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            var addedEntity = await GetAsync(e => e.Id == entity.Id, includes, false);
            return addedEntity;
        }

        public virtual async Task<bool> DeleteByIdAsync(int id)
        {
            var entity =
                await _dbSet.FindAsync(id) ?? throw new EntityNotFoundException(typeof(T).Name, id);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>>? filter)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (query.ToList().Count > 0)
                return true;
            return false;
        }

        public async Task<PaginatedResult<T>> GetAllAsync(
            QueryOptions queryOptions,
            Expression<Func<T, bool>> filter = null,
            Expression<Func<T, object>>[] includes = null,
            bool tracked = true
        )
        {
            IQueryable<T> query = _dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            query = ApplyFiltering(query, queryOptions.SearchBy, queryOptions.SearchTerm);
            query = ApplySorting(query, queryOptions.SortBy, queryOptions.IsAscending);
            var allEntities = await query.ToListAsync();
            query = ApplyPaging(
                query,
                queryOptions.PageNumber ?? 1,
                queryOptions.PageSize ?? allEntities.Count
            );

            var entities = await query.ToListAsync();

            return new PaginatedResult<T>
            {
                Items = entities,
                TotalCount = allEntities.Count,
                PageNumber = queryOptions.PageNumber ?? 1,
                PageSize = queryOptions.PageSize ?? allEntities.Count
            };
        }

        public async Task<T> GetAsync(
            Expression<Func<T, bool>> filter = null,
            Expression<Func<T, object>>[] includes = null,
            bool tracked = true
        )
        {
            IQueryable<T> query = _dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<T> UpdateAsync(
            T entity,
            Expression<Func<T, bool>>? checkDuplicate = null
        )
        {
             if (checkDuplicate != null)
            {
                IQueryable<T> query = _dbSet;
                var result = await query.AnyAsync(checkDuplicate);
                if (result)
                {
                    throw new DuplicateEntityException();
                }
            }
            ArgumentNullException.ThrowIfNull(entity);
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            var UpdatedEntity = await GetAsync(e => e.Id == entity.Id, null, false);
            return UpdatedEntity;
        }

        private IQueryable<T> ApplyFiltering(IQueryable<T> query, string columnName, object value)
        {
            try
            {
                if (!string.IsNullOrEmpty(columnName) && value != null)
                {
                    var property = typeof(T).GetProperty(
                        columnName,
                        BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase
                    );
                    if (property != null)
                    {
                        var parameter = Expression.Parameter(typeof(T), "e");
                        var propertyAccess = Expression.Property(parameter, property);

                        Expression condition;

                        if (property.PropertyType == typeof(string))
                        {
                            var toLowerMethod = typeof(string).GetMethod(
                                "ToLower",
                                Type.EmptyTypes
                            );
                            var propertyToLower = Expression.Call(propertyAccess, toLowerMethod);
                            var constant = Expression.Constant(value.ToString().ToLower());
                            var containsMethod = typeof(string).GetMethod(
                                "Contains",
                                new[] { typeof(string) }
                            );
                            condition = Expression.Call(propertyToLower, containsMethod, constant);
                        }
                        else if (
                            property.PropertyType == typeof(int)
                            || property.PropertyType == typeof(decimal)
                        )
                        {
                            var constant = Expression.Constant(
                                Convert.ChangeType(value, property.PropertyType)
                            );
                            condition = Expression.Equal(propertyAccess, constant);
                        }
                        else if (property.PropertyType == typeof(bool))
                        {
                            var constant = Expression.Constant(Convert.ToBoolean(value));
                            condition = Expression.Equal(propertyAccess, constant);
                        }
                        else if (property.PropertyType == typeof(DateTime))
                        {
                            var constant = Expression.Constant(
                                Convert.ToDateTime(value).Kind == DateTimeKind.Utc
                                    ? value
                                    : DateTime
                                        .SpecifyKind(Convert.ToDateTime(value), DateTimeKind.Utc)
                                        .ToUniversalTime()
                            );
                            condition = Expression.Equal(propertyAccess, constant);
                        }
                        else
                        {
                            throw new InvalidQueryOptionException(
                                $"Search not supported for type {property.PropertyType}"
                            );
                        }

                        var lambda = Expression.Lambda<Func<T, bool>>(condition, parameter);
                        return query.Where(lambda);
                    }
                    else
                    {
                        throw new InvalidQueryOptionException(
                            $"Invalid search property's name {columnName}"
                        );
                    }
                }
                return query;
            }
            catch
            {
                throw new InvalidQueryOptionException(
                    $"Invalid Search options. ({columnName}:{value})"
                );
            }
        }

        private IQueryable<T> ApplySorting(IQueryable<T> query, string sortColumn, bool isAscending)
        {
            if (!string.IsNullOrEmpty(sortColumn))
            {
                var sortProperty = typeof(T).GetProperty(
                    sortColumn,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase
                );
                if (sortProperty != null)
                {
                    var parameter = Expression.Parameter(typeof(T), "e");
                    var propertyAccess = Expression.Property(parameter, sortProperty);
                    var lambda = Expression.Lambda<Func<T, object>>(
                        Expression.Convert(propertyAccess, typeof(object)),
                        parameter
                    );

                    query = isAscending ? query.OrderBy(lambda) : query.OrderByDescending(lambda);
                }
                else
                {
                    throw new InvalidQueryOptionException(
                        $"Invalid sort property's name {sortColumn}  {sortProperty}"
                    );
                }
            }
            query.OrderBy(u => u.Id);
            return query;
        }

        private IQueryable<T> ApplyPaging(IQueryable<T> query, int pageNumber, int pageSize)
        {
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
