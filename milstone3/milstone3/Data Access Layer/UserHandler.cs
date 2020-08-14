using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace milstone3
{
    [Serializable]
    public class DatUser
    {
        private String userName;
        private String passWord;
        private String boardId;

        public DatUser(string username, string pass, string boardId)
        {
            this.userName = username;
            this.passWord = pass;
            this.boardId = boardId;
        }
        public string getDatUserName()
        {
            return this.userName;
        }
        public string getDatpassword()
        {
            return this.passWord;
        }
        public string getDatboardId()
        {
            return this.boardId;
        }
    }

    class UserHandler
    {
        public static bool saveUsers(List<DatUser> users)
        {
            Stream myFileStream = File.Create("UserData.bin");
            BinaryFormatter serializes = new BinaryFormatter();
            serializes.Serialize(myFileStream, users);
            myFileStream.Close();
            return true;
        }
        
        public static List<DatUser> getUsers()
        {
            List<DatUser> output =null;
            if (File.Exists("UserData.bin"))
            {
                Stream FileStream = File.OpenRead("UserData.bin");
                BinaryFormatter deserializer = new BinaryFormatter();
                output = ((List<DatUser>)deserializer.Deserialize(FileStream));
                FileStream.Close();
            }
            return output;
        }
    }
}
