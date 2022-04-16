using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCard : MonoBehaviour
{
    public Game.Card card;
    public UnityEngine.UI.Image cardImage;
    public UnityEngine.UI.Text cardName;
    public UnityEngine.UI.Text cardDesc;
    public UnityEngine.UI.Text cardCost;

    // Start is called before the first frame update
    void Start()
    {
        cardImage.sprite = card.sprite;
        cardName.text = card.name;
        cardDesc.text = card.description;
        cardCost.text = card.cost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
