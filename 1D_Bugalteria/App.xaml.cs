using System.Windows;
using SimpleAccounting.DataAccess;
using SimpleAccounting.DataAccess.Models;
using System.Linq;
using System.Data.SQLite;

namespace SimpleAccounting
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Код для проверки подключения к базе данных и ее инициализации
            // Создаем базу данных (если она не существует)
            CreateDatabase();

            //Код для проверки подключения к базе данных и ее инициализации
            using (var db = new AppDbContext())
            {
                // Если база данных не существует, она будет создана при первом обращении к ней
                //db.Database.Initialize(force: false);  // Теперь это не нужно, так как база данных создается вручную

                // Добавьте тестовые данные (если нужно)
                if (!db.Counterparties.Any())
                {
                    db.Counterparties.Add(new Counterparty { Name = "Тестовая организация", TaxId = "123" });
                    db.SaveChanges();
                }
            }
        }
        public static void CreateDatabase()
        {
            string dbPath = "1D_Bugalteria.db"; // Путь к файлу базы данных

            // Проверяем, существует ли файл базы данных
            if (!System.IO.File.Exists(dbPath))
            {
                // Создаем файл базы данных
                SQLiteConnection.CreateFile(dbPath);

                // Открываем соединение с базой данных
                using (SQLiteConnection dbConnection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    dbConnection.Open();

                    // Создаем таблицы (пример)
                    string sql = "CREATE TABLE IF NOT EXISTS Operations (Id INTEGER PRIMARY KEY AUTOINCREMENT, Date DATETIME, Amount DECIMAL, OperationType INTEGER, CounterpartyId INTEGER, CategoryId INTEGER, Description TEXT)";
                    SQLiteCommand createTableCommand = new SQLiteCommand(sql, dbConnection);
                    createTableCommand.ExecuteNonQuery();

                    // Закрываем соединение
                    dbConnection.Close();
                }
            }

        }

    }
}
