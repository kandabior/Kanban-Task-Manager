using milstone3.PresentationLayer.DataContexts;
using milstone3.PresentationLayer.XamlWindows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;



namespace milstone3
{
    /// <summary>
    /// Interaction logic for BoardWindow.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        BoardInput boardInput;
        String userName;
        string boardId;
         
        public BoardWindow(string boardId , String uN)
        {
            InitializeComponent();
            userName = uN;
            this.boardId = boardId;
            boardInput = new BoardInput(boardId,userName);
            this.DataContext = boardInput;

        }

        private void AddColumn_Click(object sender, RoutedEventArgs e)
        {
            AddColumn();
            
        }

        private void AddColumn()
        {
            try
            {
                if (UserController.addNewColumn(boardId, userName, boardInput.ColumnTitle))
                {
                    MessageBox.Show(this.boardInput.ColumnTitle + " Column added successfuly");
                    BoardWindow win = new BoardWindow(boardId, userName);
                    win.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Column name : " + this.boardInput.ColumnTitle + " is illegal");
            }
            catch (Exception)
            {

            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BoardWindowRow bwr = boardInput.Selected;

                EditTaskWindow win = new EditTaskWindow(boardId, userName, bwr);
                win.Show();
                this.Close();
            }
            catch (Exception)
            {

            }
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
           
            AddTaskWindow win = new AddTaskWindow(boardId,userName);
            win.Show();
            this.Close();
        }

      

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteTask();
            
        }

        private void DeleteTask()
        {
            try
            {
                if (UserController.deleteTask(boardId ,boardInput.Selected.taskId, this.userName))
                {
                    MessageBox.Show("Task number " + this.boardInput.Selected.taskId + " deleted");
                    BoardWindow win = new BoardWindow(boardId, userName);
                    win.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Task number : " + this.boardInput.Selected.taskId + " cant be deleted");
            }
            catch (Exception)
            {

            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserController.logOut(this.userName);
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }


        private void Change_State_Next(object sender, RoutedEventArgs e)
        {
            ChangeStateNext();
            
        }

        private void ChangeStateNext()
        {
            try
            {
                int i = UserController.indexOfColumn(boardId, this.userName, ((BoardColumnWindowRow)columnOrder.SelectedItem).columnId);
                if (i != -1 && UserController.changeOrderRight(boardId,  this.userName, i))
                    MessageBox.Show("State changed " + ((BoardColumnWindowRow)columnOrder.SelectedItem).columnId);
                else
                    MessageBox.Show("State can't change!!!");
                BoardWindow win = new BoardWindow(boardId, userName);
                win.Show();
                this.Close();
            }
            catch (Exception)
            {

            }
        }

        private void Change_State_Back(object sender, RoutedEventArgs e)
        {

            Change_State_Back();
            
        }

        private void Change_State_Back()
        {
            try
            {
                int i = UserController.indexOfColumn(boardId, this.userName, ((BoardColumnWindowRow)columnOrder.SelectedItem).columnId);
                if (i != -1 && UserController.changeOrderLeft(boardId,  this.userName, i))
                    MessageBox.Show("State changed " + ((BoardColumnWindowRow)columnOrder.SelectedItem).columnId);
                else
                    MessageBox.Show("State can't change!!!");
                BoardWindow win = new BoardWindow(boardId, userName);
                win.Show();
                this.Close();
            }
            catch (Exception)
            {

            }
        }

        private void Change_State(object sender, RoutedEventArgs e)
        {
            Change_State();
            

            
        }

        private void Change_State()
        {
            try
            {
                if (UserController.changeState(boardId, boardInput.Selected.taskId, this.userName))
                {
                    MessageBox.Show("Task : " + boardInput.Selected.taskId + " was moved");

                    BoardWindow win = new BoardWindow(boardId, userName);
                    win.Show();
                    this.Close();
                    

                }
                else
                {
                    MessageBox.Show("can't change task state anymore :(");
                }
            }
            catch (Exception)
            {

            }
        }

        private void DeleteColumn_Click(object sender, RoutedEventArgs e)
        {
            DeleteColumnF();
            
        }

        private void DeleteColumnF()
        {
            try
            {
                int i = UserController.indexOfColumn(boardId, this.userName, ((BoardColumnWindowRow)columnOrder.SelectedItem).columnId);
                if (i != -1 && UserController.DeleteColumn(boardId, this.userName, i))
                    MessageBox.Show("Column " + ((BoardColumnWindowRow)columnOrder.SelectedItem).columnId + " was deleted");
                else
                    MessageBox.Show("can't delete column");
                BoardWindow win = new BoardWindow(boardId, userName);
                win.Show();
                this.Close();
            }
            catch (Exception)
            {

            }
        }

        private void SetMaxTask_Click(object sender, RoutedEventArgs e)
        {
            SetMaxTaskF();
        }

        private void SetMaxTaskF()
        {
            try
            {
                if (boardInput.MaxTask!=-1 && !UserController.checkMaxTasks(boardId, this.userName, boardInput.MaxTask, boardInput.SelectedColumn.columnId))
                {
                    MessageBox.Show("there are more tasks than the max input: " + boardInput.SelectedColumn.columnId);
                }
                else
                {
                    if (UserController.setMaxTask(boardId, this.userName, boardInput.MaxTask, boardInput.SelectedColumn.columnId))
                    {
                        MessageBox.Show("set max task for column: " + boardInput.SelectedColumn.columnId);
                        BoardWindow win = new BoardWindow(boardId, userName);
                        win.Show();
                        this.Close();


                    }
                    else
                    {
                        MessageBox.Show("can't set max task for this column");
                    }
                }
            }
            catch (Exception)
            {

            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SortTasks();
        }

        private void SortTasks()
        {
            try
            {
                if (UserController.sortTasks(boardId, userName, this.userName))
                {
                    MessageBox.Show("tasks were sorted successfuly");

                    BoardWindow win = new BoardWindow(boardId, userName);
                    win.Show();
                    this.Close();


                }
                else
                {
                    MessageBox.Show("tasks cant be sorted");
                }
            }
            catch (Exception)
            {

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                BoardWindowRow bwr = boardInput.Selected;

                TaskWindow win = new TaskWindow(boardId, userName, bwr);
                win.Show();
                this.Close();
            }
            catch (Exception)
            {

            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            chooseBoard win = new chooseBoard(userName);
            win.Show();
            this.Close();
        }
    }
}
