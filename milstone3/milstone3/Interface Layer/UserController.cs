using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milstone3
{
    class UserController
    {

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

        public static bool register(String username, String password)
        {
            return User.register(username, password);
        }

        public static bool addTask(String title, String descrip, String userName, String dueDate)
        {
            User u = User.getUserByEmail(userName);
            if (u != null)
            {
                return u.addTask(title, descrip, DateTime.Parse(dueDate));
            }
            else
            {
                log.Error("treid to add task with incorrect user: " + userName);
                return false;
            }
        }

        public static bool deleteTask(int taskId,String username) 
        {
            User u = User.getUserByEmail(username);
            if (u != null)
            {
                return u.deleteTask(taskId);
            }
            else
            {
                log.Error("treid to delete task with incorrect user: " + username);
                return false;
            }
        }

        public static bool editTask(int taskId, String username, String title, String description, string dueDate)
        {
            User u = User.getUserByEmail(username);
            if (u != null)
            {
                return u.editTask(taskId, title, description, DateTime.Parse(dueDate));
            }
            else
            {
                log.Error("treid to edit task with incorrect user: " + username);
                return false;
            }
        }

        public static bool changeState(int task, String username)
        {
            User u = User.getUserByEmail(username);
            if (u != null)
            {
                return u.changeState(task);
            }
            else
            {
                log.Error("treid to change state for a task with incorrect user: " + username);
                return false;
            }
        }

        public static bool setMaxTask(string username,int max,string columnId)
        {
            User u = User.getUserByEmail(username);
            if (u != null)
            {
                return u.setMaxTasks(columnId,max);
            }
            else
            {
                log.Error("treid to change state for a task with incorrect user: " + username);
                return false;
            }
        }

        public static void showTasks(string username, string columnId)
        {
            User u = User.getUserByEmail(username);
            if (u != null)
            {
                u.showTasks(columnId);
            }
            else
            {
                log.Error("treid to change state for a task with incorrect user: " + username);
                
            }
        }

        public static void printTask(String username, int taskId,string columnId)
        {
            User u = User.getUserByEmail(username);
            if (u != null)
            {
                 u.printTask(taskId,columnId);
            }
            else
            {
                log.Error("treid to change state for a task with incorrect user: " + username);
                
            }

        }
        public static bool changeOrderRight(string username, int place)
        {
            User u = User.getUserByEmail(username);
            if (u != null)
            {
                return u.changeOrderRight( place);
            }
            else
            {
                log.Error("treid to change state for a task with incorrect user: " + username);
                return false;
            }
        }
        public static bool changeOrderLeft(string username, int place)
        {
            User u = User.getUserByEmail(username);
            if (u != null)
            {
                return u.changeOrderLeft( place);
            }
            else
            {
                log.Error("treid to change state for a task with incorrect user: " + username);
                return false;
            }
        }

        public static bool sortTasks(string username, string columnId)
        {
            User u = User.getUserByEmail(username);
            if (u != null)
            {
                return u.sortTasks(columnId);
            }
            else
            {
                log.Error("treid to change state for a task with incorrect user: " + username);
                return false;
            }
        }


    }

}
