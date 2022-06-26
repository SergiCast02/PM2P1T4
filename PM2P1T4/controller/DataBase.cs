using PM2P1T4.models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PM2P1T4.Controller
{
    public class DataBase
    {
        readonly SQLiteAsyncConnection dbase;
        /* Constructor de clase */
        public DataBase(string dbpath)
        {
            dbase = new SQLiteAsyncConnection(dbpath);
            dbase.CreateTableAsync<persona>();
        }

        #region Operaciones
        // Create
        public Task<int> PersonSave(persona person)
        {
            return dbase.InsertAsync(person);
        }

        // Read
        public Task<List<persona>> obtenerListaPersonas()
        {
            return dbase.Table<persona>().ToListAsync();
        }

        // Read V2
        public Task<persona> obtenerSitio(int pid)
        {
            return dbase.Table<persona>()
                .Where(i => i.id == pid)
                .FirstOrDefaultAsync();
        }

        #endregion Operaciones
    }
}
