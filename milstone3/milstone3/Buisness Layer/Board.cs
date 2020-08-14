using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milstone3
{
    class Board
    {
        static log4net.ILog log = ILog.getlogger();

        private List<Column> columnList;
        private string title;
        private static List<Board> boards = new List<Board>();
        private static List<DatBoard> datboards;

        private Board(String title)
        {
            this.title = title;
            columnList = new List<Column>();
        }

        public static Board CreateBoard (String title)
        {
            Board board = new Board(title);
            board.addNewColumn("backlog",board.title);
            board.addNewColumn("inProccess", board.title);
            board.addNewColumn("done", board.title);
            boards.Add(board);
            datboards.Add(new DatBoard(title));
            BoardHandler.saveBoards(datboards);
            return board;
        }

        public bool addNewColumn(string columnId,string boardId)
        {
            if (columnId == null)
            {
                log.Error("input not valid");
                Console.WriteLine("input not valid");
                return false;
            }
            Column column = Column.creatColumn(columnId,boardId);
            columnList.Add(column);
            return true;
        }

        public static void datBoardToBoard()
        {
            Board.datboards= BoardHandler.getBoard();
            Column.datColToCol();
            if (Board.datboards != null)
            {
                foreach (DatBoard dat in Board.datboards)
                {
                    Board tmpBoard= new Board(dat.getDatBoardId());
                    tmpBoard.columnList = Column.getColumnById(tmpBoard.title);
                    boards.Add(tmpBoard);
                }
            }
            else
            {
                Board.datboards = new List<DatBoard>();
            }
        }
       
        public static Board getBoardById(string boardId)
        {
            foreach ( Board board in boards)
            {
                if (board.title == boardId)
                    return board;
            }
            Console.WriteLine("board  doesnt exist");
            log.Error("board doesnt exist");
            return null;
        }

        public string getBoardId()
        {
            return this.title;
        }

        public Boolean addTask(String title, String description, String username, DateTime DueTime)
        {
            if (columnList.ElementAt(0).checkMaxTask())
            {
                return columnList.ElementAt(0).addTask(title, description, username, DueTime);
            }
            else
            {
                return false;
            }

        }

        public Boolean deleteTask(int taskId)
        {
            Column column = Column.getColumnByTaskId(taskId);
            if (column == null)
            {
                return false;
            }
            if (column.deleteTask(taskId))
                return true;
            else
                return false;
            log.Error("task could not be found");
            Console.WriteLine("could not find the column ");
            return false;
        }

        public Boolean editTask(int taskId, String title, String description, DateTime dueDate)
        {
            Column column = Column.getColumnByTaskId(taskId);
            if (column == null)
            {
                return false;
            }
            if (columnList.IndexOf(column) == columnList.Count() - 1)
            {
                log.Error("attempt to edit task at the final column");
                Console.WriteLine("attempt to edit task at the final column");
                return false;
            }
            if (column.editTask(taskId, title, description, dueDate))
                return true;
            else
                return false;
        }

        public Boolean changeState(int taskId)
        {
            Column column = Column.getColumnByTaskId(taskId);
            if (column == null)
            {
                return false;
            }
            if (columnList.IndexOf(column)==columnList.Count()-1)
            {
                log.Error("attempt to change state for task in the final column");
                Console.WriteLine("attempt to change state for task in the final column");
                return false;
            }
            if (columnList.ElementAt(columnList.IndexOf(column) + 1).checkMaxTask())
            {
                if (columnList.ElementAt(columnList.IndexOf(column) + 1).addExistingTask(taskId))
                {
                    if (column.removeTaskFromColumn(taskId))
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
        
        public Boolean setMaxTasks(string columnId,int number)
        {
            foreach (Column column in columnList)
            {
                column.setMaxTasks(number);
                return true;
            }
            log.Error("cant find column by id");
            Console.WriteLine("cant find column by id");
            return false;
        }
        
        public void showTasks(string columnId)
        {
            foreach (Column column in columnList)
            {
                if (column != null)
                    column.showTasks();
            }
            log.Error("column not found");
            Console.WriteLine("column not found");
           
        }

        public bool printTask(int taskId, string columnID)
        {
            foreach(Column column in columnList)
            {
                if (column.getColumnId() == columnID)
                {
                    column.printTask(taskId);
                }
            }
            log.Error("column not found");
            Console.WriteLine("column not found");
            return false;
        }

        
        
        public bool changeOrderLeft(int current)
        {
            if (current == 0)
            {
                log.Error("can't move the column");
                Console.WriteLine("can't move the column");
                return false;
            }
            Column tmp = columnList[current];
            columnList[current] = columnList[current-1];
            columnList[current - 1] = tmp;
            return true;
        }

        public bool changeOrderRight(int current)
        {
            if (current == columnList.Count()-1)
            {
                log.Error("can't move the column");
                Console.WriteLine("can't move the column");
                return false;
            }
            Column tmp = columnList[current];
            columnList[current] = columnList[current + 1];
            columnList[current + 1] = tmp;
            return true;
        }

        public bool sortTasks(string columnId)
        {
            foreach(Column column in columnList)
            {
                if (column.getColumnId()== columnId)
                {
                    column.sortTasks();
                    return true;
                }
            }
            log.Error("column not found");
            Console.WriteLine("column not found");
            return false;
        }
    }
}
