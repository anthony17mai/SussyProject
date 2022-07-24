using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//spawns and tracks a proxy element in the canvas
public class HasProxy : MonoBehaviour
{
    public GameObject prefab;
    [ReadOnly]
    public CanvasProxy proxy;

    public void SpawnProxy()
    {
        if (proxy != null)
        {
            //proxy already exists
            return;
        }
        if (prefab == null)
        {
            Debug.Log("prefab should be non null");
            return;
        }

        GameObject parent = GameObject.Find("Canvas");
        proxy = Instantiate<GameObject>(prefab, parent.transform).GetComponent<CanvasProxy>();
        proxy.GetComponent<CanvasProxy>().Initialize(GetComponent<GenericCard>());
    }
    public void DeleteProxy()
    {
        if (proxy == null)
        {
            return;
        }
        else
        {
            GameObject.Destroy(proxy);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        SpawnProxy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        if (proxy != null) proxy.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        proxy.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        DeleteProxy();
    }
}

[CustomEditor(typeof(HasProxy))]
public class ProxyEditor : Editor
{ 
    public override void OnInspectorGUI()
    {
        HasProxy hasProxy = (HasProxy)target;

        DrawDefaultInspector();

        GUILayout.Space(50);

        bool update = GUILayout.Button("Spawn");
        if (update)
        {
            hasProxy.SpawnProxy();
        }
    }
}
