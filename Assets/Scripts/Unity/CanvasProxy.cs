using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasProxy : MonoBehaviour
{
    //used to pass argument to RectTracker when instantiating
    //public static RectTransform instanArgument;

    public GenericCard Target { get; private set; }

    private ExposedDimensions GetDimensions() { return Target.Dimensions; }

    private void UpdateSize()
    {
        Vector2 rectsize = GetDimensions().GetScreenDimensions();

        (transform as RectTransform).sizeDelta = rectsize;
        GetComponent<BoxCollider2D>().size = rectsize;
    }

    //called after instantiation
    public void Initialize(GenericCard instanArgument)
    {
        Target = instanArgument;
        if (Target == null)
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
