using UnityEngine;
using System.Collections;
using Erganamics;
using System.Collections.Generic;

public class TempoIndicator : MonoBehaviour {
    public Transform ring;
    public Transform dot;
    public float targetTempo;
    public float minTempo;
    public float maxTempo;
    public float lastSampleTime;
    public float currentTempo;
    public float lastAcceleration;

    private List<float> tempoSamples = new List<float>();

	// Use this for initialization
	private void Start() {
	}
	
	// Update is called once per frame
	private void Update() {
        var acceleration = Input.acceleration.x;
        if( (lastAcceleration < 0f) && (acceleration >= 0f) ) {
            AddSample();
        }
        lastAcceleration = acceleration;

        ////var dotX = span * Mathf.Abs( (Input.acceleration.x + 1f) / 2f );
        ////dot.position = new Vector3( dotX, Util.screenHeight - quadHeight, 0 );

        //var compassDotX = Util.screenWidth / 2f + (span / 2f) * -((Input.compass.rawVector.x - compassCenter) / (compassRange / 2f));
        //compassDot.position = new Vector3( compassDotX, Util.screenHeight - quadHeight, 0 );

        //if( Input.GetButtonDown( "Fire1" ) ) {
        //    // Construct a ray from the current mouse coordinates
        //    var ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        //    RaycastHit hit;
        //    if( Physics.Raycast( ray, out hit ) ) {
        //        Debug.Log( string.Format( "hit: {0}", hit.collider ) );
        //    }
        //}
    }

    private void ResetTempo() {
        lastSampleTime = 0f;
        tempoSamples.Clear();
    }

    private void AddSample() {
        if( lastSampleTime != 0f ) {
            tempoSamples.Add( Time.time - lastSampleTime );
        }
        lastSampleTime = Time.time;

        currentTempo = tempoSamples[tempoSamples.Count - 1];
    }

    private void OnGUI() {
        GUI.Label( new Rect( 10f, 0f, 1000f, 20f ), string.Format( "targetTempo {0}", targetTempo ) );
        GUI.Label( new Rect( 10f, 20f, 1000f, 20f ), string.Format( "currentTempo {0}", currentTempo ) );
        //GUI.Label( new Rect( 10f, 40f, 1000f, 20f ), string.Format( "compassRange: {0}", compassRange ) );
        //GUI.Label( new Rect( 10f, 60f, 1000f, 20f ), string.Format( "y/x: {0}", Mathf.Atan2( Input.compass.rawVector.y, Input.compass.rawVector.x ) ) );
        //GUI.Label( new Rect( 10f, 80f, 1000f, 20f ), string.Format( "x/z: {0}", Mathf.Atan2( Input.compass.rawVector.x, Input.compass.rawVector.z ) ) );
        //GUI.Label( new Rect( 10f, 100f, 1000f, 20f ), string.Format( "z/y: {0}", Mathf.Atan2( Input.compass.rawVector.z, Input.compass.rawVector.y ) ) );

        if( GUI.Button( new Rect( 1200f, 0f, 80f, 80f ), "Sample" ) ) {
            AddSample();
        }
    }
}
