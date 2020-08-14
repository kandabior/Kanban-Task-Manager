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
    public class DatBoard
    {
        private String boardId;


        public DatBoard(string boardId)
        {
            this.boardId = boardId;
        }
        public string getDatBoardId()
        {
            return this.boardId;
        }
    }

    class BoardHandler
    {
        public static bool saveBoards(List<DatBoard> boards)
        {
            Stream myFileStream = File.Create("BoardData.bin");
            BinaryFormatter serializes = new BinaryFormatter();
            serializes.Serialize(myFileStream, boards);
            myFileStream.Close();
            return true;
        }

        public static List<DatBoard> getBoard()
        {
            List<DatBoard> output = null;
            if (File.Exists("BoardData.bin"))
            {
                Stream FileStream = File.OpenRead("BoardData.bin");
                BinaryFormatter deserializer = new BinaryFormatter();
                output = ((List<DatBoard>)deserializer.Deserialize(FileStream));
                FileStream.Close();
            }
            return output;
        }
    }
}
