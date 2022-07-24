using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour, ICardInterface
{
    public void DropCard(GenericCard card)
    {
        GameController.Instance.playerInterface.CardPlayed(card);
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
