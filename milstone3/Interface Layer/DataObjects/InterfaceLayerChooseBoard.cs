using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milstone3.Interface_Layer.DataObjects
{
    public class InterfaceLayerChooseBoard
    {
        public string BoardId { get; private set; }
     

        public InterfaceLayerChooseBoard(string BoardId)
        {
            this.BoardId = BoardId;
            
        }
    }
}
