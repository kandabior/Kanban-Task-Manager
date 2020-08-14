using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using log4net;

namespace milstone3
{
    class ILog
    {
        //Declare an instance for log4net
        private static log4net.ILog Log;

        //making a singleton
        public static log4net.ILog getlogger()
        {
            if (Log == null)
            {
               Log =  LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return Log;
            }

            else
                return Log;
        }
    }
}
