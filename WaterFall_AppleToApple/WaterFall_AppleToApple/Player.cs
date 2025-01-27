namespace WaterFall_AppleToApple
{
    public class Player
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Score { get; set; }
        // Stores the ID of the played card
        // If a player has not played a card yet, cardSelected will be set to -1;
        // This is where we store the Judge's chosen card, since the Judge does not use this value otherwise
        public string CardSelected { get; set; } //MongoDB gives unique ids for all the cards as a string. e.g. - "67957b1410b6d9a3c46cb83b"

        public List<Card> hand;

        public Player(int id, string name)
        {
            Id = id;
            Name = name;
            Score = 0;
            CardSelected = "-1";
            hand = new List<Card>();
        }

        public void PlayCard(Card card)
        {
            
        }

        public int GetHandSize()
        {
            return hand.Count;  
        }

    }
}
