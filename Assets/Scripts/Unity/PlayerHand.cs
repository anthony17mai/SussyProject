using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//observes a certain player's hand
public class PlayerHand : AbstractContainer
{
    //the field to observe
    public FieldReference field;

    public Vector3 Center;

    public List<GenericCard> cards;

    public override void emplace(GenericCard card)
    {
        throw new System.NotImplementedException();
    }

    public override void remove(GenericCard card)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
