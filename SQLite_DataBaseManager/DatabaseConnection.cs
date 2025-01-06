using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite_DataBaseManager
{
    // Repraesentiert eine SQLite-Datenbank
    public class DatabaseConnection
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Password { get; set; }
        public FileInfo fileInfo { get; set; }

        public DatabaseConnection()
        {

        }

        public DatabaseConnection(string fileName)
        {
            fileInfo = new FileInfo(fileName);
        }
    }
}
