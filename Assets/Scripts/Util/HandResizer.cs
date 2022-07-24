using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[ExecuteInEditMode]
public class HandResizer : MonoBehaviour
{
    public GameObject prototype;
    public int cardCount;

    private void Start()
    {
        SetSize();
        GameController.Instance.scaleFactor.OnModify.AddListener(SetSize);
    }

    public void SetSize()
    {
        GridLayoutGroup grid = GetComponent<GridLayoutGroup>();
        GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        RectTransform rect = prototype.transform as RectTransform;
        Vector2 prototypeSize = controller.scaleFactor.GetScaleFactor() * rect.sizeDelta * rect.localScale;
        Vector2 handSize = prototypeSize;
        handSize.x *= cardCount;

        (transform as RectTransform).sizeDelta = handSize;
        grid.cellSize = prototypeSize;
        collider.size = handSize;
    }
}

[CustomEditor(typeof(HandResizer))]
public class HandResizerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        HandResizer hasProxy = (HandResizer)target;

        DrawDefaultInspector();

        GUILayout.Space(50);

        bool update = GUILayout.Button("Set Size");
        if (update)
        {
            hasProxy.SetSize();
        }
    }
}
