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
        
    }

    public abstract class Agent
    {
        public Deck deck;
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

    public class Deck
    {
        Multiset<Card> cards;
    }


}
