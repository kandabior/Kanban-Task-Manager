using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace milstone3
{
    [Serializable]
    public class DatColumn
    {
        private string columnId;
        private string taskId;
        private int maxTasks;
        private string boardId;

        public DatColumn(string columnId, string taskId, int maxTask, string boardId)
        {
            this.columnId = columnId;
            this.taskId = taskId;
            this.maxTasks = maxTask;
            this.boardId = boardId;
        }
        public void setMaxTasks(int max)
        {
            this.maxTasks = max;
        }
        public string getcolumnId()
        {
            return this.columnId;
        }
        public string gettaskId()
        {
            return this.taskId;
        }
        public int getmaxTasks()
        {
            return this.maxTasks;
        }
        public string getBoardId()
        {
            return this.boardId;
        }
    }

    class ColumnHandler
    {
        public static bool saveColumns(List<DatColumn> columns)
        {
            Stream myFileStream = File.Create("ColumnData.bin");
            BinaryFormatter serializes = new BinaryFormatter();
            serializes.Serialize(myFileStream, columns);
            myFileStream.Close();
            return true;
        }

        public static List<DatColumn> getColumns()
        {
            List<DatColumn> output = null;
            if (File.Exists("ColumnData.bin"))
            {
                Stream FileStream = File.OpenRead("ColumnData.bin");
                BinaryFormatter deserializer = new BinaryFormatter();
                output = ((List<DatColumn>)deserializer.Deserialize(FileStream));
                FileStream.Close();
            }
            return output;
        }
    }
}
