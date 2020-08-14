using milstone3.PresentationLayer.DataContexts;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        TaskInput taskinput;
        string username;
        string boardId;

        public AddTaskWindow(string boardId ,  string username)
        {
            InitializeComponent();
            this.boardId = boardId;
            this.username = username;
            taskinput = new TaskInput();
            this.DataContext = taskinput;

        }

       

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            BoardWindow window = new BoardWindow(boardId, username);
            window.Show();
            this.Close();
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            Add();
        }

        private void Add()
        {
            if (UserController.addTask(boardId ,taskinput.Title, taskinput.Description, username, taskinput.DueTime))
            {
                MessageBox.Show("new task was added");
                BoardWindow window = new BoardWindow(boardId, username);
                window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("can't add this task: illegal input/ no more space in the first column");
            }
        }
    }
}
