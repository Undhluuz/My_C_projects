using System.Data.Entity;
using System.IO;
using SimpleAccounting.DataAccess.Models;

namespace SimpleAccounting.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=SimpleAccountingDB")
        {
            if (!File.Exists("1D_Bugalteria.db"))
            {
                // База данных не существует, создаем ее (или выполняем другие действия)
                Database.SetInitializer(new CreateDatabaseIfNotExists<AppDbContext>());
                Database.Initialize(force: true);
            }
        }

        public DbSet<Operation> Operations { get; set; }
        public DbSet<Counterparty> Counterparties { get; set; }
        public DbSet<IncomeExpenseCategory> IncomeExpenseCategories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Если база данных SQLite не существует, создайте ее
            //var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}