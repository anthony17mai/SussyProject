using System.IO;
using System.Collections.Generic;
using static UnityEngine.JsonUtility;

using static Game.GameInstance;

namespace Game
{
    public class CardSerializer
    {
        [System.Serializable]
        public class CardImport
        {
            public string Name;
            public string Description;
            public int Cost;
            public string Image;
            public string Behavior;
        }

        public static Card Deserialize(UnityEngine.TextAsset txt)
        {
            CardImport res = FromJson<CardImport>(txt.text);
            Card ret = new Card(res);
            return ret;
        }
    }

    [System.Serializable]
    public class Card
    {
        public delegate void CardBehavior(GameInstance state, out RoundInstance ownerTraits);
        //temporary
        public static void StrikeBehavior(GameInstance state, out RoundInstance ownerTraits)
        {
            ownerTraits.attack = 7;
            ownerTraits.def = 0;
        }
        public static void DefendBehavior(GameInstance state, out RoundInstance ownerTraits)
        {
            ownerTraits.attack = 0;
            ownerTraits.def = 5;
        }

        public string name;
        public string description;
        public int cost;
        public string image;
        public CardBehavior behavior;

        public Card(CardSerializer.CardImport import) : this(import.Name, import.Description, import.Cost, import.Image, null)
        {
            if (import.Behavior == "Defend")
            {
                this.behavior = DefendBehavior;
            } 
            else if (import.Behavior == "Strike")
            {
                this.behavior = StrikeBehavior;
            }
            else
            {
                //problem - no behavior
            }
        }
        public Card(string name, string description, int cost, string image, CardBehavior behavior)
        {
            this.name = name;
            this.description = description;
            this.cost = cost;
            this.image = image;
            this.behavior = behavior;
        }
    }

    public class CardInstance
    {
        private static List<CardInstance> _id_mapping;

        // factory pattern
        public static CardInstance MakeInstance(Card c) 
        {
            return new CardInstance(c);
        }

        public Card Card { get; private set; }
        public int Id { get; private set; }

        public Card.CardBehavior Behavior { get { return Card.behavior; }}

        private CardInstance(Card c)
        {
            Card = c;
            Id = _id_mapping.Count;
            _id_mapping.Add(this);
        }
    }
}
