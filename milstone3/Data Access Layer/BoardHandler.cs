using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.SQLite;

namespace milstone3
{
    [Serializable]
    public class DatBoard
    {
        private String boardId;
        private string userName;
        private int index;

        public DatBoard(int index,string userName, string boardId)
        {
            this.index = index;
            this.userName = userName;
            this.boardId = boardId;
        }

        public int getIndex()
        {
            return index;
        }
        public string getDatBoardId()
        {
            return this.boardId;
        }

        public string getUserName()
        {
            return this.userName;
        }
    }

    class BoardHandler
    {
        static log4net.ILog log = ILog.getlogger();
        public static bool deletBoard(int index,string userName , string boardId)
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

                
                command.CommandText =
                    "DELETE FROM Boards WHERE boardIndex=" + index;
               
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
                log.Error("problem with delte board from the the database");
                connection.Close();

                return false;
            }
        }

        public static bool addBoard(int index,string userName, string boardId)
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
                    "INSERT INTO Boards (BoardId,Username,boardIndex) " +
                    "VALUES (@BoardId, @Username, @boardIndex)";
                SQLiteParameter BoardId_param = new SQLiteParameter(@"BoardId", boardId);
                SQLiteParameter Username_param = new SQLiteParameter(@"Username", userName);
                SQLiteParameter boardIndex_param = new SQLiteParameter(@"boardIndex", index);

                command.Parameters.Add(BoardId_param);
                command.Parameters.Add(Username_param);
                command.Parameters.Add(boardIndex_param);

                // Call Prepare after setting the Commandtext and Parameters.
                command.Prepare();
                int num_rows_changed = command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
               if (num_rows_changed!=0) return true;
                return false;
            }
            catch (Exception ex)
            {
                log.Error("problem with adding board to the database");
                connection.Close();
                return false;
            }
        }

        public static List<DatBoard> getBoard()
        {
            
                string connetion_string = null;

                string database_name = "KanbanDataBase.db";

                SQLiteConnection connection;


                connetion_string = $"Data Source={database_name};Version=3;";
                connection = new SQLiteConnection(connetion_string);


                try
                {

                    connection.Open();

                    string sql = "select * from Boards";
                    SQLiteCommand c = new SQLiteCommand(sql, connection);
                    SQLiteDataReader reader = c.ExecuteReader();
                    List<DatBoard> output = new List<DatBoard>();
                    while (reader.Read())
                    {
                        Object o = reader["boardIndex"];
                        DatBoard dat = new DatBoard(Convert.ToInt32(o),(String)reader["Username"], (String)reader["BoardId"]);
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

                string sql = "SELECT MAX(boardIndex) FROM Boards";

                SQLiteCommand c = new SQLiteCommand(sql, connection);
                Object reader = c.ExecuteScalar();
                int index = Convert.ToInt32(reader.ToString());

                connection.Close();
                return index + 1;

            }
            catch (Exception e)
            {
                log.Error("problem with extract from database");
                return 1;
            }
        }
    }
}
