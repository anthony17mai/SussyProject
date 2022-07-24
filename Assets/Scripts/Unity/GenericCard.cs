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

    //the container for the card
    public CardContainer container;

    private HasProxy _proxy;
    public HasProxy Proxy
    {
        get
        {
            if (_proxy == null)
                _proxy = GetComponent<HasProxy>();
            return _proxy;
        }
    }
    private ExposedDimensions _dimensions;
    public ExposedDimensions Dimensions
    {
        get
        {
            if (_dimensions == null)
                _dimensions = GetComponent<ExposedDimensions>();
            return _dimensions;
        }
    }
    private UniversalSortingLayer _usl;
    public UniversalSortingLayer UniversalSortingLayer
    {
        get
        {
            if (_usl == null)
            {
                _usl = GetComponent<UniversalSortingLayer>();
            }
            return _usl;
        }
    }

    [System.NonSerialized]
    public Game.Card card;
    [System.NonSerialized]
    public Game.CardInstance instance;

    public CanvasProxy GetCanvasProxy() => Proxy.proxy;

    void Awake()
    {
        //Initialize these
        _ = Proxy;
        _ = Dimensions;
        _ = UniversalSortingLayer;

        card = Game.CardSerializer.Deserialize(cardFile);
        cardImage.sprite = card.image;
        cardName.text = card.name;
        cardDesc.text = card.description;
        cardCost.text = card.cost.ToString();
    }

    void Start()
    {
        //start the card in it's container - this works because HasProxy runs on Awake()
        container.PlaceCard(this);
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
