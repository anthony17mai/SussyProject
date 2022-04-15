using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Util;

namespace Game {
    public class GameInstance
    {
        public struct PlayingField
        {
            Agent owner;
            CardList deck;
            CardList hand;
        }

    }

    public abstract class CardList
    {
        List<Card> collection = new List<Card>();

        public CardList (DeckData deckData) {
            deckData.cards
        }
    }

    public abstract class Agent
    {
        public DeckData deck;
    }

    public class Player : Agent
    {
        
    }

    public class Opponent : Agent
    {

    }

    public class Card
    {
        
    }

    public class DeckData
    {
        public Multiset<Card> cards;
    }


}
