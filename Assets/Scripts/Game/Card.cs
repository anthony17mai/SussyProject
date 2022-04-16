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
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public string Image { get; set; }
    }
}
