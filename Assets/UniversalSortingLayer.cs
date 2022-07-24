using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class UniversalSortingLayer : MonoBehaviour
{
    [SerializeField]
    public int sortingLayerID;
    [SerializeField]
    private int orderInLayer;

    
    private SpriteRenderer sr;
    private Canvas canvas;

    public void Set(int layerId, int order)
    {
        sortingLayerID = layerId;
        orderInLayer = order;
        BroadcastChange();
    }
    public void BroadcastChange()
    {
        sr.sortingLayerID = sortingLayerID;
        sr.sortingOrder = orderInLayer;
        canvas.sortingLayerID = sortingLayerID;
        canvas.sortingOrder = orderInLayer;
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        canvas = GetComponent<Canvas>();
    }

    void Start()
    {
        BroadcastChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[CustomEditor(typeof(SortingLayer))]
public class SortingLayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        UniversalSortingLayer usl = target as UniversalSortingLayer;
        DrawDefaultInspector();

        GUILayout.Space(50);
        bool update = GUILayout.Button("Update");

        if (update)
        {
            usl.BroadcastChange();
        }
    }
}