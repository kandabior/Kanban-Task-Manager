using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace milstone3.PresentationLayer.DataContexts
{
    class BoardInput : INotifyPropertyChanged
    {

        UserController userController;
        
        string boardId;
        public String BoardId
        {
            get
            {
                return boardId;
            }
            set
            {
                boardId = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("BoardId"));
            }
        }

        string username;
        public String Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("username"));
            }
        }
        public BoardInput(string boardId,string username)
        {
            userController = new UserController();
            this.username = username;
            this.boardId = boardId;

            ShowThread();
        }

        public void ShowThread()
        {
            ObservableCollection<BoardWindowRow> row = new ObservableCollection<BoardWindowRow>();
            ObservableCollection<BoardColumnWindowRow> row2 = new ObservableCollection<BoardColumnWindowRow>();
            foreach(var col in userController.GetBoard(boardId,username).columns)
            {
                row2.Add(new BoardColumnWindowRow(username, col));
                foreach(var task in col.Tasks)
                {
                    row.Add(new BoardWindowRow(username, task));
                }
            }
            tasks = row;
            columns = row2;
        }

        private ObservableCollection<BoardWindowRow> tasks;
        public ObservableCollection<BoardWindowRow> Tasks
        {
            get
            {
                return tasks;
            }
            set
            {
                tasks = value;
                UpdateFilter();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("tasks"));
            }
        }

        private ObservableCollection<BoardColumnWindowRow> columns;
        public ObservableCollection<BoardColumnWindowRow> Columns
        {
            get
            {
                return columns;
            }
            set
            {
                columns = value;
                UpdateFilter2();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("columns"));
            }
        }

        private void UpdateFilter()
        {
            CollectionViewSource cvs = new CollectionViewSource() { Source = tasks };
            ICollectionView cv = cvs.View;
            cv.Filter = o =>
            {
                BoardWindowRow p = o as BoardWindowRow;
                return (p.title.ToUpper().Contains(SearchTerm.ToUpper()) | p.description.ToUpper().Contains(SearchTerm.ToUpper()));
            };
            GridView = cv;
        }

        private void UpdateFilter2()
        {
            CollectionViewSource cvs2 = new CollectionViewSource() { Source = columns };
            ICollectionView cv2 = cvs2.View;
            cv2.Filter = o =>
            {
                BoardColumnWindowRow p = o as BoardColumnWindowRow;
                return (p.columnId.ToUpper().Contains(SearchTerm.ToUpper()));
            };
            ColumnGridView = cv2;
        }

        int maxTask;
        public int MaxTask
        {
            get
            {
                return maxTask;
            }
            set
            {
                maxTask = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("maxTask"));
            }
        }

        string searchTerm = "";
        public string SearchTerm
        {
            get
            {
                return searchTerm;
            }
            set
            {
                searchTerm = value;
                UpdateFilter();
                UpdateFilter2();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("SearchTerm"));
            }
        }

        
        private BoardWindowRow selected;
        public BoardWindowRow Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Selected"));
            }
        }

        private BoardColumnWindowRow selectedColumn;
        public BoardColumnWindowRow SelectedColumn
        {
            get { return selectedColumn; }
            set
            {
                selectedColumn = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("selectedColumn"));
            }
        }

        string columnTitle = "";
        public string ColumnTitle
        {
            get
            {
                return columnTitle;
            }
            set
            {
                columnTitle = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("coulmnTitle"));
            }
        }

        private ICollectionView gridView;
        public ICollectionView GridView
        {
            get
            {
                return gridView;
            }
            set
            {
                gridView = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("GridView"));
            }
        }

        

        private ICollectionView columnGridView;
        public ICollectionView ColumnGridView
        {
            get
            {
                return columnGridView;
            }
            set
            {
                columnGridView = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("columnGridView"));
            }
        }

        public UserController UserController { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
