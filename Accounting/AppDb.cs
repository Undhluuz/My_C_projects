using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Accounting
{
    internal class AppDb
    {
        private OrmLiteConnectionFactory connection;

        public AppDb()
        {
            string filepath = Directory.GetCurrentDirectory() + "\\AccountingDb.db";
            connection = new OrmLiteConnectionFactory(filepath,SqliteDialect.Provider);
            using (var db = connection.Open())
            {
                db.CreateTableIfNotExists<InfoOperation>();
                if (db.Count<InfoOperation>() <= 0)
                {
                    CreateDataBase(db);
                }
            }
        }
        private void CreateDataBase(IDbConnection db)
        {
            var item = new InfoOperation { Id = 0, IdName = "Dm", Type = OperationType.Income, Ammount = 1000, Time = DateTime.Now };
            db.Insert<InfoOperation>(item);
        }

        public List<InfoOperation> GetInfoOperations()
        {
            List<InfoOperation> Opers = new List<InfoOperation>();
            using( var db = connection.Open())
            {
                Opers = db.Select<InfoOperation>().ToList();
            }
            return Opers;
        }
    }
}
