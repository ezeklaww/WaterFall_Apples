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
		private Game game;
        private bool gameReady = false;
        private int playerCount = 0;
        List<string> names = new List<string>();
        public StartupWindow()
        {
            InitializeComponent();
        }

        private void OnSubmit(object sender, RoutedEventArgs e)
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
                var gameWindow = new GameWindow();

                //Zeke will take care of this when GameWindow stuff is pushed
                //var gameWindow = new GameWindow(game);
                //private Game game;
                //public GameWindow(Game game)
                //{
                //    InitializeComponent();

                //    this.game = game;
                //}


                gameWindow.Show();
                this.Close();
            }
        }

        private void GamePrep(string inputText)
        {
            int tempPlayerCount = 0;
            if (int.TryParse(inputText, out tempPlayerCount))
            {
                if (tempPlayerCount < 3 || tempPlayerCount > 8)
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

        // goes into the GameWindow
        // OnClick methods below, after clicking the specific button,
        // it will take you to the game logic in the Game class
        public void onShowHand()
        {
			game.ShowHand();
		}

		public void onRotateClockwise()
        {
            game.RotateClockwise();
		}

		public void onRotateCounterClockwise()
        {
			game.RotateCounterClockwise();
		}

		public void onOK(Card card)
        {
			game.OK(card);
		}

		// Should we also pass in the player that is playing the card?
		public void onSelectCard(Card card, Player player)
        {
			game.SelectCard(card, player);
		}
	}
}