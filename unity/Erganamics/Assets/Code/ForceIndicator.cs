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

public enum ForceState {
    TooLow,
    TooHigh,
    JustRight,
}

public class ForceIndicator : MonoBehaviour {
    //public Transform ring;
    //public Transform dot;
    //public float targetForce;
    //public float indicatorWidth;
    public float minAmplitude = 0.05f;
    public float maxAmplitude = 0.25f;
    public float sampleInterval = 4f;
    public bool showDebugText;
    public bool showOverrideGUI;

    private List<ForceSample> forceSamples = new List<ForceSample>();

    private ForceState? overrideForceState = null;

	// Use this for initialization
	private void Start() {
	}
	
	// Update is called once per frame
	private void Update() {
        forceSamples.Add( new ForceSample( Time.time, Input.acceleration.x ) );

        float timeoutThreshold = Time.time - sampleInterval;
        while( forceSamples[0].time < timeoutThreshold ) {
            forceSamples.RemoveAt( 0 );
        }

        //var quadWidth = ring.localScale.x;
        //var quadHeight = ring.localScale.y;

        //var averageAmplitudePortion = Mathf.Clamp01( (GetAverageAmplitude() - minAmplitude) / (maxAmplitude - minAmplitude) );
        //var dotX = Util.screenWidth / 2f + (indicatorWidth / 2f) * (averageAmplitudePortion * 2f - 1f);
        //dot.position = new Vector3( dotX, Util.screenHeight - quadHeight, 0 );

        //var targetForcePortion = Mathf.Clamp01( (targetForce - minAmplitude) / (maxAmplitude - minAmplitude) );
        //var ringX = Util.screenWidth / 2f + (indicatorWidth / 2f) * (targetForcePortion * 2f - 1f);
        //ring.position = new Vector3( ringX, Util.screenHeight - quadHeight, 0 );
    }

    public ForceState GetState() {
        if( overrideForceState != null ) {
            return (ForceState)overrideForceState;
        }

        float averageAmplitude = GetAverageAmplitude();
        if( averageAmplitude < minAmplitude ) {
            return ForceState.TooLow;
        } else if( averageAmplitude > maxAmplitude ) {
            return ForceState.TooHigh;
        } else {
            return ForceState.JustRight;
        }
    }

    public float GetAverageForce() {
        if( forceSamples.Count > 0 ) {
            float sum = 0f;
            foreach( var forceSample in forceSamples ) {
                sum += forceSample.force;
            }
            return sum / forceSamples.Count;
        } else {
            return 0f;
        }
    }

    public float GetAverageAmplitude() {
        if( forceSamples.Count > 0 ) {
            float avgForce = GetAverageForce();
            float sum = 0f;
            foreach( var forceSample in forceSamples ) {
                var adjustedSample = Mathf.Abs( forceSample.force - avgForce );
                sum += adjustedSample;
            }
            return sum / forceSamples.Count;
        } else {
            return 0f;
        }
    }

    private void OnGUI() {
        if( showDebugText ) {
            GUI.Label( new Rect( 10f, 0f, 1000f, 20f ), string.Format( "ForceState: {0}", GetState() ) );
            GUI.Label( new Rect( 10f, 20f, 1000f, 20f ), string.Format( "AverageAmplitude: {0}", GetAverageAmplitude() ) );
            GUI.Label(new Rect(10f, 40f, 1000f, 20f), string.Format("Acceleration: {0} {1} {2}", Input.acceleration.x, Input.acceleration.y, Input.acceleration.z));
        }

        if( showOverrideGUI ) {
            if( GUI.Button( new Rect( 10f, 50f, 200f, 45f ), "Override TooLow" ) ) {
                overrideForceState = ForceState.TooLow;
            }
            if( GUI.Button( new Rect( 10f, 100f, 200f, 45f ), "Override TooHigh" ) ) {
                overrideForceState = ForceState.TooHigh;
            }
            if( GUI.Button( new Rect( 10f, 150f, 200f, 45f ), "Override JustRight" ) ) {
                overrideForceState = ForceState.JustRight;
            }
            if( GUI.Button( new Rect( 10f, 200f, 200f, 45f ), "Clear Override" ) ) {
                overrideForceState = null;
            }
        }

        //if( GUI.Button( new Rect( 1200f, 0f, 80f, 80f ), "Sample" ) ) {
        //    AddSample();
        //}
    }
}
