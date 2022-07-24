using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasProxy : MonoBehaviour
{
    //used to pass argument to RectTracker when instantiating
    //public static RectTransform instanArgument;

    public GenericCard Card { get; private set; }

    private ExposedDimensions GetDimensions() { return Card.Dimensions; }

    public float rotation;

    private void UpdateSize()
    {
        Vector2 rectsize = GetDimensions().GetScreenDimensions();

        (transform as RectTransform).sizeDelta = rectsize;
        GetComponent<BoxCollider2D>().size = rectsize;
    }
    public void SetRotation(float r)
    {
        transform.rotation = Quaternion.Euler(0, 0, -r);

        rotation = r;
    }

    //called after instantiation
    public void Initialize(GenericCard instanArgument)
    {
        Card = instanArgument;
        if (Card == null)
        {
            Debug.LogError("Target is null");
            return;
        }

        //update size whenever screen dimensions changes
        GetDimensions().OnModify.AddListener(UpdateSize);

        //initialize the size
        UpdateSize();
    }
}
