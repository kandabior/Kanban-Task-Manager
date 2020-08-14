using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milstone3
{
    class TaskComperator : Comparer<Task>
    {
        public override int Compare(Task task1, Task task2)
        {
            return task1.getDueDate().CompareTo(task2.getDueDate());
        }
    }
}
