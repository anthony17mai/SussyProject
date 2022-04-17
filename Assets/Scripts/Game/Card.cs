using System.IO;
using System.Collections.Generic;
using static UnityEngine.JsonUtility;

namespace Game
{
    class CardSerializer
    {
        public static Card Deserialize(UnityEngine.TextAsset txt)
        {
            Card res = FromJson<Card>(txt.text);
            return res;
        }
    }

    [System.Serializable]
    public class Card
    {
        public string Name;
        public string Description;
        public int Cost;
        public string Image;
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

        private CardInstance(Card c)
        {
            Card = c;
            Id = _id_mapping.Count;
            _id_mapping.Add(this);
        }
    }
}
