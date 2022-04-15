using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class GameInstance
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
        Util.Multiset<Card> cards;
    }


}
