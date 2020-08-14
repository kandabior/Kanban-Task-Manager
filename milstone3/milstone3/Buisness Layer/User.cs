using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace milstone3
{
    class User
    {
        static log4net.ILog log = ILog.getlogger();

        private string userName;
        private string password;
        private Board board;
        public static Hashtable hashTable=new Hashtable();
        private static List<User> users = new List<User>();
        private static List<DatUser> datUsers;
        bool logedIn = false;


        public User(String userName, String password, Board board)
        {
            this.userName = userName;
            this.password = password;
            this.board = board;
        }


        public bool addTask(String title, String description, DateTime dueTime)
        {
            if (this.logedIn)
            {
                return (board.addTask(title, description, this.userName, dueTime));
            }
            else
            {
                Console.WriteLine("user was not loged in");
                log.Error("user was not loged in");
                return false;
            }
        }

        public bool deleteTask(int taskId)
        {
            if (this.logedIn)
            {
                return (board.deleteTask(taskId));
            }
            else
            {
                Console.WriteLine("user was not loged in");
                log.Error("user was not loged in");
                return false;
            }
        }

        public bool editTask(int taskId, String title, String description, DateTime dueDate)
        {

            Task t = Task.getTaskById(taskId);
            if (t == null)
            {
                return false;
            }
            if (t.getUsername() != this.userName)
            {
                Console.WriteLine("the user doesnt own this task");
                log.Error(this.userName +"try to edit task that not his");
                return false;
            }

            if (this.logedIn )
            {
                return (board.editTask(taskId,title,description,dueDate));
            }
            else
            {
                Console.WriteLine("user was not loged in");
                log.Error("user was not loged in");
                return false;
            }
        }

        public bool changeState(int task)
        {
            if (this.logedIn)
            {
                return board.changeState(task);
            }
            else
            {
                Console.WriteLine("user was not loged in");
                log.Error("user was not loged in");
                return false;
            }
        }

        public bool setMaxTasks(string columnId, int number)
        {
            if (this.logedIn)
            {
                return board.setMaxTasks(columnId,number);
            }
            else
            {
                Console.WriteLine("user was not loged in");
                log.Error("user was not loged in");
                return false;
            }
        }


        public void showTasks(string columnId)
        {
            if (this.logedIn)
            {
                board.showTasks(columnId);
            }
            else
            {
                Console.WriteLine("user was not loged in");
                log.Error("user was not loged in");
            }

        }

        public static bool login(string userName, string password)
        {
            if (users == null)
            {
                datUserToUser(UserHandler.getUsers());
            }
            if (hashTable == null || !hashTable.Contains(userName))
            {
                Console.WriteLine("User not exist: "+ userName);
                log.Error("Attempt to sign in with user that not exist");
                return false;
            }
            else
            {
                if ((string)hashTable[userName] != password)
                {
                    Console.WriteLine("Incorrect password");
                    log.Warn(userName + " tried to login with incorrect password");
                    return false;
                }
                else
                {
                    Console.WriteLine(userName+" loged in succesfuly");
                    log.Info(userName + " loged in");
                    getUserByEmail(userName).initiateLogin();
                    return true; 
                }
            }
        }

        private void initiateLogin()
        {
            this.logedIn = true;
        }
        public bool logOut(String username)
        {
            this.logedIn = false;
            Console.WriteLine("User Log Out");
            log.Info("User Log Out");
            
            return true;

        }

        public static bool register(string userName, string password)
        {
            if (hashTable.Contains(userName))
            {
                    Console.WriteLine("User exist");
                    log.Error("Attempt to register a user that exist");
                    return false;
            }
            else
            {
                if (!IsValid.isValidUser(userName, password))
                {
                    return false;
                }
                hashTable.Add(userName,password);
                Console.WriteLine("created a new user name: " + userName);
                log.Info("created a new user name: " + userName);
                User u = new User(userName, password, Board.CreateBoard(userName));
                users.Add(u);
                datUsers.Add((new DatUser(u.userName, u.password,u.userName)));
                return UserHandler.saveUsers(datUsers);

            }
        }

        public static  User getUserByEmail(string userName)
        {
            foreach (User u in users)
            {
                if (u.userName == userName)
                    return u;
            }
            Console.WriteLine("user doesnt exist: "+ userName);
            log.Error("userName doesnt exist"+ userName);
            return null;
        }

        public bool printTask(int taskId, string columnId)
        {
            if (board.printTask(taskId, columnId))
            {
                return true;
            }
            else
                return false;
        }

        public static void datUserToUser(List<DatUser> datuser)
        {
            Board.datBoardToBoard();
            datUsers = datuser;
            if (datUsers != null)
            {
                foreach (DatUser dat in datUsers)
                {
                    users.Add(new User(dat.getDatUserName(), dat.getDatpassword(), Board.getBoardById(dat.getDatboardId())));
                    hashTable.Add((string)dat.getDatUserName(), (string)dat.getDatpassword());
                }
                log.Info("users initiated");
                Console.WriteLine("program was initiated");
            }
            else
            {
                datUsers = new List<DatUser>();
            }

        }

        public bool changeOrderLeft(int place)
        {
            if (this.logedIn)
            {
                return board.changeOrderLeft(place);
            }
            else
            {
                Console.WriteLine("user was not loged in");
                log.Error("user was not loged in");
                return false;
            }
        }
        public bool changeOrderRight(int place)
        {
            if (this.logedIn)
            {
                return board.changeOrderRight(place);
            }
            else
            {
                Console.WriteLine("user was not loged in");
                log.Error("user was not loged in");
                return false;
            }
        }

        public bool sortTasks(string columnId)
        {
            if (this.logedIn)
            {
                return board.sortTasks(columnId);
            }
            else
            {
                Console.WriteLine("user was not loged in");
                log.Error("user was not loged in");
                return false;
            }
        }
    }
}
