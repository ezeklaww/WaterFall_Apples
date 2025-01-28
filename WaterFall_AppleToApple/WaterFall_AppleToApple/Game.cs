using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MongoDB.Driver;

namespace WaterFall_AppleToApple
{
    public class Game
    {
        const string ConnectionUri = "mongodb+srv://dev:dev@cluster0.arukagi.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
        const int DECK_SIZE = 300;
        const int GREEN_DECK_SIZE = 50;
        const int SCORE_TO_WIN = 5;
        const int MAX_HAND_SIZE = 7;


        public List<Player> players;
        public List<Card> shownCards;
        public Deck deck; //The RedAppleDeck
        private Deck greenDeck; //The GreenAppleDeck
        private List<Card> discardPile; //All used cards are discarded
        // Refers to element in players containing the Judge
        public int currentJudge;
		// Keeps track of location in the players list
		public int currentPlayer = -1;

        public Game(int playerCount, List<string> playerNames)
        {
            NewGame(playerCount, playerNames);
        }

        //public void CreatePlayers(int playerCount, List<string> playerNames)

        public void NewGame(int playerCount, List<string> playerNames)
        {
            // Any red and green cards removed from play are returned to their respective decks
            MongoFillDecks();
            // Set all player names to default and allow players to fill in their names. Each successful name adds to the player count and is stored in the list, up to a maximum of 10 players.
            CreatePlayers(playerCount, playerNames);
            // Each player draws 7 cards
            DrawToSeven();
            // A Judge is randomly selected from the list of players
            var rand = System.DateTime.Now.Second;
            int thisOne = rand % players.Count;
            currentJudge = thisOne;
		}

        public void CreatePlayers(int playerCount, List<string> playerNames)
        {
            players = new List<Player>();

			for (int i = 0; i < playerCount; i++)
			{
				players.Add(new Player((i + 1), playerNames[i])); //player 1, 2...
			}
		}

        /// <summary>
        /// every player will have up to seven cards drawn to them.
        /// </summary>
        public void DrawToSeven()
        {
            for (int i = 0; i < players.Count; i++)
            {
                for (int currentHandSize = players[i].GetHandSize(); currentHandSize < MAX_HAND_SIZE; currentHandSize++)
                {
                    players[i].hand.Add(deck.DrawCard());
                }
            }
        }

		public void ChangeTurn(string selectedCardID)
        {
			// Whoever's card was selected (selectedCard) gets a point (use either switch statement or for loop)
			for (int i = 0; i < players.Count; i++)
			{
				if (players[i].CardSelected.Equals(selectedCardID))
				{
					players[i].Score += 1;
				}
			}
			// Remove played green and red cards from play
			for (int i = 0; i < players.Count(); i++)
			{
				players[i].CardSelected = "-1";
			}
			// Everyone except the Judge draws a card
			for (int i = 0; i < players.Count(); i++)
			{
				if (i != currentJudge)
				{
					// Max card hand is 7 cards
					for (int j = 0; j < 7; j++)
					{
						if (players[i].hand[j] == null)
						{

						}
					}
				}
			}
			// Each card drawn is removed from the deck as soon as they are produced (for loop)

			// Select a new player to be the Judge
			// Draw lines from center of color wheel
			// Start with drawing a line straight up
			// Determine additional divisions with the equation (360/(playerCount - 1))
			// Reveal the color wheel
		}

		public void HideWheel()
        {
			// if all players (minus the Judge) have played a card, hide the color wheel
		}

		public void ShowWheel()
		{
			// Shows the wheel in the UI with updated with red apple players
		}

		public void Update() { }

        // Connects to the database and retrieves the card data,
        // then putting the green and red cards into the apporpiate deck
		public void MongoFillDecks()
        {
            try 
            { 
            var client = new MongoClient(ConnectionUri);
            var database = client.GetDatabase("csc130");
            var collection = database.GetCollection<Card>("AppleToApple");

            var allCards = collection.Find(Builders<Card>.Filter.Empty).ToList();

            List<Card> allGreenCards = allCards.Where(c => c.GreenApple).ToList();  //seperates green and red apple cards into their deck
            List<Card> allRedCards = allCards.Where(c => !c.GreenApple).ToList();


            var random = new Random();
            var shuffledCards = allGreenCards.OrderBy(x => random.Next()).ToList();
            deck = new Deck(shuffledCards.Take(DECK_SIZE).ToList());

            shuffledCards = allRedCards.OrderBy(x => random.Next()).ToList();
            greenDeck = new Deck(shuffledCards.Take(GREEN_DECK_SIZE).ToList());
            } catch (Exception e)
            {
                MessageBox.Show("MongoDB is not working properly, Try again later");
            }

        }


        public List<Card> ShowHand(int playerNumber)
        {
            // Needs a way to hide player hands before implementation

            if (currentPlayer == -1 && currentJudge != playerNumber)
            {
                currentPlayer = playerNumber;
                return players[currentPlayer].hand;
            }


            return null;

            // if cards are face down, Set all cards in <playerID>'s hand to be face up 
            // else set all cards in <playerID>'s hand to be face down
        }
        public void HideHand()
        {
            // Needs a way to hide player hands before implementation

            currentPlayer = -1;
            //UI hides the shown cards

            // if cards are face down, Set all cards in <playerID>'s hand to be face up 
            // else set all cards in <playerID>'s hand to be face down
        }

        public void RotateClockwise()
        {

            // Move to the next player in the list (1, 2, 3, 4...)
            // It is my understanding that the Judge is also stored in the players list, so we are ensuring that the Judge is skipped in the code here

            for (int i = 1; i < players.Count(); i++)
            {
                // if next player in list is not the judge and has NOT played a card
                if ((currentPlayer + i) % players.Count() != currentJudge && players[(currentPlayer + i) % players.Count()].CardSelected.Equals("-1"))
                {
                    // Every time we update Game.currentPlayer, run the result through a modulus of Game.players.Count()
                    // This ensures we never exceed the boundries of the list
                    currentPlayer = (currentPlayer + i) % players.Count();
                    break;
                }
            }
            // Rotate wheel clockwise by (360/(playerCount - 1)) degrees
        }

        public void RotateCounterClockwise()
        {
            // Move to the previous player in the list (4, 3, 2, 1...)
            // It is my understanding that the Judge is also stored in the players list, so we are ensuring that the Judge is skipped in the code here

            for (int i = 1; i < players.Count(); i++)
            {
                // if next player in list is not the judge and has NOT played a card
                // Take the absolute value of the modulus here to ensure results are greater than or equal to 0
                if (Math.Abs((currentPlayer - i) % players.Count()) != currentJudge && players[Math.Abs((currentPlayer - i) % players.Count())].CardSelected.Equals("-1"))
                {
                    // Every time we update Game.currentPlayer, run the result through a modulus of Game.players.Count()
                    // This ensures we never exceed the boundries of the list
                    currentPlayer = (currentPlayer + i) % players.Count();
                    break;
                }
            }
            // Rotate wheel counterclockwise by (360/(playerCount - 1)) degrees
        }

        public void OK(Card card) //EndTurn?
        {
            // Hide and disable the following buttons: RotateClockwise, RotateCounterClockwise, OK

            // Hide the color wheel

            // set value selectedCard to be equal to the Judge's choosen card ---
            players[currentJudge].CardSelected = card.Id;

            ChangeTurn(players[currentJudge].CardSelected);

        }

        // Should we also pass in the player that is playing the card?
        public void SelectCard(Card card, Player player)
        {
            // remove card from hand
            for (int i = 0; i < player.hand.Count(); i++)
            {
                if (card.Id == player.hand[i].Id)
                {
                    // We should consider having the player's hand store just the card's ID rather than card objects. Anything beyond the cardID is only utilized by the UI
                    player.hand[i] = null;
                    break;
                }
            }
            // store the card's cardID in the player class under selectedCard
            player.CardSelected = card.Id;
            // Place card face down in front of the player
        }
    }
}
