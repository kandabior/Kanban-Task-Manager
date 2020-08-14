using milstone3.PresentationLayer.DataContexts;
using milstone3.Interface_Layer;
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
    /// Interaction logic for EditTaskWindow.xaml
    /// </summary>
    public partial class EditTaskWindow : Window
    {

        //BoardInput boardInput;
        string username;
        editTask et;
        string boardId;

        public EditTaskWindow(string boardId ,string username,BoardWindowRow bwr)
        {
            InitializeComponent();

            this.username = username;
            this.boardId = boardId;
           // boardInput = new BoardInput(username);
            //this.DataContext = boardInput;
            et = new editTask(bwr);
            this.DataContext = et;
            

        }

        

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            BoardWindow window = new BoardWindow(boardId, username);
            window.Show();
            this.Close();
        }

        private void ButtonEdit(object sender, RoutedEventArgs e)
        {
            Edit();
        }   

        private void Edit()
        {
            if (UserController.editTask(boardId, et.EditTaskId, this.username, et.EditTaskTitle, et.EditTaskDecription, et.EditTaskDateTime))
            {
                MessageBox.Show("Task was edit " + et.EditTaskId);
                BoardWindow window = new BoardWindow(boardId, username);
                window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("can't edit this task");
            }
        }
    }
}
