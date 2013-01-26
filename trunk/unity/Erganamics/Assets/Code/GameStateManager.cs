using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {

    private Settings.GameState currentState = Settings.GameState.Title;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    bool changeStateTo(Settings.GameState newState)
    {
        Debug.Log("I'm supposed to change state to " + newState);
        return false;
    }


}
