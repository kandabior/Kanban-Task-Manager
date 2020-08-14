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
    class chooseBoardInput : INotifyPropertyChanged
    {
        UserController userController;

        private string username;

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
                    PropertyChanged(this, new PropertyChangedEventArgs("Username"));
            }
        }
        public chooseBoardInput(string username)
        {
            userController = new UserController();
            Username = username;
            ShowThread();
        }

        private string boardId = "";
        public string BoardId
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

        public void ShowThread()
        {
            ObservableCollection<chooseBoardRowInput> row = new ObservableCollection<chooseBoardRowInput>();
            foreach (var board in UserController.GetlistBoards(username))
            {
                row.Add(new chooseBoardRowInput(board.BoardId,board));
            }
            Boards = row; //this.setBoard(row)
            //board = row; this.board = row;
        }


        private ObservableCollection<chooseBoardRowInput> boards;
        public ObservableCollection<chooseBoardRowInput> Boards
        {
            get
            {
                return boards;
            }
            set
            {
                boards = value;
                UpdateFilter();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Boards"));
            }
        }

        private void UpdateFilter()
        {
            CollectionViewSource cvs = new CollectionViewSource() { Source = boards };
            ICollectionView cv = cvs.View;
            cv.Filter = o =>
            {
                chooseBoardRowInput p = o as chooseBoardRowInput;
                return (p.boardId.Contains(SearchTerm.ToUpper()));
            };
            BoardGridView = cv;
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
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("SearchTerm"));
            }
        }

        private chooseBoardRowInput selectedBoard;
        public chooseBoardRowInput SelectedBoard
        {
            get { return selectedBoard; }
            set
            {
                selectedBoard = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedBoard"));
            }
        }

        private ICollectionView boardgridView;
        public ICollectionView BoardGridView
        {
            get
            {
                return boardgridView;
            }
            set
            {
                boardgridView = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("BoardGridView"));
            }
        }


        public UserController UserController { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
