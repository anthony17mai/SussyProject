using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//observes a certain player's hand
public class PlayerHand : CardContainer
{
    //determines the shape of the hand
    [System.Serializable]
    public struct Parameters
    {
        [System.Serializable]
        public struct PRS
        {
            public Vector2 position;
            public float rotation;
            public float scale;
        }

        public PRS nullState;
        public PRS firstDerivitive;
        public PRS secondDerivitive;
    }

    public Parameters parameters;
    public string layerName;


    //the field to observe
    public FieldReference field;

    //A card may be removed from this container but still belong to it.
    public List<GenericCard> physicalCards;

    //start
    private int layerID;

    private void Start()
    {
        layerID = SortingLayer.NameToID(layerName);
    }

    public void PositionCards()
    {
        //take each card and position them according to their position in the hand
        int l = physicalCards.Count;
        for (int i = 0; i < l; i++)
        {
            Transform target = physicalCards[i].GetCanvasProxy().transform;

            float x;
            if ((l & 1) == 1)
            {
                int nil = l / 2;
                x = i - nil;
            }
            else
            {
                int lnil = l / 2;
                x = i - lnil + 0.5f;
            }

            Vector2 position = transform.position;
            position += parameters.nullState.position;
            position += x * parameters.firstDerivitive.position;
            position += 0.5f * x * x * parameters.secondDerivitive.position;

            float rotation = parameters.nullState.rotation;
            rotation += x * parameters.firstDerivitive.rotation;
            rotation += 0.5f * x * x * parameters.secondDerivitive.rotation;

            float scale = parameters.nullState.scale;
            scale += x * parameters.firstDerivitive.scale;
            scale += 0.5f * x * x * parameters.secondDerivitive.scale;

            target.position = position;
            physicalCards[i].GetCanvasProxy().SetRotation(rotation);
            target.localScale = Vector2.one * scale;

            physicalCards[i].UniversalSortingLayer.Set(layerID, i);
        }
    }

    protected override void Hook(GenericCard card)
    {
        physicalCards.Add(card);
        PositionCards();
    }

    protected override void Unhook(GenericCard card)
    {
        physicalCards.Remove(card);
        PositionCards();
    }
}
