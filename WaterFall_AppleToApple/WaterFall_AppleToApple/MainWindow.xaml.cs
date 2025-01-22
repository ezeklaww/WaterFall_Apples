using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WaterFall_AppleToApple
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Button btnSubmit = new Button();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Player_Name(object sender, TextChangedEventArgs e)
        {

        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            string tempPlayerName = tbPlayerName.Text;
            Console.WriteLine(tempPlayerName);
            
        }

        private void playerName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}