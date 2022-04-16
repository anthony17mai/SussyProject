using System.Text.Json;
using System.IO;

namespace Game
{
    class CardSerializer
    { 
        public static Card Deserialize(string fil)
        {
            FileStream fs = File.Open(fil, FileMode.Open);
            Card res = JsonSerializer.Deserialize<Card>(fs);
            return res;
        }
        public static Card Deserialize(UnityEngine.TextAsset txt)
        {
            Card res = JsonSerializer.Deserialize<Card>(txt.text);
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
