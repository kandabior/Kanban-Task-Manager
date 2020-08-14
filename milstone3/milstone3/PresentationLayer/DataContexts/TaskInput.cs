using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milstone3.PresentationLayer.DataContexts
{
    class TaskInput : INotifyPropertyChanged
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

        public bool edit()
        {
          return  (UserController.editTask(this.taskId, this.userName,this.title, this.description, this.dueTime));
        }

        public bool delete()
        {
            return (UserController.deleteTask(this.taskId, this.userName));
        }

        public bool changeState()
        {
            return UserController.changeState(this.taskId, this.userName);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
