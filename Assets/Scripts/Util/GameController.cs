using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GameController : MonoBehaviour
{
    public static GameController gameController;

    public ScaleFactor scaleFactor;

    private void Awake()
    {
        gameController = this;
    }
}
