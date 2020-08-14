using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace milstone3
{
    [Serializable]
    public class DatTask
    {
        private string taskId;
        private String title;
        private String description;
        private String username;
        private String dueDate;
        private String creationTime;
        private string columnId;

        public DatTask(string taskId,string title, string description, string username, string dueDate, string creationTime,string columnId)
        {
            this.taskId = taskId;
            this.title = title;
            this.description = description;
            this.username = username;
            this.dueDate = dueDate;
            this.creationTime = creationTime;
            this.columnId = columnId;

        }
        public string getTitle()
        {
            return title;
        }
        public string gettaskId()
        {
            return this.taskId;
        }
        public void setColumnId(string columnId)
        {
            this.columnId = columnId;
        } 
        public string getdescription()
        {
            return this.description;
        }
        public string getusername()
        {
            return this.username;
        }
        public string getdueDate()
        {
            return this.dueDate;
        }
        
        public string getcreationTime()
        {
            return this.creationTime;
        }
        public string getColumnId()
        {
            return this.columnId;
        }
       
        public void editTask(string title,string description, string duedate)
        {
            this.title = title;
            this.description = description;
            this.dueDate = duedate;
        }


    }

    

    class TaskHandler
    {
        public static bool saveTasks(List<DatTask> tasks, int taskIndex)
        {
            Stream myFileStream1 = File.Create("taskData.bin");
            BinaryFormatter serializes1 = new BinaryFormatter();
            serializes1.Serialize(myFileStream1, tasks);
            myFileStream1.Close();

            Stream myFileStream2 = File.Create("taskIndexData.bin");
            BinaryFormatter serializes2 = new BinaryFormatter();
            serializes2.Serialize(myFileStream2, taskIndex);
            myFileStream2.Close();
            return true;
        }

        public static List<DatTask> getTasks()
        {
            List<DatTask> output = null;
            if (File.Exists("taskData.bin"))
            {
                Stream FileStream = File.OpenRead("taskData.bin");
                BinaryFormatter deserializer = new BinaryFormatter();
                output = ((List<DatTask>)deserializer.Deserialize(FileStream));
                FileStream.Close();
            }
            return output;
        }

        public static int getTaskIndex()
        {
            int output=0;
            if (File.Exists("taskIndexData.bin"))
            {
                Stream FileStream = File.OpenRead("taskIndexData.bin");
                BinaryFormatter deserializer = new BinaryFormatter();
                output = ((int) deserializer.Deserialize(FileStream));
                FileStream.Close();
            }
            return output;
        }
    }
}



