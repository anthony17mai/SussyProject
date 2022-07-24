using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

//requires mouse
public class UIDragger : MonoBehaviour
{
    [ReadOnly]
    public CanvasProxy target = null;

    public string sortingLayerName;
    [Tooltip("used for card proxies")]
    public LayerMask cardLayerMask;
    [Tooltip("not used yet")]
    public LayerMask containerLayerMask;// not used - Containers are not raycasted on
    [Tooltip("used for when player drops a card")]
    public LayerMask CardInterfaceMask; // used for when player drops a card

    //s
    private int sortingLayerID;

    private void Start()
    {
        sortingLayerID = SortingLayer.NameToID(sortingLayerName);
    }

    public void ClickAction(InputAction.CallbackContext callback)
    {
        float clickValue = callback.ReadValue<float>();

        if (clickValue == 1)
        {
            PickUpObject();
        }
        else if(clickValue == 0)
        {
            DropObject();
        }
    }
    void PickUpObject()
    {
        //pick up object at mouse location
        //TODO: its better to take all of the mouse hits and find the one which is closest to the cursor
        RaycastHit2D hit = Physics2D.Raycast(Mouse.current.position.ReadValue(), Vector2.zero, Mathf.Infinity, cardLayerMask);

        if (hit.transform != null)
        { 
            //Mouse hit a draggable object
            PickUpObject(hit.transform.gameObject);
        }
        else
        {
            //Mouse missed a draggable object
        }
    }
    void PickUpObject(GameObject target)
    {
        //Check if the card has a location it belongs to
        CanvasProxy prox = target.GetComponent<CanvasProxy>();
        GenericCard card = prox.Card;

        if (card.container != null)
        {
            //unhook from container
            card.container.Pickup(card);
        }

        prox.SetRotation(0);
        card.UniversalSortingLayer.Set(sortingLayerID, 0);
        this.target = prox;
    }
    void DropObject()
    {
        if (target != null)
        {
            //Check where the player drops the card
            RaycastHit2D hit = Physics2D.Raycast(Mouse.current.position.ReadValue(), Vector2.zero, Mathf.Infinity, CardInterfaceMask);

            if (hit.transform != null)
            {
                //drop card into an interface
                ICardInterface cardInterface = hit.transform.GetComponent<ICardInterface>();

                if (cardInterface != null)
                {
                    cardInterface.DropCard(target.Card);
                    target = null;
                }
                else
                {
                    throw new System.Exception("No container found on gameobject: " + hit.transform.gameObject.name);
                }
            }
            else
            {
                //release card - place it in it's container
                target.Card.container.PlaceCard(target.Card);
                target = null;
            }
        }
        else
        {
            //not holding a card
        }
    }

    void UpdateTargetPosition()
    {
        target.transform.position = Mouse.current.position.ReadValue();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            UpdateTargetPosition();
        }
    }
}
