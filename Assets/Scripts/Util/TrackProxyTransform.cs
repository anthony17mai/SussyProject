using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Graphing;
using System;

public class TrackProxyTransform : MonoBehaviour
{
    [SerializeField]
    private MotionParameters motionParameters;
    [SerializeField]
    private MotionParameters rotationParameters;

    private HasProxy Proxy;
    private Camera MainCamera;

    [SerializeField]
    [ReadOnly]
    private Vector2 y;
    [SerializeField]
    [ReadOnly]
    private Vector2 yPrime;
    private Vector2 previousLocation;

    float r;
    float rPrime;
    float previousRotation;


    // Start is called before the first frame update
    void Start()
    {
        Proxy = gameObject.GetComponent<HasProxy>();
        MainCamera = Camera.main;
        previousLocation = MainCamera.ScreenToWorldPoint(Proxy.proxy.transform.position);

        yPrime = Vector2.zero;
        y = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // proxy's position
        Vector2 x;
        // proxy's velocity
        Vector2 xPrime;

        float rot;
        float rotPrime;

        x = MainCamera.ScreenToWorldPoint(Proxy.proxy.transform.position);
        rot = Proxy.proxy.rotation;
        if (Proxy.proxy.transform.hasChanged)
        {
            xPrime = x - previousLocation;
            xPrime /= Time.deltaTime;

            rotPrime = rot - previousRotation;
            rotPrime /= Time.deltaTime;

            Proxy.proxy.transform.hasChanged = false;
        }
        else
        {
            rotPrime = 0;
            xPrime = Vector2.zero;
        }
        previousLocation = x;

        //calculate and constrain k2
        float k2_stable = Time.deltaTime * (Time.deltaTime / 4 + motionParameters.coefficients.x / 2);
        k2_stable = Mathf.Max(k2_stable, motionParameters.coefficients.y);

        y = y + Time.deltaTime * yPrime;
        yPrime = yPrime + Time.deltaTime * (x + motionParameters.coefficients.z * xPrime - y - motionParameters.coefficients.x * yPrime) / k2_stable;

        r = r + Time.deltaTime * rPrime;
        rPrime = rPrime + Time.deltaTime * (rot + rotationParameters.coefficients.z * rotPrime - r - rotationParameters.coefficients.x * rPrime) / rotationParameters.coefficients.y;

        transform.position = y;
        transform.rotation = Quaternion.Euler(0, 0, -r);
    }
}

