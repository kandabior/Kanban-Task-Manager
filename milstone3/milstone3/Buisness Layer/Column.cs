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

        private List<Task> Tasks;
        private String columnId;
        private string boardId;
        private int maxTask;
        private static List<Column> columns= new List<Column>();
        private static List<DatColumn> datColumns ;

        private Column(String columnId,List<Task> taskList, int maxTask, string boardId)
        {
            this.Tasks = taskList;
            this.columnId = columnId;
            this.maxTask = maxTask;
            this.boardId = boardId;
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

        public static Column creatColumn(String columnId,string boardId)
        {
            Column column = new Column(columnId, new List<Task>(),int.MaxValue, boardId);
            columns.Add(column);
            datColumns.Add(new DatColumn(columnId, columnId, column.maxTask,boardId));
            ColumnHandler.saveColumns(Column.datColumns);
            return column;
        }

        public string getColumnId()
        {
            return columnId;
        }

        public static List<Column> getColumnById(string boardId)
        {
            List<Column> output = new List<Column>();
            foreach (Column col in columns)
            {
                if (col.boardId == boardId)
                {
                    output.Add(col);
                }
            }
            return output;
        }

        public static void datColToCol()
        {
            datColumns = ColumnHandler.getColumns();
            Task.datTaskToTask();
            if (datColumns != null)
            {
                foreach (DatColumn dat in datColumns)
                {
                    columns.Add(new Column(dat.getcolumnId(), Task.getTaskByColumnId(dat.getcolumnId()),dat.getmaxTasks(),dat.getBoardId()));
                }
            }
            else
            {
                datColumns = new List<DatColumn>();
            }

        }

        public bool checkMaxTask()
        {
            if (Tasks.Count >= maxTask)
            {
                Console.WriteLine("attempt to add a task to a limited column");
                log.Info("you have reached to the maximum tasks in the column");
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
            if (IsValid.isValidTask(title, description))
            {
                Task task = Task.createTask(title, description, username, DueTime, columnId);
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
            if (max < 0 | max > int.MaxValue)
            {
                log.Error("the value of max tasks is ilegal");
                Console.WriteLine("the value of max tasks is ilegal");
                return false;
            }
            this.maxTask = max;
            log.Info("new max task Was determined");
            Console.WriteLine("new max task Was determined");
            DatColumn datcolumn= getDatColumnById(this.columnId);
            datcolumn.setMaxTasks(this.maxTask);
            ColumnHandler.saveColumns(datColumns);
            return true;
        }

        public static DatColumn getDatColumnById(string columnId)
        {
            foreach(DatColumn dat in datColumns)
            {
                if (dat.getcolumnId() == columnId)
                {
                    return dat;
                }
            }
            return null;
        }

        public bool editTask(int id, String title, String description, DateTime dueDate)
        {
            Task task = SearchTaskById(id);
            if (task != null)
            {
                if (!(IsValid.isValidTask(title, description)))
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

        private Task SearchTaskById(int id)
        {
            Task del = null;
            bool found = false;
            for (int i = 0; i < Tasks.Count & !found; i++)
            {
                if (Tasks.ElementAt(i).getTaskId() == id)
                {
                    del = Tasks.ElementAt(i);
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
