using UnityEngine;
using System.Collections;
using Erganamics;

public class Heartbeat : MonoBehaviour {
    public float beatLength;  // full cycle in seconds
    public float lastPortion;
    public float timeLeftRed;

	// Use this for initialization
	private void Start() {
	}
	
	// Update is called once per frame
	private void Update() {
        if( Input.GetButtonDown( "Fire1" ) ) {
            // Construct a ray from the current mouse coordinates
            var ray = Camera.main.ScreenPointToRay( Input.mousePosition );
            RaycastHit hit;
            if( Physics.Raycast( ray, out hit ) ) {
                Debug.Log( string.Format( "hit: {0}", hit.collider ) );
            }
        }

        var quadWidth = transform.localScale.x;
        var quadHeight = transform.localScale.y;
        var span = Util.screenWidth - quadWidth;
        var t = Time.time % beatLength;
        var portion = t/beatLength;
        var x = span*Mathf.Abs( portion*2f - 1f );

        bool hitLeft = (lastPortion < 0.5f) && (portion >= 0.5f);
        bool hitRight = (lastPortion > 0.9f) && (portion < 0.1f);
        lastPortion = portion;

        if( hitLeft && (Input.acceleration.x >= 0f) ) {
            timeLeftRed = 1f;
        } else if( hitRight && (Input.acceleration.x <= 0f) ) {
            timeLeftRed = 1f;
        }
        var blueGreen = Mathf.Clamp01( 1f - timeLeftRed );
        timeLeftRed -= Time.deltaTime;

        transform.position = new Vector3( x, Util.screenHeight - quadHeight, 0 );
        //renderer.material.color = new Color( 1f, blueGreen, blueGreen );
        transform.localScale = new Vector3( 100f, 100f * blueGreen, 1f );
	}

    private void OnGUI() {
        GUI.Label( new Rect( Screen.width / 2f, 0f, 100f, 20f ), string.Format( "tilt: {0}", Input.acceleration.x ) );
    }
}
