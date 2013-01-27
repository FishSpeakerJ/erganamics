using UnityEngine;
using System.Collections;
using System;

public class GameStateManager : MonoBehaviour {

    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject replayButton;

    public StuffManager stuffManager;

    public Action exitCurrentState { get; set; }
    private Settings.GameState currentState = Settings.GameState.Title;

	// Use this for initialization
	void Start () {
        changeStateTo(currentState);
	}
	
	// Update is called once per frame
	void Update () {
	}

    public bool changeStateTo(Settings.GameState newState)
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
        stuffManager.resetLevel();
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
        this.winScreen.SetActive(true);
        this.replayButton.SetActive(true);
        return true;
    }

    void exitWin()
    {
        this.winScreen.SetActive(false);
        this.replayButton.SetActive(false);
    }

    bool enterLose()
    {
        this.loseScreen.SetActive(true);
        return true;
    }

    void exitLose()
    {
        this.loseScreen.SetActive(false);
    }


}
