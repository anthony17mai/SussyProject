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

        public System.Random rng = new System.Random();

        PlayingField leftField;
        PlayingField rightField;

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

        public CardList (DeckData deckData) {
            foreach (KeyValuePair<CardInstance, uint> pair in deckData.cards)
            {
                for(uint i = pair.Value; i > 0; i--)
                    collection.Add(pair.Key);
            }
        }

        public void Shuffle (System.Random rng)
        {
            var shuffled = collection.OrderBy(item => rng.Next());
            collection = shuffled.ToList<CardInstance>();
        }

        // Check for null if empty
        // Remove also returns card removed, can be used for draw
        public CardInstance RemoveCardTop ()
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

    public abstract class Agent
    {
        public struct CharData {
            public DeckData deckData;
            public int maxHP;
            public int maxMana;
            public int stackSize;
            public int handSize;
        }

        CharData chardata;
    }

    public class Player : Agent
    {
        
    }

    public class Opponent : Agent
    {

    }

    public class DeckData
    {
        public Multiset<Card> cards;
    }


}
