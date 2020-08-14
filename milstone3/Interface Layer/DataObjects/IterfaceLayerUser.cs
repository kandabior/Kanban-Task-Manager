using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milstone3.Interface_Layer
{
    public class InterfaceLayerUser
    {
        public string Username { get; private set; }
        


        public InterfaceLayerUser(string username)
        {
          
            Username = username;
           
        }
    }
}
