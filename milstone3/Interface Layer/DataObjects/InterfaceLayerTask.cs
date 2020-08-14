using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milstone3.Interface_Layer
{
    public class InterfaceLayerTask
    {
        public InterfaceLayerUser Author { get; private set; }
        public string title { get; private set; }
        public string columnId { get; private set; }
        public DateTime dueDate { get; private set; }
        public string description { get; private set; }
        public int taskId { get; private set; }
        public DateTime creationTime { get; private set; }

        public InterfaceLayerTask(InterfaceLayerUser author, string title,string columnId,DateTime duedate,string description, int taskId, DateTime creationTime)
        {
            this.Author = author;
            this.title = title;
            this.columnId = columnId;
            this.dueDate = duedate;
            this.description = description;
            this.taskId = taskId;
            this.creationTime = creationTime;
        }
    }
}
