using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEditor;

[ExecuteInEditMode]
public class ScaleFactor : MonoBehaviour
{
    [ReadOnly]
    [SerializeField]
    [Tooltip("scaling factor between main camera and canvas")]
    private float scaleFactor = 0;
    private RectTransform canvas;

    public UnityEvent OnModify = new UnityEvent();

    public float GetScaleFactor()
    {
        if (scaleFactor == 0)
        {
            UpdateScaleFactor();
        }
        return scaleFactor;
    }
    public void Rescale()
    {
        UpdateScaleFactor();
        OnModify.Invoke();
    }

    private void UpdateScaleFactor()
    {
        //find the scaling factor
        //scaling factor is the same as the pixels per unit
        
        float camHeight = GetMain().orthographicSize * 2;
        float canvasHeight = canvas.rect.height;
        scaleFactor = canvasHeight / camHeight;
    }
    private Camera GetMain()
    {
        return Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindWithTag("MainCanvas").transform as RectTransform;

        UpdateScaleFactor();
        canvas.hasChanged = false;
    }

    // Update is called once per frame
    void Update()
    {
        // When the Canvas is resized we recalculate the scale factor
        if (canvas.hasChanged)
        {
            UpdateScaleFactor();
            OnModify.Invoke();
            canvas.hasChanged = false;
        }
    }
}

[CustomEditor(typeof(ScaleFactor))]
public class SFEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ScaleFactor sf = (ScaleFactor)target;

        DrawDefaultInspector();

        GUILayout.Space(50);

        bool update = GUILayout.Button("Rescale");
        if (update)
        {
            sf.Rescale();
        }
    }
}