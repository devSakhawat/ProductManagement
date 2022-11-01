using Microsoft.EntityFrameworkCore;
using ProductManagement.DAL.Constracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DAL.Repositories
{
   public class Repository<T> : IRepository<T> where T : class
   {
      protected readonly TonerContext context;
      public Repository(TonerContext context)
      {
         this.context = context;
      }

      public T Add(T entity)
      {
         try
         {
            return context.Set<T>().Add(entity).Entity;
         }
         catch (Exception)
         {
            throw;
         }
      }

      public virtual T Get(int id)
      {
         try
         {
            return context.Set<T>().Find(id);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public virtual T Get(long id)
      {
         try
         {
            return context.Set<T>().Find();
         }
         catch (Exception)
         {
            throw;
         }
      }

      public IEnumerable<T> GetAll()
      {
         try
         {
            return context.Set<T>().AsQueryable().AsNoTracking().ToList();
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate)
      {
         try
         {
            return await context.Set<T>().AsQueryable().AsNoTracking().Where(predicate).ToListAsync();
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> obj)
      {
         try
         {
            return await context.Set<T>().AsQueryable().Where(predicate).Include(obj).ToListAsync();
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> obj, Expression<Func<T, object>> next)
      {
         try
         {
            return await context.Set<T>().AsQueryable().Where(predicate).Include(obj).Include(next).ToListAsync();
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
      {
         try
         {
            return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public void Update(T entity)
      {
         try
         {
            context.Entry(entity).State = EntityState.Modified;
            context.Set<T>().Update(entity);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public void Delete(T entity)
      {
         try
         {
            context.Entry(entity).State = EntityState.Modified;
            context.Set<T>().Remove(entity);
         }
         catch (Exception)
         {
            throw;
         }
      }
   }
}