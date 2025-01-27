using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterFall_AppleToApple
{
    public class Deck
    {
        List<Card> cards;

        public Deck(List<Card> cards)
        {
            this.cards = cards;
        }

        public void Shuffle()
        {

        }

        public Card DrawCard()
        {
            Card card = cards[0];
            cards.Remove(card);
            return card;
        }
    }
}
