using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardContainer : MonoBehaviour, ICardInterface
{
    //ICardInterface
    public void DropCard(GenericCard card)
    {
        card.Container = this;
        Hook(card);
    }
    public void Pickup(GenericCard card)
    {
        Unhook(card);
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
