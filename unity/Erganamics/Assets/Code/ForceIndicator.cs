using UnityEngine;
using System.Collections;
using Erganamics;
using System.Collections.Generic;
using System.Linq;

public struct ForceSample {
    public readonly float time;
    public readonly float force;

    public ForceSample( float time, float force ) {
        this.time = time;
        this.force = force;
    }
}

public class ForceIndicator : MonoBehaviour {
    public Transform ring;
    public Transform dot;
    public float targetForce;
    public float minForce;
    public float maxForce;
    public float sampleInterval;
    public float indicatorWidth;

    public float CurrentForce {
        get {
            if( forceSamples.Count > 0 ) {
                float sum = 0f;
                foreach( var forceSample in forceSamples ) {
                    sum += forceSample.force;
                }
                return sum / forceSamples.Count;
            } else {
                return minForce + (maxForce - minForce) / 2f;
            }
        }
    }

    private List<ForceSample> forceSamples = new List<ForceSample>();

	// Use this for initialization
	private void Start() {
	}
	
	// Update is called once per frame
	private void Update() {
        var quadWidth = ring.localScale.x;
        var quadHeight = ring.localScale.y;

        forceSamples.Add( new ForceSample( Time.time, Mathf.Abs( Input.acceleration.x ) ) );

        float timeoutThreshold = Time.time - sampleInterval;
        while( forceSamples[0].time < timeoutThreshold ) {
            forceSamples.RemoveAt( 0 );
        }

        var currentForcePortion = Mathf.Clamp01( (CurrentForce - minForce) / (maxForce - minForce) );
        var dotX = Util.screenWidth / 2f + (indicatorWidth / 2f) * (currentForcePortion * 2f - 1f);
        dot.position = new Vector3( dotX, Util.screenHeight - quadHeight, 0 );

        var targetForcePortion = Mathf.Clamp01( (targetForce - minForce) / (maxForce - minForce) );
        var ringX = Util.screenWidth / 2f + (indicatorWidth / 2f) * (targetForcePortion * 2f - 1f);
        ring.position = new Vector3( ringX, Util.screenHeight - quadHeight, 0 );
    }

    private void OnGUI() {
        GUI.Label( new Rect( 10f, 0f, 1000f, 20f ), string.Format( "targetForce {0}", targetForce ) );
        GUI.Label( new Rect( 10f, 20f, 1000f, 20f ), string.Format( "CurrentForce {0}", CurrentForce ) );
        //GUI.Label( new Rect( 10f, 40f, 1000f, 20f ), string.Format( "compassRange: {0}", compassRange ) );
        //GUI.Label( new Rect( 10f, 60f, 1000f, 20f ), string.Format( "y/x: {0}", Mathf.Atan2( Input.compass.rawVector.y, Input.compass.rawVector.x ) ) );
        //GUI.Label( new Rect( 10f, 80f, 1000f, 20f ), string.Format( "x/z: {0}", Mathf.Atan2( Input.compass.rawVector.x, Input.compass.rawVector.z ) ) );
        //GUI.Label( new Rect( 10f, 100f, 1000f, 20f ), string.Format( "z/y: {0}", Mathf.Atan2( Input.compass.rawVector.z, Input.compass.rawVector.y ) ) );

        //if( GUI.Button( new Rect( 1200f, 0f, 80f, 80f ), "Sample" ) ) {
        //    AddSample();
        //}
    }
}
