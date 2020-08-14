using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Data.SQLite;

namespace milstone3
{
    [Serializable]
    public class DatColumn
    {
        private string columnId;

        private int maxTasks;
        private string boardId;
        private int index;
        private string userName;

        public DatColumn(int index,string userName, string columnId, int maxTask, string boardId)
        {
            this.userName = userName;
            this.columnId = columnId;
            this.index = index;
            this.maxTasks = maxTask;
            this.boardId = boardId;
        }
        public int getIndex()
        {
            return this.index;
        }
        public void setMaxTasks(int max)
        {
            this.maxTasks = max;
        }
        public string getcolumnId()
        {
            return this.columnId;
        }

        public int getmaxTasks()
        {
            return this.maxTasks;
        }
        public string getBoardId()
        {
            return this.boardId;
        }

        internal string getUserName()
        {
            return this.userName;
        }
    }

    class ColumnHandler
    {
        static log4net.ILog log = ILog.getlogger();
       
        public static List<DatColumn> getColumns()
        {
            string connetion_string = null;

            string database_name = "KanbanDataBase.db";

            SQLiteConnection connection;


            connetion_string = $"Data Source={database_name};Version=3;";
            connection = new SQLiteConnection(connetion_string);


            try
            {

                connection.Open();

                string sql = "select * from Columns";
                SQLiteCommand c = new SQLiteCommand(sql, connection);
                SQLiteDataReader reader = c.ExecuteReader();
                List<DatColumn> output = new List<DatColumn>();
                while (reader.Read())
                {
                    object max = reader["MaxTask"];
                    object index = reader["ColumnIndex"];
                    DatColumn dat = new DatColumn(Convert.ToInt32(index), (string)reader["UserName"], (string)reader["ColumnId"],Convert.ToInt32( max) , (string)reader["BoardId"]);
                    output.Add(dat);
                }
                connection.Close();
                return output;

            }
            catch (Exception)
            {
                log.Error("problem with extract from database");
                return null;
            }
        }

        public static bool addColumn(int Index,string userName, string columnId, int maxTask, string boardId)
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
                    "INSERT INTO Columns (ColumnId,MaxTask,BoardId , Username, ColumnIndex) " +
                    "VALUES (@ColumnId, @MaxTask , @BoardId ,@Username , @ColumnIndex)";
                SQLiteParameter ColumnId_param = new SQLiteParameter(@"ColumnId", columnId);
                SQLiteParameter MaxTask_param = new SQLiteParameter(@"MaxTask", maxTask);
                SQLiteParameter BoardId_param = new SQLiteParameter(@"BoardId", boardId);
                SQLiteParameter Username_param = new SQLiteParameter(@"Username", userName);
                SQLiteParameter Index_param = new SQLiteParameter(@"ColumnIndex", Index);

                command.Parameters.Add(ColumnId_param);
                command.Parameters.Add(MaxTask_param);
                command.Parameters.Add(BoardId_param);
                command.Parameters.Add(Username_param);
                command.Parameters.Add(Index_param);

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
                log.Error("problem with adding column to the database");
                connection.Close();
                return false;
            }
        }

        public static int getIndex()
        {
            string connetion_string = null;

            string database_name = "KanbanDataBase.db";

            SQLiteConnection connection;


            connetion_string = $"Data Source={database_name};Version=3;";
            connection = new SQLiteConnection(connetion_string);


            try
            {

                connection.Open();
                                            
                string sql = "SELECT MAX(ColumnIndex) FROM Columns";

                SQLiteCommand c = new SQLiteCommand(sql, connection);
                Object reader = c.ExecuteScalar();
                int index = Convert.ToInt32(reader.ToString());

                connection.Close();
                return index+1;

            }
            catch (Exception e)
            {
                log.Error("problem with extract from database");
                return 1;
            }
        }

        public static bool deleteColumn(int index)
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
                    "DELETE FROM Columns WHERE ColumnIndex=" + index;

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

        public static bool chengeMaxTask(int index,int maxTask)
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
                
                sql_query = "Update Columns SET MaxTask=@param0 WHERE ColumnIndex=" + index.ToString();
                command = new SQLiteCommand(sql_query);
                SQLiteParameter param0 = new SQLiteParameter(@"param0", maxTask);
                command.Parameters.Add(param0);
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

        internal static void swapColumn(int column1, int column2,string username1,string username2,string board1,string board2,string columnId1,string columnId2, int max1,int max2)
        {
            
            string connetion_string = null;
            string sql_query1 = null;
            string sql_query2 = null;
            string database_name = "KanbanDataBase.db";

            SQLiteCommand command;
            SQLiteCommand command2;

            SQLiteConnection connection;
            
            connetion_string = $"Data Source={database_name};Version=3;";
            connection = new SQLiteConnection(connetion_string);
            SQLiteDataReader dataReader1;
            SQLiteDataReader dataReader2;

            try
            {

                connection.Open();
                command = new SQLiteCommand(null, connection);
                command.CommandText =
                    "DELETE FROM Columns WHERE ColumnIndex=" + column1;
                command.Prepare();
                command.ExecuteNonQuery();
                command.Dispose();
                command2 = new SQLiteCommand(null, connection);
                command2.CommandText =
                    "DELETE FROM Columns WHERE ColumnIndex=" + column2;
                command2.Prepare();
                command2.ExecuteNonQuery();
                command2.Dispose();

                addColumn(column1, username1, columnId1, max1, board1);
                addColumn(column2, username2, columnId2, max2, board2);



                connection.Close();
                

            }
            catch (Exception e)
            {
                log.Error("problem with edit the database");
                connection.Close();
                
            }
        }
    }
}

