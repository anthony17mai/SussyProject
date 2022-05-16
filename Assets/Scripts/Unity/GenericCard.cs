using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GenericCard : MonoBehaviour
{
    public TextAsset cardFile;
    public UnityEngine.UI.Image cardImage;
    public UnityEngine.UI.Text cardName;
    public UnityEngine.UI.Text cardDesc;
    public UnityEngine.UI.Text cardCost;

    //Ill figure these out later
    [System.NonSerialized]
    public Game.Card card;
    [System.NonSerialized]
    public Game.CardInstance instance;

    void Start()
    {
        card = Game.CardSerializer.Deserialize(cardFile);
        //cardImage.sprite = card.sprite;
        cardName.text = card.name;
        cardDesc.text = card.description;
        cardCost.text = card.cost.ToString();
    }

    void Update()
    {
        
    }

    public void Notify(Notification note)
    {
        switch (note)
        {
            case Notification.moveHand:
                break;
            case Notification.moveDeck:
                break;
        }
    }
}
