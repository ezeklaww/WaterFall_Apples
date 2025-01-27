using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon.Primitives;
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
    public partial class StartupWindow : Window
    {
        private bool gameReady = false;
        private int playerCount = 0;
        List<string> names = new List<string>();

        const int PLAYER_MIN = 1; //change back to 3 later
        const int PLAYER_MAX = 8;
        public StartupWindow()
        {
            InitializeComponent();
        }

        private void OnClickSubmit(object sender, RoutedEventArgs e)
        {
            string inputText = tbInput.Text;
            if (string.IsNullOrWhiteSpace(inputText))
            {
                MessageBox.Show("Cannot leave empty");
                return;
            }

            if (!gameReady) 
            {
                GamePrep(inputText);
            } else
            { 
                GameReady(inputText);
            }
        }

        private void GameReady(string inputText)
        {
            names.Add(inputText);

            lblOutput.Content = $"Enter a name for player {names.Count + 1}";
            tbInput.Clear();

            if (names.Count >= playerCount)
            {
                Game game = new Game(playerCount, names); //player count, and player names
                //switch windows
                var gameWindow = new GameWindow(game);

                gameWindow.Show();
                this.Close();
            }
        }

        private void GamePrep(string inputText)
        {
            int tempPlayerCount = 0;
            if (int.TryParse(inputText, out tempPlayerCount))
            {
                if (tempPlayerCount < PLAYER_MIN || tempPlayerCount > PLAYER_MAX)
                {
                    MessageBox.Show("Must be between 3 and 8");
                }
                else
                {
                    gameReady = true;
                    playerCount = tempPlayerCount;
                    lblOutput.Content = $"Enter a name for player {names.Count + 1}";
                }
            }
            else
            {
                MessageBox.Show("Must enter a number");
            }
            
            tbInput.Clear();
        }


	}
}