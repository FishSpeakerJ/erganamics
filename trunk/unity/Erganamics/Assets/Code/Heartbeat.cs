using UnityEngine;
using System.Collections;

public class Heartbeat : MonoBehaviour {
    public float beatDuration;

	// Use this for initialization
	private void Start() {
	}
	
	// Update is called once per frame
	private void Update() {
        var quadWidth = transform.localScale.x;
        var quadHeight = transform.localScale.y;

        //transform.position = new Vector3( , Screen.height - quadHeight, 0 );
	}
}
