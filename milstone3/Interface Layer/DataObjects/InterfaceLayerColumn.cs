using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milstone3.Interface_Layer.DataObjects
{
    public class InterfaceLayerColumn
    {
        public string columnId{ get; private set; }
        public int maxTask { get; private set; }
        public IReadOnlyCollection<InterfaceLayerTask> Tasks { get; private set; }

        public InterfaceLayerColumn(string columnId,int maxTask, IReadOnlyCollection<InterfaceLayerTask> Tasks)
        {
            this.columnId = columnId;
            if (maxTask == int.MaxValue)
            {
                this.maxTask = -1;
            }
            else
            {
                this.maxTask = maxTask;
            }
            this.Tasks = Tasks;
        }

    }
}
