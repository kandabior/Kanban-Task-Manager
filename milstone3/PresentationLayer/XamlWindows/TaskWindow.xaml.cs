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

namespace milstone3.PresentationLayer.XamlWindows
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        string username;
        ShowTaskInput st;
        string boardId;
        public TaskWindow(string boardId ,string username, BoardWindowRow bwr)
        {
            InitializeComponent();
            this.boardId = boardId;
            this.username = username;
            
            st = new ShowTaskInput(username, bwr);
            this.DataContext = st;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BoardWindow win = new BoardWindow(boardId, username);
            win.Show();
            this.Close();
        }
    }
}
