using RAK.Core.UI.Xam.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace RAK.Core.UI.Xam
{
    public class DataBase : IRAKXamarinDatabase
    {
        // -- Contstantes
        public const string SQL_QuerySelect = "select name from sqlite_master where name not like 'sqlite_%' and name not in (";
        public const string SQL_ExcludeTables = "'User')";
        public const string SQL_QueryDelete = "delete from ";
        public readonly SQLiteAsyncConnection database;

        public DataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
        }

        public Task<List<T>> GetListAsync<T>()
            where T : ModelBase, new()
        {
            return database.Table<T>().ToListAsync();
        }

        public Task<T> GetAsync<T>(long id)
            where T : ModelBase, new()
        {
            return database.Table<T>()
                .Where(x => x.EntityID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveAsync<T>(T entity)
            where T : ModelBase, new()
        {
            if (entity.EntityID == 0)
            {
                return database.InsertAsync(entity);

            }
            else
            {
                var existe = this.GetAsync<T>(entity.EntityID).Result;
                if (existe!=null)
                {
                    entity.LocalID = existe.LocalID;
                    return database.UpdateAsync(entity);
                }
                else
                {
                    return database.InsertAsync(entity);
                }
            }
        }

        public Task<int> DeleteAsync<T>(T entity)
            where T : ModelBase, new()
        {
            return database.DeleteAsync(entity);
        }


        public Task<int> DeleteAllAsync<T>()
            where T : ModelBase, new()
        {
            return database.DeleteAllAsync<T>();
        }

        public Task<List<T>> GetByCriteria<T> (Expression<Func<T, bool>> criteria)
        where T : ModelBase, new()
        {
            return database.Table<T>().Where(criteria).ToListAsync();
        }

        /// <summary>
        /// Recupera los nombres de todas las tablas menos las de sistema
        /// </summary>
        public List<Table> GetNameOfAllTables()
        {
            var SQLiteCommand = database.GetConnection().CreateCommand(SQL_QuerySelect + SQL_ExcludeTables, new object[] { });
            var result = SQLiteCommand.ExecuteQuery<Table>();
            return result;
        }

        /// <summary>
        /// Elimina las tablas de SQL Lite
        /// </summary>
        public void DeleteTables()
        {
            var tables = this.GetNameOfAllTables();
            foreach (var table in tables)
            {
                var SQLiteCommand = database.GetConnection().CreateCommand(SQL_QueryDelete + table.name, new object[] { });
                SQLiteCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Ejecuta una consulta particular
        /// </summary>
        /// <param name="query"></param>
        public void ExecuteQuery(string query)
        {
            var SQLiteCommand = database.GetConnection().CreateCommand(query);
            SQLiteCommand.ExecuteNonQuery();
        }
    } 

    public interface IRAKXamarinDatabase
    {
    }

    /// <summary>
    /// Representa a una tabla de SQLLite
    /// </summary>
    public class Table
    {
        public string name { get; set; }
    }

}
