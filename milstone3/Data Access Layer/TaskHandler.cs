using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Data.SQLite;
using System.Data.OleDb;

namespace milstone3
{
    [Serializable]
    public class DatTask
    {
        private string boardId;
        private int taskId;
        private String title;
        private String description;
        private String username;
        private String dueDate;
        private String creationTime;
        private string columnId;

        public DatTask(string boardId , int taskId,string title, string description, string username, string dueDate, string creationTime,string columnId)
        {
            this.boardId = boardId;
            this.taskId = taskId;
            this.title = title;
            this.description = description;
            this.username = username;
            this.dueDate = dueDate;
            this.creationTime = creationTime;
            this.columnId = columnId;

        }
        public string getTitle()
        {
            return title;
        }
        public int gettaskId()
        {
            return this.taskId;
        }
        public void setColumnId(string columnId)
        {
            this.columnId = columnId;
        } 
        public string getdescription()
        {
            return this.description;
        }
        public string getusername()
        {
            return this.username;
        }
        public string getdueDate()
        {
            return this.dueDate;
        }
        
        public string getcreationTime()
        {
            return this.creationTime;
        }
        public string getColumnId()
        {
            return this.columnId;
        }
       
        public void editTask(string title,string description, string duedate)
        {
            this.title = title;
            this.description = description;
            this.dueDate = duedate;
        }

        internal string getBoardId()
        {
            return this.boardId;
        }
    }

    class TaskHandler
    {
        static log4net.ILog log = ILog.getlogger();

        public static List<DatTask> getTasks()
        {
            string connetion_string = null;

            string database_name = "KanbanDataBase.db";

            SQLiteConnection connection;


            connetion_string = $"Data Source={database_name};Version=3;";
            connection = new SQLiteConnection(connetion_string);


            try
            {

                connection.Open();

                string sql = "select * from Tasks";
                SQLiteCommand c = new SQLiteCommand(sql, connection);
                SQLiteDataReader reader = c.ExecuteReader();
                List<DatTask> output = new List<DatTask>();
                while (reader.Read())
                {
                    Object o = reader["TaskId"];
                    DatTask dat = new DatTask((string)reader["BoardId"], Convert.ToInt32(o), (string)reader["Title"], (string)reader["Description"], (string)reader["Username"], (string)reader["Duedate"], (string)reader["CreationDate"], (string)reader["ColumnId"]);
                    output.Add(dat);
                }
                connection.Close();
                
                return output;

            }
            catch (Exception e)
            {
                log.Error("problem with extract from database");
                return null;
            }
        }
   
        
          

        public static bool addTasks(string boardId, int taskId, string title, string description, string username, string dueDate, string creationTime, string columnId)
        {
            string connetion_string = null;
            string sql_query = null;
            string database_name = "KanbanDataBase.db";



            SQLiteConnection connection;
            SQLiteCommand command;

            connetion_string = $"Data Source={database_name};Version=3;";
            connection = new SQLiteConnection(connetion_string);
            SQLiteDataReader dataReader;
            
            try
            {
                connection.Open();

                command = new SQLiteCommand(null, connection);

                // Create and prepare an SQL statement.
                // Use should never use something like: query = "insert into table values(" + value + ");" 
                // Especially when selecting. More about it on the lab about security.
                command.CommandText =
                    "INSERT INTO Tasks (TaskId,ColumnId,BoardId,Username,Title,Description,Duedate,CreationDate) " +
                    "VALUES (@TaskId, @ColumnId , @BoardId ,@Username,@Title, @Description , @Duedate ,@CreationDate)";
                SQLiteParameter TaskId_param = new SQLiteParameter(@"TaskId", taskId.ToString());
                SQLiteParameter ColumnId_param = new SQLiteParameter(@"ColumnId", columnId);
                SQLiteParameter BoardId_param = new SQLiteParameter(@"BoardId", boardId);
                SQLiteParameter Username_param = new SQLiteParameter(@"Username", username);
                SQLiteParameter Title_param = new SQLiteParameter(@"Title", title);
                SQLiteParameter Description_param = new SQLiteParameter(@"Description", description);
                SQLiteParameter Duedate_param = new SQLiteParameter(@"Duedate", dueDate);
                SQLiteParameter CreationDate_param = new SQLiteParameter(@"CreationDate", creationTime);

                command.Parameters.Add(TaskId_param);
                command.Parameters.Add(ColumnId_param);
                command.Parameters.Add(BoardId_param);
                command.Parameters.Add(Username_param);
                command.Parameters.Add(Title_param);
                command.Parameters.Add(Description_param);
                command.Parameters.Add(Duedate_param);
                command.Parameters.Add(CreationDate_param);

                // Call Prepare after setting the Commandtext and Parameters.
                command.Prepare();
                int num_rows_changed = command.ExecuteNonQuery();
                command.Dispose();
                return true;
            }

            
            catch (Exception ex)
            {
                log.Error("problem with adding task to the database");
                connection.Close();
                return false;
            }
        }

        public static bool deleteTask(int taskId,string columnId, string boardId, string username)
        {

            string connetion_string = null;

            string database_name = "KanbanDataBase.db";

            SQLiteConnection connection;
            SQLiteCommand command;

            connetion_string = $"Data Source={database_name};Version=3;";
            connection = new SQLiteConnection(connetion_string);

            try
            {
                connection.Open();

                command = new SQLiteCommand(null, connection);

                // Create and prepare an SQL statement.
                // Use should never use something like: query = "insert into table values(" + value + ");" 
                // Especially when selecting. More about it on the lab about security.
                command.CommandText =
                    "DELETE FROM Tasks WHERE TaskId="+taskId.ToString();

                // Call Prepare after setting the Commandtext and Parameters.
                command.Prepare();
                int num_rows_changed = command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                if (num_rows_changed != 0) return true;
                return false;
            }
            catch (Exception ex)
            {
                log.Error("problem with delete column from the the database");
                connection.Close();
                return false;
            }

        }

        public static bool EditTask(string boardId, int taskId, string title, string description, string username, string dueDate, string columnId)
        {
            string connetion_string = null;
            string sql_query = null;
            string database_name = "KanbanDataBase.db";

            SQLiteConnection connection;
            SQLiteCommand command;

            connetion_string = $"Data Source={database_name};Version=3;";
            connection = new SQLiteConnection(connetion_string);
            SQLiteDataReader dataReader;

            try
            {

                connection.Open();

                sql_query = "Update Tasks SET Title=@param0, Description=@param1, DueDate=@param2 WHERE TaskId=" + taskId.ToString();
                command = new SQLiteCommand(sql_query);
                SQLiteParameter param0 = new SQLiteParameter(@"param0", title);
                SQLiteParameter param1 = new SQLiteParameter(@"param1", description);
                SQLiteParameter param2 = new SQLiteParameter(@"param2", dueDate);
                command.Parameters.Add(param0);
                command.Parameters.Add(param1);
                command.Parameters.Add(param2);
                command.Connection = connection;
                command.Prepare();
                dataReader = command.ExecuteReader();
                dataReader.Read();
                connection.Close();
                return true;

            }
            catch (Exception e)
            {
                log.Error("problem with edit the database");
                connection.Close();
                return false;
            }
        }
     
        public static bool setState(int taskId, string columnId)
        {
            string connetion_string = null;
            string sql_query = null;
            string database_name = "KanbanDataBase.db";

            SQLiteConnection connection;
            SQLiteCommand command;

            connetion_string = $"Data Source={database_name};Version=3;";
            connection = new SQLiteConnection(connetion_string);
            SQLiteDataReader dataReader;

            try
            {

                connection.Open();

                sql_query = "Update Tasks SET ColumnId=@param0 WHERE TaskId=" + taskId.ToString();
                command = new SQLiteCommand(sql_query);
                SQLiteParameter param0 = new SQLiteParameter(@"param0", columnId);
                command.Parameters.Add(param0);
                command.Connection = connection;
                command.Prepare();
                dataReader = command.ExecuteReader();
                dataReader.Read();
                connection.Close();
                return true;

            }
            catch (Exception)
            {
                log.Error("problem with edit the database");
                connection.Close();
                return false;
            }
        }

        public static int getTaskIndex()
        {
            string connetion_string = null;

            string database_name = "KanbanDataBase.db";

            SQLiteConnection connection;


            connetion_string = $"Data Source={database_name};Version=3;";
            connection = new SQLiteConnection(connetion_string);


            try
            {

                connection.Open();

                

                string sql = "SELECT MAX(TaskId) FROM Tasks";

                SQLiteCommand c = new SQLiteCommand(sql, connection);
                Object reader = c.ExecuteScalar();
                int index = Convert.ToInt32(reader.ToString());
                
                connection.Close();
                return index;

            }
            catch (Exception e)
            {
                log.Error("problem with extract from database");
                return 1;
            }
        }
    }
}



