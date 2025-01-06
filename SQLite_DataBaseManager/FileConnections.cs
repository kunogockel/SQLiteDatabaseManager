// FileConnections.cs
// Verwaltet Datenbankverbindungen in einer Konfigurationsdatei
// 2025-01-06 KG Ueberarbeitet
using System;
using System.Collections.Generic;
using System.IO;

namespace SQLite_DataBaseManager
{
    // Verwaltet Datenbankverbindungen in einer Konfigurationsdatei
    public class FileConnections
    {
        public List<DatabaseConnection> connections = new List<DatabaseConnection>();
        public string fileLocation = Directory.GetCurrentDirectory() + "\\Databases.txt";

        public FileConnections()
        {
            connections = getConnections();
        }

        //public void addConnection(string name, string path, string password)
        //{
        //    if (!connectionExists(name, path))
        //    {
        //        connections.Add(new DatabaseConnection()
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = name,
        //            Path = path,
        //            Password = password
        //        });
        //        setConnections();
        //    }
        //    else
        //    {
        //        throw new Exception("Connection already exists.");
        //    }
        //}

        public void addConnection(string name, string path)
        {
            if (!connectionExists(name, path))
            {
                connections.Add(new DatabaseConnection()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Path = path,
                    Password = ""
                });
                setConnections();
            }
            else
            {
                throw new Exception("Connection already exists.");
            }
        }

        private bool connectionExists(string name, string path)
        {
            foreach (var connection in connections)
            {
                if (connection.Name == name && connection.Path == path)
                {
                    return true;
                }
            }
            return false;
        }

        public void editConnection(Guid id, string name, string path, string password)
        {
            foreach (var connection in connections)
            {
                if (connection.Id == id)
                {
                    connection.Name = name;
                    connection.Path = path;
                    connection.Password = password;
                    break;
                }
            }
        }

        public bool removeConnection(Guid id)
        {
            int itemToRemove = -1;
            for (int i = 0; i < connections.Count; i++)
            {
                if (connections[i].Id == id)
                {
                    itemToRemove = i;
                    break;
                }
            }
            if (itemToRemove != -1)
            {
                connections.RemoveAt(itemToRemove);
                return true;
            }
            return false;
        }

        // Liest die gespeicherten Datenbankverbindungen aus der Konfigurationsdatei
        public List<DatabaseConnection> getConnections()
        {
            connections.Clear();
            if (File.Exists(fileLocation))
            {
                string[] lines = File.ReadAllLines(fileLocation);
                foreach (var line in lines)
                {
                    string[] connectionProperties = line.Split(',');
                    if (connectionProperties.Length > 1)
                    {
                        DatabaseConnection dbConnection = new DatabaseConnection();
                        dbConnection.Name = connectionProperties[0];
                        dbConnection.Path = connectionProperties[1];
                        dbConnection.Password = connectionProperties[2];
                        connections.Add(dbConnection);
                    }
                }
            }
            return connections;
        }

        //Write-append to a CSV File
        //public bool saveConnection(string name, string path)
        //{
        //    if (!File.Exists(fileLocation))
        //    {
        //        File.WriteAllText(fileLocation, name + "," + path + Environment.NewLine);
        //    }
        //    else
        //    {
        //        File.AppendAllText(fileLocation, name + "," + path + Environment.NewLine);
        //    }
        //    return true;
        //}

        public void setConnections()
        {
            File.WriteAllText(fileLocation, "");
            foreach (var connection in connections)
            {
                File.AppendAllText(fileLocation, 
                    connection.Name + "," + connection.Path + "," + connection.Password + Environment.NewLine);
            }
        }
    }
}
