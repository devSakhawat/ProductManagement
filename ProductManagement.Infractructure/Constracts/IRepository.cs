using System.Linq.Expressions;

namespace ProductManagement.DAL.Constracts
{/// <summary>
 /// Contains signatures of all generic methods.
 /// </summary>
 /// <typeparam name="T">T is a model class.</typeparam>
   public interface IRepository<T>
   {
      // Object to be saved int the table as a row.
      T Add(T entity);

      // Returns a row from the table as an object if primary key matches.
      T Get(int id);

      // Returns a row from the table as an object if primary key matches.
      T Get(long id);

      //Returns matched rows as a list of objects.
      IEnumerable<T> GetAll();

      //Returns matched rows as a list of objects.
      Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate);

      //Returns matched rows as a list of objects.
      Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> obj);

      // Returns first matched row as an object from the table.
      Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> obj, Expression<Func<T, object>> next);

      // Returns first matched row as an object from the table.
      Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

      // Updates an existing row in the table.
      void Update(T entity);

      // Deletes a row from the table.
      void Delete(T entity);
   }
}
