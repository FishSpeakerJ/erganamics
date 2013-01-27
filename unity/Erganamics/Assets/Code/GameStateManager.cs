using UnityEngine;
using System.Collections;
using System;

public class GameStateManager : MonoBehaviour {
    public GameObject replayButton;
    public GameObject blackCurtain;
    public GameObject startScreen;
    public Baby baby;
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
        startScreen.SetActive(true);
        return true;
    }

    void exitTitle()
    {
    }

    bool enterMainGame()
    {
        startScreen.SetActive(false);
        baby.ResetBaby();
        return true;
    }

    void exitMainGame()
    {
    }

    bool enterWin()
    {
        StartCoroutine( WinSequence() );
        return true;
    }

    void exitWin()
    {
        replayButton.SetActive( false );
        blackCurtain.SetActive( false );
    }

    bool enterLose()
    {
        return true;
    }

    void exitLose()
    {
    }

    private IEnumerator WinSequence() {
        // -- show win screen
        // -- fade to black
        // -- show replay button
        yield return new WaitForSeconds(.5f);
        baby.ShowFinalBaby();
        yield return new WaitForSeconds( 2f );

        yield return StartCoroutine( FadeToBlack.Instance.FadeOut( 3f ) );

        // Switch from FadeToBlack (which uses OnGUI) to a black curtain,
        // so we can show the replay button on top.
        blackCurtain.SetActive( true );
        FadeToBlack.Instance.alpha = 0f;

        replayButton.SetActive( true );
    }

}
