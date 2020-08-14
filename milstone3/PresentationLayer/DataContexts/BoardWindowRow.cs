using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using milstone3.Interface_Layer;

namespace milstone3.PresentationLayer.DataContexts
{
    public class BoardWindowRow
    {
        private string username;
        private InterfaceLayerTask task;

        public string title { get; set; }
        public string description { get; set;}
        public string duedate { get; set; }
        public string columnId { get; set; }
        public int taskId { get; set; }
        public string creationTime { get; set; }

        public BoardWindowRow(string username, InterfaceLayerTask task)
        {
            this.username = username;
            this.task = task;
            title = task.title;
            columnId = task.columnId;
            description = task.description;
            this.taskId = task.taskId;
            this.duedate = task.dueDate.ToString("dd/MM/yyy");
            this.creationTime=task.creationTime.ToString("dd/MM/yyy");
        }
        
        //public event PropertyChangedEventHandler PropertyChanged;

    }
}
