using milstone3.Interface_Layer.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milstone3.Interface_Layer
{
    public class InterfaceLayerBoard
    {
        public string BoardId { get; private set; }
        public IReadOnlyCollection<InterfaceLayerColumn> columns { get; private set; }

        public InterfaceLayerBoard(string BoardId, IReadOnlyCollection<InterfaceLayerColumn> Columns)
        {
            this.BoardId = BoardId;
            this.columns = Columns;
        }

    }
}
