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
        private string boardId;// name of board
        private string userName;
        private int boardIndex;
        private static int index;
        private static List<Board> boards = new List<Board>();
        
        private Board(int index,string userName, String boardId)
        {
            this.boardIndex = index;
            this.userName = userName;
            this.boardId = boardId;
            columnList = new List<Column>();
        }

        public static Board CreateBoard(string userName, String boardId)
        {
            
            Board board = new Board(index,userName, boardId);
            board.addNewColumn("backlog", board.boardId);
            board.addNewColumn("inProccess", board.boardId);
            board.addNewColumn("done", board.boardId);
            boards.Add(board);
            BoardHandler.addBoard(index,userName,boardId);
            index++;
            return board;
        }

        public IReadOnlyCollection<Column> getColumns
        {
            get { return columnList.AsReadOnly(); }
        }

        public bool addNewColumn(string columnId,string boardId)
        {
            if (columnId == null | columnId == "" )
            {
                log.Error("input not valid");
                return false;
            }
            foreach (Column c in columnList)
                if (c.getColumnId() == columnId)
                {
                    log.Error(boardId +": column title already exist");
                    return false;
                }
            Column column = Column.creatColumn(userName, columnId,boardId);
            columnList.Add(column);
            log.Info("column was added to the board: " + boardId);
            return true;
        }

        public static void datBoardToBoard()
        {
            List<DatBoard> datboards = BoardHandler.getBoard();
            Column.datColToCol();
            if (datboards != null)
            {
                foreach (DatBoard dat in datboards)
                {
                    Board tmpBoard = new Board(dat.getIndex(),dat.getUserName(), dat.getDatBoardId());
                    tmpBoard.columnList = Column.getColumnById(tmpBoard.boardId , tmpBoard.userName);
                    boards.Add(tmpBoard);
                }

            }
            index = BoardHandler.getIndex();
           
        }

        public static List<Board> getListBoardById(string userName)
        {
            List<Board> outputBoards = new List<Board>();
            foreach (Board board in boards)
            {
                if (board.userName == userName )
                    outputBoards.Add(board);
            }
            return outputBoards;
        }

        public string getBoardId()
        {
            return this.boardId;
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

        public static bool removeBoard(Board b, string userName)
        {
            boards.Remove(b);
            if (BoardHandler.deletBoard(b.boardIndex,userName, b.boardId))
            {
                foreach(Column c in b.columnList)
                {
                    Column.DeleteColumn(c,userName);
                }
                log.Info("board deleted successfuly: " + b.boardId);
                return true;
            }

           
            log.Error("could delete the board: " + b.boardId);
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
            int i;
            for (i = 0; i < columnList.Count; i++)
            {
                if (columnList[i].getColumnId() == column.getColumnId())
                {
                    break;
                }
            }

            if (i==columnList.Count()-1)
            {
                log.Error("attempt to change state for task in the final column");
                Console.WriteLine("attempt to change state for task in the final column");
                return false;
            }
            

            if (columnList.ElementAt(i+1).checkMaxTask())
            {
                if (columnList.ElementAt(i + 1).addExistingTask(taskId))
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
                if (column == columnList.ElementAt(columnList.Count - 1)) { 
                    log.Error("can't chenge max task for the last column");
                    return false;
                }
                if (column.getColumnId() == columnId)
                {
                    if (number == -1)
                    {
                        column.setMaxTasks(int.MaxValue);
                        return true;
                    }
                    column.setMaxTasks(number);
                    return true;
                }
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

        public bool DelelteColumn(int columnI, string username)
        {
            if (columnList.Count == 1)
            {
                log.Error("username: " + username + ", cant delete the last column in the board");
                return false;
            }
            Column column = columnList[columnI];
            if (column == null)
            {
                return false;
            }
            if (columnList.Remove(column))
            {
                if (Column.DeleteColumn(column, username))
                {
                    columnList.ElementAt(columnList.Count - 1).setMaxTasks(int.MaxValue);
                    return true;
                }
            }
            
            return false;

        }

        public bool checkMaxTasks(string columnId, int max)
        {
            foreach (Column col in columnList)
            {
                if (col.getColumnId() == columnId)
                {
                    return col.checkMaxTask(max);
                }
            }
            log.Error("cant find the column by the input: " + columnId);
            return false;
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
            Column.SwapColumns(columnList[current], columnList[current - 1]);
            Column tmp = columnList[current];
            columnList[current] = columnList[current-1];
            columnList[current - 1] = tmp;
            columnList.ElementAt(columnList.Count - 1).setMaxTasks(int.MaxValue);
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
            Column.SwapColumns(columnList[current], columnList[current + 1]);
            Column tmp = columnList[current];
            columnList[current] = columnList[current + 1];
            columnList[current + 1] = tmp;
            columnList.ElementAt(columnList.Count - 1).setMaxTasks(int.MaxValue);
            return true;
        }

        public bool sortTasks()
        {
            foreach(Column column in columnList)
            {
                column.sortTasks();
            }
            log.Info("tasks sorted in board :"+this.boardId);
            return true;
        }

        public int indexOfColumn(String Col)
        {
            for (int i = 0; i < columnList.Count; i++)
            {
                if (columnList[i].getColumnId() == Col)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
