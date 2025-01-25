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
        public StartupWindow()
        {
            InitializeComponent();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            string tempPlayerCount = tbInput.Text;
            
            tbInput.Clear();
            MessageBox.Show($"there are {tempPlayerCount} players");
            
        }

        public void onShowHand()
        {
			// Needs a way to hide player hands before implementation

			// if cards are face down, Set all cards in <playerID>'s hand to be face up 
			// else set all cards in <playerID>'s hand to be face down
		}

		public void onRotateClockwise()
        {
			// Move to the next player in the list (1, 2, 3, 4...)
			// It is my understanding that the Judge is also stored in the players list, so we are ensuring that the Judge is skipped in the code here

			for (int i = 1; i < Game.players.Count(); i++)
			{
				// if next player in list is not the judge and has NOT played a card
				if ((Game.currentPlayer + i) % Game.players.Count() != Game.currentJudge && Game.players[(Game.currentPlayer + i) % Game.players.Count()].cardSelected == -1)
				{
					// Every time we update Game.currentPlayer, run the result through a modulus of Game.players.Count()
					// This ensures we never exceed the boundries of the list
					Game.currentPlayer = (Game.currentPlayer + i) % Game.players.Count();
					break;
				}
			}
			// Rotate wheel clockwise by (360/(playerCount - 1)) degrees
		}

		public void onRotateCounterClockwise()
        {
			// Move to the previous player in the list (4, 3, 2, 1...)
			// It is my understanding that the Judge is also stored in the players list, so we are ensuring that the Judge is skipped in the code here

			for (int i = 1; i < Game.players.Count(); i++)
			{
				// if next player in list is not the judge and has NOT played a card
				// Take the absolute value of the modulus here to ensure results are greater than or equal to 0
				if (Math.Abs((Game.currentPlayer - i) % Game.players.Count()) != Game.currentJudge && Game.players[Math.Abs((Game.currentPlayer - i) % Game.players.Count())].cardSelected == -1)
				{
					// Every time we update Game.currentPlayer, run the result through a modulus of Game.players.Count()
					// This ensures we never exceed the boundries of the list
					Game.currentPlayer = (Game.currentPlayer + i) % Game.players.Count();
					break;
				}
			}
			// Rotate wheel counterclockwise by (360/(playerCount - 1)) degrees
		}

		public void onOK()
        {
			// Hide and disable the following buttons: RotateClockwise, RotateCounterClockwise, OK
			// Hide the color wheel
			// set value selectedCard to be equal to the Judge's choosen card --- Game.players[currentjudge].cardSelected = cardID;
			// Game.ChangeTurn(Game.players[Game.currentJudge].cardSelected);
		}

		public void onSelectCard(Card card)
        {
			// remove card from hand
			// store the card's cardID in the player class under selectedCard
			// Place card face down in front of the player
		}
	}
}