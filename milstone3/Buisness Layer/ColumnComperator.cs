using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milstone3
{
    class ColumnComperator : Comparer<Column>
    {

        public override int Compare(Column x, Column y)
        {
            return (x.getIndex() - y.getIndex());
        }
    }
}