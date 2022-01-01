using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DataRepository : IDataRepository
    {
        private readonly IServiceProvider serviceProvider;
        private DataContext dataContext;

        public DataRepository(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task Create<T>(T entity) where T : class
        {
            await Set<T>().AddAsync(entity);
            dataContext.SaveChanges();
        }

        public Task<T> GetFirst<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return Set<T>().FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return Set<T>();
        }

        private DbSet<T> Set<T>() where T : class
        {
            if (dataContext == null)
            {
                dataContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
            }

            return dataContext.Set<T>();
        }
    }
}
