using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Game;

public class PlayerInterface : Agent
{
    public void CardPlayed(GenericCard card)
    {
        Debug.Log("Game Instance notified of player action");
        // TODO:
    }

    public override IEnumerator PromptAgent(GameInstance game, DecisionData decision)
    {
        // TODO: give the user control of the game - the user can choose to play a card or to pass their turn.
        yield return new WaitForEndOfFrame();
    }
}

[ExecuteInEditMode]
public class GameController : MonoBehaviour
{
    public static GameController _instance;
    public static GameController Instance 
    { 
        get
        {
            if (_instance == null)
                _instance = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            return _instance;
        }
    }

    public ScaleFactor scaleFactor;
    public PlayerInterface playerInterface = new PlayerInterface();

    private void Awake()
    {
        _ = Instance;
        scaleFactor = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<ScaleFactor>();
    }
}
