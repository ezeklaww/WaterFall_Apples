using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterFall_AppleToApple
{
    class Game
    {
        public static List<Player> players;
        public List<Card> shownCards;
        public Deck deck;
        // Refers to element in players containing the Judge
        public static int currentJudge;
		// Keeps track of location in the players list
		public static int currentPlayer = 0;

        public void NewGame()
        {
			// Any red and green cards removed from play are returned to their respective decks
			// Set all player names to default and allow players to fill in their names. Each successful name adds to the player count and is stored in the list, up to a maximum of 10 players.
			// Set everyone's point total to 0
			// Each player draws 7 cards
			// Each card drawn is removed from the deck as soon as they are produced (for loop)
			// A Judge is randomly selected from the list of players
		}

		public void ChangeTurn(int selectedCardID)
        {
			// Whoever's card was selected (selectedCard) gets a point (use either switch statement or for loop)
			// Remove played green and red cards from play
			// Everyone except the Judge draws a card
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
    }
}
