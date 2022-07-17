using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExposedDimensions : MonoBehaviour
{
    //called when either scale factor changes or when transform changes
    public UnityEvent OnModify;

    public Vector2 GetDimensions()
    {
        return (transform as RectTransform).sizeDelta * transform.localScale;
    }
    public Vector2 GetScreenDimensions()
    {
        return GetDimensions() * GameController.gameController.scaleFactor.GetScaleFactor();
    }

    void Broadcast()
    {
        OnModify.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameController.gameController.scaleFactor.OnModify.AddListener(Broadcast);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.hasChanged)
        {
            Broadcast();
            transform.hasChanged = false;
        }
    }
}
