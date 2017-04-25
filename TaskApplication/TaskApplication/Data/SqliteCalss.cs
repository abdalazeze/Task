using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using TaskApplication.Models;

namespace TaskApplication.Data
{
    public class SqliteCalss
    {
        private SQLiteConnection sql_con;

        private SQLiteCommand sql_cmd;

        private SQLiteDataAdapter DB;

        private DataSet DS = new DataSet();

        public TaskModel Task { get; set; }

        public List<TaskModel> Tasks { get; set; }

        // Creates an empty database file
        public void createNewDatabase()
        {
            SQLiteConnection.CreateFile("Task.sqlite");
        }

        
        public void SetConnection()
        {
            sql_con = new SQLiteConnection
                ("Data Source=Task.sqlite;Version=3;New=False;Compress=True;");
        }
        public void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }        

        public List<TaskModel> LoadData()
        {
            var listOfTasks = new List<TaskModel>();
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "select * from TaskModels";
            using (SQLiteCommand cmd = new SQLiteCommand(CommandText, sql_con))
            {
                using (SQLiteDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        listOfTasks.Add(new TaskModel
                        {
                            oldId = int.Parse(rdr["ID"].ToString()),
                            name = rdr["name"].ToString(),
                            desc = rdr["desc"].ToString(),
                        });
                    }
                    rdr.Close();
                    Tasks = listOfTasks;
                    return Tasks;
                }
            }
            
        }

        // Creates a table named 'task' with two columns: name (a string of max 20 characters) and desc (an text)
        public void createTable()
        {
            string createTableQuery = @"CREATE TABLE IF NOT EXISTS [TaskModels] (
                          [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                          [name] NVARCHAR(2048)  NULL,
                          [desc] VARCHAR(2048)  NULL
                          )";

            ExecuteQuery(createTableQuery);
        }

        // Inserts some values in the task table.
        public void fillTable(string name, string desc)
        {
            string sql = "insert into TaskModels (name, desc) values ('"+ name + "','"+ desc +"')";
            ExecuteQuery(sql);
        }

        public TaskModel findrow(int? id)
        {
            var _Tasks = new TaskModel();
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string sql = "select * from TaskModels WHERE ID ='" + id + "'";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, sql_con))
            {
                using (SQLiteDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        _Tasks.oldId = int.Parse(rdr["ID"].ToString());
                        _Tasks.name = rdr["name"].ToString();
                        _Tasks.desc = rdr["desc"].ToString();                        
                    }
                    rdr.Close();
                    Task = _Tasks;
                    return Task;
                }
            }
        }

        public void editTable(string name, string desc, int id)
        {
            string sql = "UPDATE TaskModels SET name='"+ name +"', desc='"+ desc +"' WHERE ID ='"+ id +"'";
            ExecuteQuery(sql);
        }

        public void deleterowTable(int id)
        {
            string sql = "DELETE FROM TaskModels WHERE ID ='" + id + "'";
            ExecuteQuery(sql);
        }
    }
}