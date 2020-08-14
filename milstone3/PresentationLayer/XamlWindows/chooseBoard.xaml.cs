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
    /// Interaction logic for chooseBoard.xaml
    /// </summary>
    public partial class chooseBoard : Window
    {
        private chooseBoardInput choose;
        

        public chooseBoard(String username)
        {

            InitializeComponent();
            
            choose = new chooseBoardInput(username);
            this.DataContext = choose;
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                chooseBoardRowInput bwr = choose.SelectedBoard;

                BoardWindow win = new BoardWindow(bwr.boardId, choose.Username);
                win.Show();
                this.Close();
            }
            catch (Exception)
            {

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddBoard();
        }

        private void AddBoard()
        {
            try
            {
                if (UserController.addBoard(choose.BoardId, choose.Username))
                {
                    chooseBoard win = new chooseBoard(choose.Username);
                    win.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("board name is illegal");
                }
            }
            catch(Exception)
            {

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            deleteBoard();
        }

        public void deleteBoard()
        {
            try
            {
                UserController.deleteBoard(choose.SelectedBoard.boardId,choose.Username);
                chooseBoard win = new chooseBoard(choose.Username);
                win.Show();
                this.Close();
            }
            catch (Exception)
            {

            }
        }
    }
}
