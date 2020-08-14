using milstone3.Interface_Layer;
using milstone3.Interface_Layer.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milstone3
{
    public class UserController
    {
        //check for git purpuse
        public static int initiateCounter = 0;
        static log4net.ILog log = ILog.getlogger();

        public static void initiateProg()
        {
            if (initiateCounter == 0)
            {
                User.datUserToUser(UserHandler.getUsers());
                initiateCounter++;
            }

        }

        public static bool login(String username, String password)
        {
            return User.login(username, password);
        }
        public static bool logOut(String username)
        {
            User u = User.getUserByEmail(username);
            
            return u.logOut(username);
        }

        public static bool addNewColumn(string boardId, String userName, String columnId)
        {
            User u = User.getUserByEmail(userName);
            if (u != null)
            {
                return u.addNewColumn(boardId, columnId);
            }
            else
            {
                log.Error("tried to add column with incorrect user: " + userName);
                return false;
            }
        }
        public static bool register(String username, String password)
        {
            return User.register(username, password);
        }

        public static bool addTask(string boardId, String title, String descrip, String userName, String dueDate)
        {
            User u = User.getUserByEmail(userName);
            try
            {
                if (u != null)
                {
                    return u.addTask(boardId, title, descrip, DateTime.Parse(dueDate));
                }
                else
                {
                    log.Error("treid to add task with incorrect user: " + userName);
                    return false;
                }
            }
            catch (Exception)
            {
                log.Error("date not valid: " + dueDate);
                return false;
            }


        }

        public static bool deleteBoard(string boardId, string username)
        {
            User u = User.getUserByEmail(username);
            if (u != null)
            {
                return u.deleteBoard(boardId);
            }
            else
            {
                log.Error("treid to delete board with incorrect user: " + username);
                return false;
            }


        }

        public static bool deleteTask(string boardId, int taskId, String username)
        {
            User u = User.getUserByEmail(username);
            if (u != null)
            {
                return u.deleteTask(taskId, boardId);
            }
            else
            {
                log.Error("treid to delete task with incorrect user: " + username);
                return false;
            }
        }

        public static bool editTask(string boardId, int taskId, String username, String title, String description, string dueDate)
        {
            User u = User.getUserByEmail(username);
            if (u != null)
            {
                return u.editTask(taskId, boardId, title, description, DateTime.Parse(dueDate));
            }
            else
            {
                log.Error("treid to edit task with incorrect user: " + username);
                return false;
            }
        }

        public static bool changeState(string boardId, int task, String username)
        {
            User u = User.getUserByEmail(username);
            if (u != null)
            {
                return u.changeState(task, boardId);
            }
            else
            {
                log.Error("treid to change state for a task with incorrect user: " + username);
                return false;
            }
        }

        public static bool setMaxTask(string boardId, string username, int max, string columnId)
        {
            User u = User.getUserByEmail(username);
            if (u != null)
            {
                return u.setMaxTasks(boardId, columnId, max);
            }
            else
            {
                log.Error("treid to change state for a task with incorrect user: " + username);
                return false;
            }
        }

        public static void showTasks(string boardId, string username, string columnId)
        {
            User u = User.getUserByEmail(username);
            if (u != null)
            {
                u.showTasks(boardId, columnId);
            }
            else
            {
                log.Error("treid to change state for a task with incorrect user: " + username);

            }
        }

        public static void printTask(string boardId, String username, int taskId, string columnId)
        {
            User u = User.getUserByEmail(username);
            if (u != null)
            {
                u.printTask(boardId, taskId, columnId);
            }
            else
            {
                log.Error("treid to change state for a task with incorrect user: " + username);

            }

        }

        public static bool changeOrderRight(string boardId, string username, int place)
        {
            User u = User.getUserByEmail(username);
            if (u != null)
            {
                return u.changeOrderRight(boardId, place);
            }
            else
            {
                log.Error("treid to change state for a task with incorrect user: " + username);
                return false;
            }
        }

        public static bool DeleteColumn(string boardId, string userName, int i)
        {
            User u = User.getUserByEmail(userName);
            if (u != null)
            {
                return u.DeleteColumn(boardId, i);
            }
            else
            {
                log.Error("tried to delete column with incorrect user: " + userName);
                return false;
            }
        }

        public static bool changeOrderLeft(string boardId, string username, int place)
        {
            User u = User.getUserByEmail(username);
            if (u != null)
            {
                return u.changeOrderLeft(boardId, place);
            }
            else
            {
                log.Error("treid to change state for a task with incorrect user: " + username);
                return false;
            }
        }

        public static bool checkMaxTasks(string boardId, string username, int max, string columnId)
        {
            User u = User.getUserByEmail(username);
            if (u != null)
            {
                return u.checkMaxTasks(boardId, columnId, max);
            }
            else
            {
                log.Error("incorrect user: " + username);
                return false;
            }
        }
        public static bool sortTasks(string boardId, string username, string columnId)
        {
            User u = User.getUserByEmail(username);
            if (u != null)
            {
                return u.sortTasks(boardId, columnId);
            }
            else
            {
                log.Error("treid to change state for a task with incorrect user: " + username);
                return false;
            }
        }

        public InterfaceLayerBoard GetBoard(string boardId,string name)
        {
            try
            {
                List<InterfaceLayerColumn> columns = new List<InterfaceLayerColumn>();


                List<Board> boards = Board.getListBoardById(name);
                InterfaceLayerUser author = new InterfaceLayerUser(name);
                foreach (Board board in boards)
                {
                    foreach (Column c in board.getColumns)
                    {
                        if (c.getBoardId() == boardId)
                        {
                            List<InterfaceLayerTask> tasks = new List<InterfaceLayerTask>();
                            foreach (Task t in c.getTasks)
                            {
                                tasks.Add(new InterfaceLayerTask(author, t.getTitle(), t.getColumnId(), t.getDueDate(), t.getDescription(), t.getTaskId(), t.getCreationTime()));
                            }
                            columns.Add(new InterfaceLayerColumn(c.getColumnId(), c.getMaxTasks(), tasks));
                        }
                    }
                }
                return new InterfaceLayerBoard(name, columns);
            }
            catch (Exception)
            {
                return null;
            }
        }
      
        public static List<InterfaceLayerChooseBoard> GetlistBoards(string name)
        {
            List<InterfaceLayerChooseBoard> retList = new List<InterfaceLayerChooseBoard>();
            User u = User.getUserByEmail(name);
            IReadOnlyCollection<Board>  listBoards = u.getBoards;
            foreach (var b in listBoards)
            {
                retList.Add(new InterfaceLayerChooseBoard(b.getBoardId()));
            }
           
            return retList;
        }

        public static bool addBoard(string boardId, string username)
        {
            User u = User.getUserByEmail(username);
            if (u != null)
            {
                return u.addBoard(boardId, username);
            }
            else
            {
                log.Error("treid to add board with incorrect user: " + username);
                return false;
            }
        }

        public static int indexOfColumn(string boardId, String username, String Col)
        {
            User u = User.getUserByEmail(username);
            if (u != null)
            {
                return u.indexOfColumn(boardId, Col);
            }
            else
            {
                log.Error("treid to change state for a task with incorrect user: " + username);
                return -1;
            }
        }

    }

}
