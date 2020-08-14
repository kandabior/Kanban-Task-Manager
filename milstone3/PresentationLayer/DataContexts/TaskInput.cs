using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milstone3.PresentationLayer.DataContexts
{
    public class TaskInput : INotifyPropertyChanged
    {
        int taskId;
        string publishDate;
        string title = "";
        string description = "";
        string userName;
        string dueTime = "";


        public String getUserName()
        {
            return this.userName;
        }
        public int getTaskId()
        {
            return this.taskId;
        }
        public string getPublishDate()
        {
            return this.publishDate;
        }
 
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("title"));
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("description"));
            }
        }
          public string DueTime
        {
            get
            {
                return dueTime;
            }
            set
            {
                dueTime = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("dueTime"));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
