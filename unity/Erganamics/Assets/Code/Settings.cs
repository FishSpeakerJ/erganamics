using UnityEngine;
using System.Collections.Generic;


public class Settings : MonoBehaviour {
    // The first state is what the game will start in
    public enum GameState { Title, TutorialHoldBaby, TutorialRockBaby, TutorialStealCandy, MainGame, PassTablet, Win, Lose };

    public static Settings Instance { get; private set; }
    public float pickupScale = 1.2f;
    public float babyZ = 50.0f;
    public float candyZ = 40.0f;
    public float stuffZ = 20.0f;
    public Vector3 stuffScale = new Vector3(75, 75, 75);
    public Vector3 candyScale = new Vector3(50, 50, 50);
    public Vector2 stuffBoundsLow = new Vector2(40, 40);
    public Vector2 stuffBoundsHigh = new Vector2(1240, 300);
    public int piecesOfCandy = 5;
    public int piecesOfStuff = 80;
    public float babySpeedAwakeThreshold = 1000.0f;
    public Color[] candyColors = new Color[] { new Color(1, 0, 0), new Color(0, 1, 0), new Color(0, 0, 1), new Color(1, 1, 0), new Color(0, 1, 1), new Color(1, 0, 1) };

    // tutorial vars
    public float maxTimeBacklogAllowedForViewingRockingTutorial = 6.0f;  //secs
    public float timeToAddToViewingTimeBacklogForRockingTutorialEachIncident = 2.0f;  //secs
    public float timeSpentInAwakeStateBeforeShowingTutorial = 4.0f;  //secs
    public float timeToFadeInTutorialOver = 1.0f;  // secs
    public float timeToPauseOnEachFrameOfTutorial = 0.6f;  //secs
    public float timeToShowWarningFor = 2.0f;  //secs

    public Material quadMaterial;

    private void Awake() {
        if( Instance == null ) {
            Instance = this;
        } else {
            Debug.LogError( "Attempting to create more than one Settings object." );
        }
    }
}