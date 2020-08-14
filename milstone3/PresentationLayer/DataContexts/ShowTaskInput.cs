using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milstone3.PresentationLayer.DataContexts
{
    class ShowTaskInput : INotifyPropertyChanged
    {
        private BoardWindowRow bwr;
        public ShowTaskInput(string username, BoardWindowRow bwr)
        {
            this.bwr = bwr;
            showTaskTitle = bwr.title;
            showTaskDecription = bwr.description;
            ShowTaskDateTime = bwr.duedate;
            showTaskId = bwr.taskId;
            showUsername = username;
            showColumnId = bwr.columnId;
            showCreationTime = bwr.creationTime;

        }

        int showTaskId;
        public int ShowTaskId
        {
            get
            {
                return showTaskId;
            }
            set
            {
                showTaskId = bwr.taskId;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("showTaskId"));
            }
        }

        string showTaskTitle;
        public string ShowTaskTitle
        {
            get
            {
                return showTaskTitle;
            }
            set
            {
                showTaskTitle = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("showTaskTitle"));
            }
        }

        string showTaskDecription;
        public string ShowTaskDecription
        {
            get
            {
                return showTaskDecription;
            }
            set
            {
                showTaskDecription = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("showTaskDecription"));
            }
        }

        string showTaskDateTime;
        public string ShowTaskDateTime
        {
            get
            {
                return showTaskDateTime;
            }
            set
            {
                showTaskDateTime = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("showTaskDateTime"));
            }
        }

        string showColumnId;
        public string ShowColumnId
        {
            get
            {
                return showColumnId;
            }
            set
            {
                showColumnId = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("showColumnId"));
            }
        }

        string showUsername;
        public string ShowUsername
        {
            get
            {
                return showUsername;
            }
            set
            {
                showUsername = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("showUsername"));
            }
        }

        string showCreationTime;
        public string ShowCreationTime
        {
            get
            {
                return showCreationTime;
            }
            set
            {
                showCreationTime = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("showCreationTime"));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
