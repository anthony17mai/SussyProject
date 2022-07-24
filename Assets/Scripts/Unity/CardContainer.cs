using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardContainer : MonoBehaviour
{
    //ICardInterface
    /*
    public void DropCard(GenericCard card)
    {
        //Notify the PlayerInterface
        GameController.gameController.playerInterface.CardPlayed(card);
    }
    */

    public void Pickup(GenericCard card)
    {
        Unhook(card);
    }

    public void PlaceCard(GenericCard card)
    {
        card.container = this;
        Hook(card);
    }

    protected virtual void Hook(GenericCard card)
    {
        card.GetCanvasProxy().transform.SetParent(transform);
    }
    protected virtual void Unhook(GenericCard card)
    {
        card.GetCanvasProxy().transform.SetParent(transform.parent);
    }
}
