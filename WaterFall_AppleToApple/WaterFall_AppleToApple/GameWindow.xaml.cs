using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WaterFall_AppleToApple
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Game game;
        int playedCardCount = 0;
        public GameWindow(Game game)
        {
            InitializeComponent();

            this.game = game;

            LoadPlayersToGrid();
            JudgeGridCards();
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
        public void JudgeGridCards()
        {
            for (int i = 0; i < (game.players.Count - 1); i++)
            {
                JudgeGrid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                });

                JudgeGridFaceDown.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                });



                var centerCard = new Button
                {
                    Name = $"CenterCard{i}",
                    Width = 162,
                    Height = 250,
                    Margin = new Thickness(0, 0, 0, 5),
                    Visibility = Visibility.Hidden,
                    Background = new SolidColorBrush(Colors.Transparent)
                };
                centerCard.Click += OnJudgeCard;



                var cardBorder = new Border
                {
                    Background = Brushes.White,
                    BorderBrush = Brushes.White,
                    BorderThickness = new Thickness(2),
                    CornerRadius = new CornerRadius(5),
                    Width = 162,
                    Height = 250,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Bottom
                };


                StackPanel stackPanel = new StackPanel();

                var titleBorder = new Border
                {
                    BorderBrush = Brushes.Red,
                    BorderThickness = new Thickness(0, 0, 0, 2),
                    Margin = new Thickness(5)
                };

                var cardTitle = new TextBlock
                {
                    Name = $"CenterTitleCard{i}",
                    Text = "Title",
                    FontWeight = FontWeights.Bold,
                    FontSize = 16,
                    Padding = new Thickness(0, 0, 0, 5),
                    TextWrapping = TextWrapping.Wrap
                };

                titleBorder.Child = cardTitle;

                var cardDescription = new TextBlock
                {
                    Name = $"CenterDescriptionCard{i}",
                    Text = "Description",
                    FontSize = 14,
                    Margin = new Thickness(5),
                    TextWrapping = TextWrapping.Wrap
                };

                stackPanel.Children.Add(titleBorder);
                stackPanel.Children.Add(cardDescription);

                cardBorder.Child = stackPanel;

                centerCard.Content = cardBorder;



                JudgeGrid.Children.Add(centerCard);
                Grid.SetColumn(centerCard, i);




                //now for the back side of the card
                var cardImg = new Image
                {
                    Source = new BitmapImage(new Uri("Resources/redAppleBack.png", UriKind.Relative)),
                    Width = 162,
                    Height = 250,
                    Margin = new Thickness(0, 0, 0, 5),
                    Visibility = Visibility.Hidden,
                    Stretch = Stretch.UniformToFill
                };


                JudgeGridFaceDown.Children.Add(cardImg);
                Grid.SetColumn(cardImg, i);


                RegisterName(centerCard.Name, centerCard);
                RegisterName(cardTitle.Name, cardTitle);
                RegisterName(cardDescription.Name, cardDescription);


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
                        DisplayCardWords(hand);

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

        public void DisplayCardWords(List<Card> cards)
        {
            for (int i = 0; i < cards.Count(); i++)
            {
                TextBlock btnTitle = (TextBlock)FindName($"TitleCard{i}");
                TextBlock btnDescription = (TextBlock)FindName($"DescriptionCard{i}");
                btnTitle.Text = cards[i].Title;
                btnDescription.Text = cards[i].Description;
            }

        }


        public void ShowPlacedCards(Card card)
        {

        }

        public void OnCardInHand(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedBtn)
            {

                //Card0    4
                //TitleCard0    9
                //DescriptionCard0  15
                //MessageBox.Show(clickedBtn.Name);

                int num = int.Parse(clickedBtn.Name.Substring(4));


                Card card = game.players[game.currentPlayer].hand[num];
                
                if (game.players[game.currentPlayer].CardSelected == "-1") { 
                game.players[game.currentPlayer].CardSelected = card.Id;


                    // num is for the index of the card you chose not the
                    
                        TextBlock btnTitle = (TextBlock)FindName($"CenterTitleCard{playedCardCount}");
                        TextBlock btnDescription = (TextBlock)FindName($"CenterDescriptionCard{playedCardCount}");
                        Button centerBtn = (Button)FindName($"CenterCard{playedCardCount}");
                        centerBtn.Visibility = (Visibility.Visible);
                        btnTitle.Text = card.Title;
                        btnDescription.Text = card.Description;
                    
                    playedCardCount++;

                    //if (playedCardCount >= game.players.Count)
                    //{
                    //    for (int i = 0; i < game.players.Count - 1; i++) ;
                    //}

                } else
                {
                    MessageBox.Show("You can only play one card");
                }


                //ShowPlacedCards();
            }
        }



        public void OnJudgeCard(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedBtn)
            {

                //CenterCard0    4
                //CenterTitleCard0    9
                //CenterDescriptionCard0  15
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
