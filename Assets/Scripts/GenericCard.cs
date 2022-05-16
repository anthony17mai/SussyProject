using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GenericCard : MonoBehaviour
{
    public TextAsset cardFile;
    private Game.Card card;
    public UnityEngine.UI.Image cardImage;
    public UnityEngine.UI.Text cardName;
    public UnityEngine.UI.Text cardDesc;
    public UnityEngine.UI.Text cardCost;

    // Start is called before the first frame update
    void Start()
    {
        card = Game.CardSerializer.Deserialize(cardFile);
        cardImage.sprite = card.image;
        cardName.text = card.name;
        cardDesc.text = card.description;
        cardCost.text = card.cost.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
