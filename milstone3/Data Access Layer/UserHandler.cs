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
    public class DatUser
    {
        private String userName;
        private String passWord;


        public DatUser(string username, string pass)
        {
            this.userName = username;
            this.passWord = pass;

        }
        public string getDatUserName()
        {
            return this.userName;
        }
        public string getDatpassword()
        {
            return this.passWord;
        }

    }

    class UserHandler
    {
        static log4net.ILog log = ILog.getlogger();

        public static bool addUser(string username, string password)
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
                    "INSERT INTO Users (Username,Password) " +
                    "VALUES (@Username, @Password)";
                SQLiteParameter Username_param = new SQLiteParameter(@"Username", username);
                SQLiteParameter Password_param = new SQLiteParameter(@"Password", password);

                command.Parameters.Add(Username_param);
                command.Parameters.Add(Password_param);

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
                log.Error("problem with adding user to the database");
                connection.Close();
                return false;
            }



        }




        public static List<DatUser> getUsers()
        {
            string connetion_string = null;

            string database_name = "KanbanDataBase.db";

            SQLiteConnection connection;


            connetion_string = $"Data Source={database_name};Version=3;";
            connection = new SQLiteConnection(connetion_string);


            try
            {

                connection.Open();

                string sql = "select * from Users";
                SQLiteCommand c = new SQLiteCommand(sql, connection);
                SQLiteDataReader reader = c.ExecuteReader();
                List<DatUser> output = new List<DatUser>();
                while (reader.Read())
                {
                    DatUser dat = new DatUser((String)reader["Username"], (String)reader["Password"]);
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
    }
}

