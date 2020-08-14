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
        private List<Board> boards;
        public static Hashtable hashTable = new Hashtable();
        private static List<User> users = new List<User>();
        bool logedIn = false;


        public User(String userName, String password, List<Board> boards)
        {
            this.userName = userName;
            this.password = password;
            this.boards = boards;
        }
        public bool addTask(string boardId, String title, String description, DateTime dueTime)
        {
            if (this.logedIn)
            {
                foreach (Board board in boards)
                {
                    if (boardId == board.getBoardId())
                    {
                        return (board.addTask(title, description, this.userName, dueTime));
                    }
                }
                log.Error("error with finding the board: " + boardId + ", " + userName);
                return false;
            }
            else
            {
                Console.WriteLine("user: " + this.userName + " was not loged in");
                log.Error("user: " + this.userName + " was not loged in");
                return false;
            }
        }

        public bool deleteTask(int taskId, string boardId)
        {
            if (this.logedIn)
            {
                foreach (Board board in boards)
                {
                    if (boardId == board.getBoardId())
                    {
                        return (board.deleteTask(taskId));
                    }
                }
                log.Error("error with finding the board: " + boardId + ", " + userName);
                return false;

            }
            else
            {
                Console.WriteLine("user was not loged in");
                log.Error("user was not loged in");
                return false;
            }
        }

        public bool editTask(int taskId, string boardId, String title, String description, DateTime dueDate)
        {

            Task t = Task.getTaskById(taskId);
            if (t == null)
            {
                return false;
            }
            if (t.getUsername() != this.userName)
            {
                Console.WriteLine("the user doesnt own this task");
                log.Error(this.userName + "try to edit task that not his");
                return false;
            }

            if (this.logedIn)
            {
                foreach (Board board in boards)
                {
                    if (boardId == board.getBoardId())
                    {
                        return (board.editTask(taskId, title, description, dueDate));
                    }
                }
                log.Error("error with finding the board: " + boardId + ", " + userName);
                return false;
            }
            else
            {
                Console.WriteLine("user was not loged in");
                log.Error("user was not loged in");
                return false;
            }
        }

        public bool deleteBoard(string boardId)
        {
            foreach(Board b in boards)
            {
                if (b.getBoardId() == boardId)
                {
                    boards.Remove(b);
                    return Board.removeBoard(b, this.userName);
                }
            }
            log.Error("could not find the board by the Id: " + boardId);
            return false;
        }

        public bool changeState(int taskId, string boardId)
        {
            if (this.logedIn)
            {
                foreach (Board board in boards)
                {
                    if (boardId == board.getBoardId())
                    {
                        return (board.changeState(taskId));
                    }
                }
                log.Error("error with finding the board: " + boardId + ", " + userName);
                return false;
            }
            else
            {
                Console.WriteLine("user was not loged in");
                log.Error("user was not loged in");
                return false;
            }
        }

        public bool setMaxTasks(string boardId, string columnId, int number)
        {
            if (this.logedIn)
            {
                foreach (Board board in boards)
                {
                    if (boardId == board.getBoardId())
                    {
                        return (board.setMaxTasks(columnId, number));
                    }
                }
                log.Error("error with finding the board: " + boardId + ", " + userName);
                return false;
            }
            else
            {
                Console.WriteLine("user was not loged in");
                log.Error("user was not loged in");
                return false;
            }
        }

        public bool addNewColumn(string boardId, string columnId)
        {
            foreach (Board board in boards)
            {
                if (boardId == board.getBoardId())
                {
                    if (board.addNewColumn(columnId, boardId))
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            log.Error("error with finding the board: " + boardId + ", " + userName);
            return false;

        }

        public void showTasks(string boardId, string columnId)
        {
            if (this.logedIn)
            {
                foreach (Board board in boards)
                {
                    if (boardId == board.getBoardId())
                    {
                        board.showTasks(columnId);
                    }
                }
                log.Error("error with finding the board: " + boardId + ", " + userName);

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
                Console.WriteLine("User not exist: " + userName);
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
                    User u = getUserByEmail(userName);
                    if (u.logedIn)
                    {
                        log.Error("user is already loged in: " + userName);
                        return false;
                    }
                    u.initiateLogin();
                    Console.WriteLine(userName + " loged in succesfuly");
                    log.Info(userName + " loged in");
                    
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
            log.Info(username + " Log Out");

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
                hashTable.Add(userName, password);
                Console.WriteLine("created a new user name: " + userName);
                log.Info("created a new user name: " + userName);
                Board newBoard = Board.CreateBoard(userName, userName);
                List<Board> newBoardList = new List<Board>();
                newBoardList.Add(newBoard);
                User u = new User(userName, password, newBoardList);
                users.Add(u);
                return UserHandler.addUser(userName,password);

            }
        }

        internal bool DeleteColumn(string boardId, int i)
        {
            foreach (Board board in boards)
            {
                if (boardId == board.getBoardId())
                {
                    if (board.DelelteColumn(i, board.getBoardId()))
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            log.Error("error with finding the board: " + boardId + ", " + userName);
            return false;
        }

        internal bool checkMaxTasks(string boardId, string columnId, int max)
        {
            foreach (Board board in boards)
            {
                if (boardId == board.getBoardId())
                {
                    return board.checkMaxTasks(columnId, max);
                }
            }

            log.Error("error with finding the board: " + boardId + ", " + userName);
            return false;
        }

        internal bool addBoard(string boardId, string username)
        {
            if (boardId == "")
            {
                log.Error("cant add board with empty name: "+ username);
                return false;
            }
            foreach(Board board in boards)
            {
                if (board.getBoardId() == boardId)
                {
                    log.Error("cant add board with same boardName: " + boardId+", "+username);
                    return false;
                }
            }
            Board newBoard = Board.CreateBoard(username, boardId);
            boards.Add(newBoard);
            return true;


        }

        public static User getUserByEmail(string userName)
        {
            foreach (User u in users)
            {
                if (u.userName == userName)
                    return u;
            }
            Console.WriteLine("user doesnt exist: " + userName);
            log.Error("userName doesnt exist" + userName);
            return null;
        }

        public bool printTask(string boardId, int taskId, string columnId)
        {
            foreach (Board board in boards)
            {
                if (boardId == board.getBoardId())
                {
                    if (board.printTask(taskId, columnId))
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            log.Error("error with finding the board: " + boardId + ", " + userName);
            return false;

        }

        public static void datUserToUser(List<DatUser> datuser)
        {
            Board.datBoardToBoard();
            List<DatUser> datUsers = datuser;
            if (datUsers != null)
            {
                foreach (DatUser dat in datUsers)
                {
                    users.Add(new User(dat.getDatUserName(), dat.getDatpassword(), Board.getListBoardById(dat.getDatUserName())));
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

        public bool changeOrderLeft(string boardId, int place)
        {
            if (this.logedIn)
            {
                foreach (Board board in boards)
                {
                    if (boardId == board.getBoardId())
                    {
                        return board.changeOrderLeft(place);
                    }
                }
                log.Error("error with finding the board: " + boardId + ", " + userName);
                return false;

            }
            else
            {
                Console.WriteLine("user was not loged in");
                log.Error("user was not loged in");
                return false;
            }
        }
        public bool changeOrderRight(string boardId, int place)
        {
            if (this.logedIn)
            {
                foreach (Board board in boards)
                {
                    if (boardId == board.getBoardId())
                    {
                        return board.changeOrderRight(place);
                    }
                }
                log.Error("error with finding the board: " + boardId + ", " + userName);
                return false;

            }
            else
            {
                Console.WriteLine("user was not loged in");
                log.Error("user was not loged in");
                return false;
            }
        }

        public bool sortTasks(string boardId, string columnId)
        {
            if (this.logedIn)
            {
                foreach (Board board in boards)
                {
                    if (boardId == board.getBoardId())
                    {
                        return board.sortTasks();
                    }
                }
                log.Error("error with finding the board: " + boardId + ", " + userName);
                return false;

            }
            else
            {
                Console.WriteLine("user was not loged in");
                log.Error("user was not loged in");
                return false;
            }
        }

        public int indexOfColumn(string boardId, String Col)
        {
            foreach (Board board in boards)
            {
                if (boardId == board.getBoardId())
                {
                    return board.indexOfColumn(Col);
                }
            }
            log.Error("error with finding the board: " + boardId + ", " + userName);
            return -1;
        }
        public IReadOnlyCollection<Board> getBoards
        {
            get { return boards.AsReadOnly(); }
        } 
    }

}

