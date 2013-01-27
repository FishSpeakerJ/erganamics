using UnityEngine;
using System.Collections;
using System;

public class GameStateManager : MonoBehaviour {
    public Action exitCurrentState { get; set; }
    private Settings.GameState currentState = Settings.GameState.Title;

	// Use this for initialization
	void Start () {
        changeStateTo(currentState);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    bool changeStateTo(Settings.GameState newState)
    {
        Debug.Log("I'm supposed to change state to '" + newState + "'");
        if (exitCurrentState != null)
        {
            exitCurrentState();
        }
        if (newState == Settings.GameState.Title)
        {
            exitCurrentState = exitTitle;
            return enterTitle();
        }
        else if (newState == Settings.GameState.MainGame)
        {
            exitCurrentState = exitMainGame;
            return enterMainGame();
        }
        else if (newState == Settings.GameState.Win)
        {
            exitCurrentState = exitWin;
            return enterWin();
        }
        else if (newState == Settings.GameState.Lose)
        {
            exitCurrentState = exitLose;
            return enterLose();
        }
        else
        {
            return false;
        }
    }

    bool enterTitle()
    {
        return true;
    }

    void exitTitle()
    {
    }

    bool enterMainGame()
    {
        return true;
    }

    void exitMainGame()
    {
    }

    bool enterWin()
    {
        return true;
    }

    void exitWin()
    {
    }

    bool enterLose()
    {
        return true;
    }

    void exitLose()
    {
    }


}
