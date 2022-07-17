using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//observes a certain player's hand
public class PlayerHand : CardContainer
{
    //the field to observe
    public FieldReference field;

    public Vector3 Center;

    public List<GenericCard> cards;

    protected override void Hook(GenericCard card)
    {
        throw new System.NotImplementedException();
    }

    protected override void Unhook(GenericCard card)
    {
        throw new System.NotImplementedException();
    }
}
