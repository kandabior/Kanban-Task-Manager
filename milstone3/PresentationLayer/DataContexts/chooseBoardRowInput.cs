using milstone3.Interface_Layer;
using milstone3.Interface_Layer.DataObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milstone3.PresentationLayer.DataContexts
{
    class chooseBoardRowInput
    {
    
        private InterfaceLayerChooseBoard board ;
        public string boardId { get; set; }

        public chooseBoardRowInput(string boardId , InterfaceLayerChooseBoard board)
        {
            this.board = board;
      
            this.boardId = boardId;
        }

        //public event PropertyChangedEventHandler PropertyChanged;
    }
}
