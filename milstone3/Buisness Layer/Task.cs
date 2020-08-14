using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milstone3
{
    public class Task
    {
        static log4net.ILog log = ILog.getlogger();

        private int taskId;
        private string title;
        private string description;
        private string userName;
        private DateTime dueTime;
        private string boardId;
        private String columnId;
        private DateTime date;
        private static int taskIndex;
        public static List<Task> tasks = new List<Task>();
        //public static List<DatTask> datTasks;


        private Task(string boardId ,int taskId,string title, string description, string username, DateTime dueTime,DateTime date,String columnId)
        {
            this.boardId = boardId;
            this.taskId = taskId;
            this.title = title;
            this.description = description;
            this.userName = username;
            this.dueTime = dueTime;
            this.date = date;
            this.columnId = columnId;
        }

        public static Task createTask(string boardId ,string title, string description, string username, DateTime dueTime,string columnId)
        {
            Task task = new Task(boardId,  taskIndex, title, description, username, dueTime, DateTime.Now,columnId);
            tasks.Add(task);
            if (TaskHandler.addTasks(boardId, taskIndex, title, description, username, dueTime.ToString(), DateTime.Now.ToString(), columnId)) {
                log.Info("added new task to databas: " + username + " " + taskIndex);
            }
            else
            {
                log.Info("couldnt to add new task to databas: " + username + " " + taskIndex);
            }
            taskIndex++;
            return task;
        }

        public string getColumnId()
        {
            return this.columnId;
        }

        public String getUsername()
        {
            return this.userName;
        }

        public static List<Task> getTaskByColumnId(String columnId,string boardId,string username)
        {
            List<Task> tasklist = new List<Task>();
            foreach (Task task in Task.tasks)
            {
                if (task.columnId == columnId && task.boardId==boardId && task.userName==username)
                {
                    tasklist.Add(task);
                }
            }
            return tasklist;
        }

        public static void datTaskToTask()
        {
            List<DatTask>datTasks = TaskHandler.getTasks();
            taskIndex = TaskHandler.getTaskIndex()+1;
            if (datTasks != null)
            {
                foreach (DatTask dat in datTasks)
                {
                    tasks.Add(new Task(dat.getBoardId(), Convert.ToInt32(dat.gettaskId()), dat.getTitle(), dat.getdescription(), dat.getusername(), DateTime.Parse(dat.getdueDate()),
                         DateTime.Parse(dat.getcreationTime()),dat.getColumnId()));
                }
            }
            else
            {
                datTasks = new List<DatTask>();
            }
        }

        public string getTitle()
        {
            return this.title;
        }
        public string getUser()
        {
            return this.userName;
        }
        public string getDescription()
        {
            return this.description;
        }
       
        public DateTime getCreationTime()
        {
            return this.date;
            
        }
        public DateTime getDueDate()
        {
            return this.dueTime;

        }
        public bool isValid()
        {
            return (IsValid.isValidTask(title, description,dueTime));
        }
        public bool setTitle(string title)
        {
            if (!IsValid.isValidTitle(title)) return false;
            this.title = title;
            return true;
        }
        public bool setDescription(string description)
        {
            if (!IsValid.isValidDescription(title)) return false;
            this.description = description;
            return true;
        }

        public int getTaskId()
        {
            return this.taskId;
        }

       
        public bool setDueDate(DateTime dueDate)
        {
            this.dueTime = dueDate;
            return true;
        }

        public bool setState(string columnId)
        { 
            this.columnId = columnId;
            if (TaskHandler.setState(taskId, columnId))
                return true;
            return false;
        }

        public void ToString()
        {
            Console.WriteLine(this.title);
            Console.WriteLine(this.description);
            Console.WriteLine(this.dueTime);
            Console.WriteLine("task Id: "+this.taskId);
        }

        public static Task getTaskById(int taskId)
        {
            foreach (Task task in tasks)
            {
                if (task.taskId == taskId)
                {
                    return task;
                }
            }
            Console.WriteLine("Could not find a task by the current Id");
            log.Error("task does not found by Id");
            return null;
        }

        public static bool deleteTask(Task task)
        {
            if (tasks.Remove(task))
            {
                if (TaskHandler.deleteTask(task.taskId, task.columnId, task.boardId, task.userName))
                {
                    log.Info("delted task from database: " + task.taskId + " taskId");
                    return true;
                }
                else
                {
                    log.Info("couldnt delete task form databas: " + task.taskId + " taskId");
                }
                return false;
            }
            return false;
        }

        public static bool editTask(Task task, String title, String description, DateTime dueDate)
        {
            if (task.setTitle(title) & task.setDescription(description) & task.setDueDate(dueDate))
            {
                if(TaskHandler.EditTask(task.boardId, task.taskId, title, description, task.userName, dueDate.ToString(), dueDate.ToString()))
                    return true;
            }
            log.Info("atempted to edit task "+task.taskId+" unsuccesfuly");
            Console.WriteLine("could not edit task "+task.taskId);
            return false;
        }

        public void printTask()
        {
            Console.WriteLine(this.title);
        }

    }

}



