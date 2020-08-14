using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milstone3.PresentationLayer.DataContexts
{
    class editTask : INotifyPropertyChanged
    {
        private BoardWindowRow bwr;
        public editTask(BoardWindowRow bwr)
        {
            this.bwr = bwr;
            editTaskTitle = bwr.title;
            editTaskDecription = bwr.description;
            EditTaskDateTime = bwr.duedate;
            editTaskId = bwr.taskId;
            
           
        }
        
                int editTaskId ;
                public int EditTaskId
                {
                    get
                    {
                        return editTaskId;
                    }
                    set
                    {
                        editTaskId = bwr.taskId;

                        if (PropertyChanged != null)
                            PropertyChanged(this, new PropertyChangedEventArgs("editTaskId"));
                    }
                }

                string editTaskTitle = "";
                public string EditTaskTitle
                {
                    get
                    {
                        return editTaskTitle;
                    }
                    set
                    {
                        editTaskTitle = value;

                        if (PropertyChanged != null)
                            PropertyChanged(this, new PropertyChangedEventArgs("editTaskTitle"));
                    }
                }

                string editTaskDecription = "";
                public string EditTaskDecription
                {
                    get
                    {
                        return editTaskDecription;
                    }
                    set
                    {
                        editTaskDecription = value;

                        if (PropertyChanged != null)
                            PropertyChanged(this, new PropertyChangedEventArgs("editTaskDecription"));
                    }
                }

                string editTaskDateTime = "";
                public string EditTaskDateTime
                {
                    get
                    {
                        return editTaskDateTime;
                    }
                    set
                    {
                        editTaskDateTime = value;

                        if (PropertyChanged != null)
                            PropertyChanged(this, new PropertyChangedEventArgs("editTaskDateTime"));
                    }
                }
                
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
