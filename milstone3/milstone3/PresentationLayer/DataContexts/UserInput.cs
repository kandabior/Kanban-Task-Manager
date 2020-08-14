using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milstone3.PresentationLayer.DataContexts
{
    class UserInput : INotifyPropertyChanged
    {
            string userName = "";
            public string UserName
            {
                get
                {
                    return userName;
                }
                set
                {
                    userName = value;
                  
                if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("UserName"));
                }
            }

            string pwd = "";
            public string PWD
            {
                get
                {
                    return pwd;
                }
                set
                {
                    pwd = value;

                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("PWD"));
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;

            public bool login()
            {
                return UserController.login(this.userName, this.pwd);
            }
            public bool Register()
            {
                return UserController.register(this.userName, this.pwd);
            }
        }
    }
