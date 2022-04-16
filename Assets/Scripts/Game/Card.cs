using System.IO;
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
}
