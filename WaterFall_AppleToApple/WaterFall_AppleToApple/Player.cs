using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterFall_AppleToApple
{
    public class Player
    {
        private int id;
        private string name;
        public int score;
        // Stores the ID of the played card
        // If a player has not played a card yet, cardSelected will be set to -1;
        // This is where we store the Judge's chosen card, since the Judge does not use this value otherwise
        public int cardSelected = -1;
        
        public List<Card> hand;

        public void PlayCard(Card card)
        {
            
        }

    }
}
