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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WaterFall_AppleToApple
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Game game;
        public GameWindow(Game game)
        {
            InitializeComponent();

            this.game = game;

            LoadPlayersToGrid();
            
            UpdateJudge();
        }

        /// <summary>
        /// This will load the players to the bottom-left of the GameWindow, 
        /// assigning the player's name and the show hand btn to the correct grid placement
        /// each button is assigned the OnShowHand onClick method.
        /// </summary>
        public void LoadPlayersToGrid()
        {
            for (int i = 0; i < game.players.Count; i++)
            {
                var playerName = new TextBlock
                {
                    Name = $"player{i}Name",
                    Text = game.players[i].Name,
                    Margin = new Thickness(5, 0, 0, 5),
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 215, 0))
                };
                var playerBtn = new Button
                {
                    Name = $"btnPlayer{i}",
                    Content = "Show Hand",
                    Margin = new Thickness(0, 0, 0, 10),
                    Background = new SolidColorBrush(Color.FromRgb(245, 245, 220))
                };
                playerBtn.Click += OnShowHand;

                switch (i)
                {
                    case 0:
                        Grid.SetRow(playerName, 0);
                        Grid.SetColumn(playerName, 0);
                        Grid.SetRow(playerBtn, 0);
                        Grid.SetColumn(playerBtn, 1);
                       break;
                    case 1:
                        Grid.SetRow(playerName, 1);
                        Grid.SetColumn(playerName, 0);
                        Grid.SetRow(playerBtn, 1);
                        Grid.SetColumn(playerBtn, 1);
                        break;
                    case 2:
                        Grid.SetRow(playerName, 2);
                        Grid.SetColumn(playerName, 0);
                        Grid.SetRow(playerBtn, 2);
                        Grid.SetColumn(playerBtn, 1);
                        break;
                    case 3:
                        Grid.SetRow(playerName, 3);
                        Grid.SetColumn(playerName, 0);
                        Grid.SetRow(playerBtn, 3);
                        Grid.SetColumn(playerBtn, 1);
                        break;
                    case 4:
                        Grid.SetRow(playerName, 0);
                        Grid.SetColumn(playerName, 2);
                        Grid.SetRow(playerBtn, 0);
                        Grid.SetColumn(playerBtn, 3);
                        break;
                    case 5:
                        Grid.SetRow(playerName, 1);
                        Grid.SetColumn(playerName, 2);
                        Grid.SetRow(playerBtn, 1);
                        Grid.SetColumn(playerBtn, 3);
                        break;
                    case 6:
                        Grid.SetRow(playerName, 2);
                        Grid.SetColumn(playerName, 2);
                        Grid.SetRow(playerBtn, 2);
                        Grid.SetColumn(playerBtn, 3);
                        break;
                    case 7:
                        Grid.SetRow(playerName, 3);
                        Grid.SetColumn(playerName, 2);
                        Grid.SetRow(playerBtn, 3);
                        Grid.SetColumn(playerBtn, 3);
                        break;
                }  

                PlayerGrid.Children.Add(playerName);
                PlayerGrid.Children.Add(playerBtn);
                RegisterName(playerBtn.Name, playerBtn);
            }



        }


        /// <summary>
        /// a temp way to show that these buttons are individual.
        /// </summary>
        /// <param name="sender"></param> the object that sent the onClick to this method
        public void OnShowHand(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedBtn)  // if all buttons are "OnShowHand" this will determine which specific btn was clicked
            {

                if (clickedBtn.Content.Equals("Show Hand"))
                {
                    string btnName = clickedBtn.Name;
                    int currentPlayerNum = int.Parse(btnName.Substring(9));
                    List<Card> hand = game.ShowHand(currentPlayerNum);

                    if (hand != null)
                    {
                        clickedBtn.Content = "Hide Hand";
                        GridHand.Visibility = Visibility.Visible;
                        GridHandFaceDown.Visibility = Visibility.Hidden;
                        DisplayHand(hand);

                        for (int i = 0; i < game.players.Count; i++)
                        {
                            if (i == currentPlayerNum) continue;
                            Button tempBtn = (Button)FindName($"btnPlayer{i}");
                            if (tempBtn != null) tempBtn.IsEnabled = false;
                        }
                    } else
                    {
                        MessageBox.Show("The judge can't play any cards this turn");
                    }
                } else if (clickedBtn.Content.Equals("Hide Hand"))
                {
                    clickedBtn.Content = "Show Hand";

                    GridHand.Visibility = Visibility.Hidden;
                    GridHandFaceDown.Visibility = Visibility.Visible;
                    game.HideHand();

                    for (int i = 0; i < game.players.Count; i++)
                    {
                        Button playerBtn = (Button)FindName($"btnPlayer{i}");
                        playerBtn.IsEnabled = true;
                    }
                }




            }
        }

        public void DisplayHand(List<Card> hand)
        {
            for (int i = 0; i < hand.Count(); i++)
            {

                TextBlock btnTitle = (TextBlock)FindName($"TitleCard{i}");
                TextBlock btnDescription = (TextBlock)FindName($"DescriptionCard{i}");

                btnTitle.Text = hand[i].Title;
                btnDescription.Text = hand[i].Description;
            }

        }

        public void OnCardInHand(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedBtn)
            {

                //Card0    4
                //TitleCard0    9
                //DescriptionCard0  15
                MessageBox.Show(clickedBtn.Name);
            }
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


        public void UpdateJudge()
        {
            txtJudge.Text = $"Current Judge: {game.players[game.currentJudge].Name} ({(game.currentJudge + 1)})";
        }


        //the red and green deck have click functionality if you want
        public void ClickGreenDeck(object sender, MouseButtonEventArgs e) 
        {

        }
        public void ClickRedDeck(object sender, MouseButtonEventArgs e)
        {

        }
        
    }
}
