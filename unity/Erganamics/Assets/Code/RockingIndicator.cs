using UnityEngine;
using System.Collections;
using Erganamics;

public class RockingIndicator : MonoBehaviour {
    public Transform ring;
    public Transform dot;
    public Transform gyroDot;
    public Transform compassDot;
    public float cycleDuration;
    public float lastPortion;
    public float compassCenter;
    public float compassRange = 1.5f;

	// Use this for initialization
	private void Start() {
	}
	
	// Update is called once per frame
	private void Update() {
        var quadWidth = ring.localScale.x;
        var quadHeight = ring.localScale.y;
        var span = Util.screenWidth/2f;
        var t = Time.time % cycleDuration;
        var portion = t / cycleDuration;

        var ringX = Util.screenWidth / 2f + (span / 2f) * Mathf.Sin( Time.time*2f*Mathf.PI/cycleDuration );
        Debug.Log( string.Format( "ringX: {0}", ringX ) );
        ring.position = new Vector3( ringX, Util.screenHeight - quadHeight, 0 );

        //var dotX = span * Mathf.Abs( (Input.acceleration.x + 1f) / 2f );
        //dot.position = new Vector3( dotX, Util.screenHeight - quadHeight, 0 );

        var compassDotX = Util.screenWidth / 2f + (span / 2f) * -((Input.compass.rawVector.x - compassCenter) / (compassRange / 2f));
        compassDot.position = new Vector3( compassDotX, Util.screenHeight - quadHeight, 0 );

        //if( Input.GetButtonDown( "Fire1" ) ) {
        //    // Construct a ray from the current mouse coordinates
        //    var ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        //    RaycastHit hit;
        //    if( Physics.Raycast( ray, out hit ) ) {
        //        Debug.Log( string.Format( "hit: {0}", hit.collider ) );
        //    }
        //}
    }

    private void OnGUI() {
        GUI.Label( new Rect( 10f, 0f, 1000f, 20f ), string.Format( "rawVector.x: {0}", Input.compass.rawVector.x ) );
        GUI.Label( new Rect( 10f, 20f, 1000f, 20f ), string.Format( "compassCenter: {0}", compassCenter ) );
        GUI.Label( new Rect( 10f, 40f, 1000f, 20f ), string.Format( "compassRange: {0}", compassRange ) );
        //GUI.Label( new Rect( 10f, 60f, 1000f, 20f ), string.Format( "y/x: {0}", Mathf.Atan2( Input.compass.rawVector.y, Input.compass.rawVector.x ) ) );
        //GUI.Label( new Rect( 10f, 80f, 1000f, 20f ), string.Format( "x/z: {0}", Mathf.Atan2( Input.compass.rawVector.x, Input.compass.rawVector.z ) ) );
        //GUI.Label( new Rect( 10f, 100f, 1000f, 20f ), string.Format( "z/y: {0}", Mathf.Atan2( Input.compass.rawVector.z, Input.compass.rawVector.y ) ) );

        if( GUI.Button( new Rect( 1200f, 0f, 80f, 80f ), "Calibrate" ) ) {
            compassCenter = Input.compass.rawVector.x;
        }
    }
}
