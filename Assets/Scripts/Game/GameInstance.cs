using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Util;

namespace Game {
    public class GameInstance
    {
        public struct PlayingField
        {
            public Agent owner;
            public CardList deck;
            public CardList hand;
            public CardList stack;
        }
        public struct RoundInstance
        {
            public int attack;
            public int def;
        }

        public System.Random rng = new System.Random();

        PlayingField leftField;
        PlayingField rightField;

        // FINISH THIS SECTION
        public void playCard(int cardLocation, bool owner)
        {
            ref PlayingField field = ref leftField;
            if(owner == true)
            {
                field = leftField;
                CardInstance card = field.hand.RemoveCardAt(cardLocation);

            }
            else if(owner == false)
            {
                field = rightField;
            }
        }

        public void endTurn(bool owner)
        {

        }
    }

    public abstract class CardList
    {
        List<CardInstance> collection = new List<CardInstance>();

        /// <summary>
        /// Initializes the Card List using a DeckData.
        /// </summary>
        /// <param name="deckData"> Contains the set of cards to be used in the deck.</param>
        public CardList(DeckData deckData) {
            foreach (KeyValuePair<CardInstance, uint> pair in deckData.cards)
            {
                for(uint i = pair.Value; i > 0; i--)
                    collection.Add(pair.Key);
            }
        }

        /// <summary>
        /// Initializes the Card List to an empty list.
        /// </summary>
        public CardList()
        {
            // lol nothing
        }

        public void Shuffle(System.Random rng)
        {
            var shuffled = collection.OrderBy(item => rng.Next());
            collection = shuffled.ToList<CardInstance>();
        }

        // Check for null if empty
        // Remove also returns card removed, can be used for draw
        public CardInstance RemoveCardTop()
        {
            CardInstance topCard;

            if(collection.Count == 0)
                return null;

            topCard = collection[collection.Count - 1];
            collection.RemoveAt(collection.Count - 1);

            return topCard;
        }

        // If index = 0, bottom of deck
        public CardInstance RemoveCardAt (int index)
        {
            CardInstance card;
            card = collection[index];
            collection.RemoveAt(index);
            return card;
        }

        public void AddCardTop (CardInstance card)
        {
            collection.Add(card);
        }

        public void AddCardAt (CardInstance card, int index)
        {
            collection.Insert(index, card);
        }
    }

    public class DeckData
    {
        public Multiset<Card> cards;
    }


}
