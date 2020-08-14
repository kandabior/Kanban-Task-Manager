using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milstone3
{
    class Column
    {
        static log4net.ILog log = ILog.getlogger();
        private string userName;
        private List<Task> Tasks;
        private String columnId;//name column
        private string boardId;
        private int index;
        private int maxTask;
        public static int columnIndex;
        private static List<Column> columns= new List<Column>();
        

        private Column( int index,string userName , String columnId,List<Task> taskList, int maxTask, string boardId)
        {
            this.index = index;   
            this.userName = userName;
            this.Tasks = taskList;
            this.columnId = columnId;
            this.maxTask = maxTask;
            this.boardId = boardId;
        }
        public int getIndex()
        {
            return this.index;
        }

        public string getBoardId()
        {
            return this.boardId;
        }

        public static Column getColumnByTaskId(int id)
        {
            for(int i=0; i<columns.Count; i++)
            {
                for (int j = 0; j < columns.ElementAt(i).Tasks.Count; j++)
                {
                    if(columns.ElementAt(i).Tasks.ElementAt(j).getTaskId() == id)
                    {
                        return columns.ElementAt(i);
                    }

                }
            }
            Console.WriteLine("task: " +id+ "doesnt exist");
            log.Error("task: " + id + "doesnt exist");
            return null;
        }

        public IReadOnlyCollection<Task> getTasks
        {
            get { return Tasks.AsReadOnly(); }
        }

        public static Column creatColumn(string userName , String columnId,string boardId)
        {

            Column column = new Column(columnIndex,userName ,columnId, new List<Task>(),int.MaxValue, boardId);
            columns.Add(column);
            ColumnHandler.addColumn(columnIndex,userName,columnId, int.MaxValue, boardId);
            columnIndex++;
            return column;
        }

        public int getMaxTasks()
        {
            return this.maxTask;
        }

        public string getColumnId()
        {
            return columnId;
        }

        public static List<Column> getColumnById(string boardId, string userName)
        {
            List<Column> output = new List<Column>();
            foreach (Column col in columns)
            {
                if (col.boardId == boardId & col.userName == userName)
                {
                    output.Add(col);
                }
            }
            return output;
        }

        public static void datColToCol()
        {
            columnIndex = ColumnHandler.getIndex();
            List<DatColumn> datColumns = ColumnHandler.getColumns();
            Task.datTaskToTask();
            if (datColumns != null)
            {
                foreach (DatColumn dat in datColumns)
                {
                    columns.Add(new Column(dat.getIndex(),dat.getUserName(), dat.getcolumnId(), Task.getTaskByColumnId(dat.getcolumnId(),dat.getBoardId(),dat.getUserName()),dat.getmaxTasks(),dat.getBoardId()));
                }
            }
            else
            {
                datColumns = new List<DatColumn>();
            }

        }

        public bool checkMaxTask(int max)
        {
            if (Tasks.Count > max)
            {
                log.Error("there are more tasks than the max input: " + max);
                return false;
            }
            return true;
        }

        public bool checkMaxTask()
        {
            if (Tasks.Count >= maxTask)
            {
                Console.WriteLine("you have reached to the maximum tasks in the column: " + this.columnId);
                log.Info("attemp to add task to a limited column: " + this.columnId);
                return false;
            }
            return true;
        }

        public bool addTask (String title, String description, String username, DateTime DueTime)
        {
            if (!this.checkMaxTask())
            {
                return false;
            }
            if (IsValid.isValidTask(title, description,DueTime))
            {
                Task task = Task.createTask(boardId, title, description, username, DueTime, columnId);
                Tasks.Add(task);
                Console.WriteLine("new task added by " + username);
                Console.WriteLine("your task Id: " + task.getTaskId());
                log.Info("new task added by " + username);
                return true;
            }
            return false;
            
        }

        public void showTasks()
        {
            int i = 1;
            foreach(Task t in Tasks)
            {
                Console.WriteLine("Task "+i+": ");
                t.ToString();
                i++;
            }
        }

        public bool deleteTask(int id)
        {
            Task del = SearchTaskById(id);
            if (del!=null)
            {
                if (Tasks.Remove(del))
                    if (Task.deleteTask(del))
                    {
                        Console.WriteLine("task deleted");
                        log.Info("task: " + del.getTaskId() + " deleted");
                        return true;
                    }
            }
            Console.WriteLine("task was not found");
            log.Info("task: " + id + " was not found for deleting");
            return false;
        }
                
        public bool setMaxTasks (int max)
        {
            if(max< Tasks.Count)
            {
                log.Error("there is more tasks in the column already: "+ this.boardId+" , "+ columnId);
                return false;
            }
            if (max < 0 | max > int.MaxValue)
            {
                log.Error("the value of max tasks is ilegal");
                Console.WriteLine("the value of max tasks is ilegal");
                return false;
            }
            this.maxTask = max;
            log.Info("new max task Was determined");
            Console.WriteLine("new max task Was determined");
            ColumnHandler.chengeMaxTask(index ,maxTask);
            return true;
        }

        

        public bool editTask(int id, String title, String description, DateTime dueDate)
        {
            Task task = SearchTaskById(id);
            if (task != null)
            {
                if (!(IsValid.isValidTask(title, description,dueDate)))
                {
                    log.Error("could not edit task " + task.getTaskId() + " because invalid input");
                    Console.WriteLine("could not edit task " + task.getTaskId() + " because invalid input");
                    return false;
                }
                else
                {
                    if (Task.editTask(task, title, description, dueDate))
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            return false;
        }

        public static bool DeleteColumn(Column column, string username)
        {
            foreach(Column col in columns)
            {
                if(col.getColumnId()==column.getColumnId() && col.boardId == column.boardId)
                {
                    try
                    {
                        columns.Remove(col);
                        log.Info(username + " deleted column: " + column.columnId);
                        break;
                    }
                    catch (Exception)
                    {
                        log.Error("could not remove column from main list: " + col.boardId + " , " + col.columnId);
                        return false;
                    }
                }
            }
            foreach(Task task in column.Tasks)
            {
                Task.deleteTask(task);
            }
            if (ColumnHandler.deleteColumn(column.index))
            {
                log.Info("column was deleted succesfuly");
                return true;
            }
            else
            {
                log.Info("problem with saving the data of deleting column");
                return false;
            }
        }

        public bool addExistingTask(int id)
        {
            Task task = SearchTaskById(id);
            if (task != null)
            {
                Tasks.Add(task);
                task.setState(this.columnId);
                return true;
            }
            return false;
            
        }

        public bool removeTaskFromColumn(int id)
        {
            Task task = SearchTaskById(id);
            if (task != null)
            {
                if (Tasks.Remove(task))
                {
                    return true;
                }
                else
                {
                    log.Error("Could not remove task from: " + this.columnId);
                    return false;
                }
            }
            return false;

        }

        public bool printTask(int taskId)
        {
            foreach(Task task in Tasks)
            {
                if (task.getTaskId() == taskId)
                {
                    task.printTask();
                    return true;
                }
            }
            log.Error("task could not be found in the current column: "+ this.columnId);
            Console.WriteLine("task could not be found in the current column: " + this.columnId);
            return false;
        }

        public static void SwapColumns(Column column1, Column column2)
        {
            int tmp = column1.index;
            column1.index = column2.index;
            column2.index = tmp;
            ColumnHandler.swapColumn(column1.index, column2.index,column1.userName, column2.userName,column1.boardId, column2.boardId,column1.columnId,column2.columnId,column1.maxTask,column2.maxTask);
            columns.Sort(new ColumnComperator());
        }

        private Task SearchTaskById(int id)
        {
            Task del = null;
            bool found = false;
            for (int i = 0; i < Task.tasks.Count & !found; i++)
            {
                if (Task.tasks.ElementAt(i).getTaskId() == id)
                {
                    del = Task.tasks.ElementAt(i);
                    found = true;
                }
            }
            if (!found)
            {
                log.Error("task could not be found in the current column: " + this.columnId);
                Console.WriteLine("task could not be found in the current column: " + this.columnId);
            }
            return del;
        }

        public bool sortTasks()
        {
            Tasks.Sort(new TaskComperator());
            return true;
        }
    }
}
