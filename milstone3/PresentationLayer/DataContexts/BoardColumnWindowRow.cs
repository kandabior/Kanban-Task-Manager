using milstone3.Interface_Layer.DataObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milstone3.PresentationLayer.DataContexts
{
    class BoardColumnWindowRow
    {
        public InterfaceLayerColumn column;
        public string username;

        public string columnId { get; set; }
        public int maxTask { get; set; }

        public BoardColumnWindowRow(string username, InterfaceLayerColumn column)
        {
            this.column = column;
            this.username = username;
            this.columnId = column.columnId;
            this.maxTask = column.maxTask;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
