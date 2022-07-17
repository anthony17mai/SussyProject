using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackProxyTransform : MonoBehaviour
{
    private HasProxy Proxy;
    private Camera MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        Proxy = gameObject.GetComponent<HasProxy>();
        MainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Proxy.proxy.transform.hasChanged)
        {
            Vector3 proxyLocation = MainCamera.ScreenToWorldPoint(Proxy.proxy.transform.position);
            transform.position = new Vector3(proxyLocation.x, proxyLocation.y, transform.position.z);
            transform.rotation = Proxy.transform.rotation;

            Proxy.proxy.transform.hasChanged = false;
        }
    }
}
