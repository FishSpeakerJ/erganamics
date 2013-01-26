using UnityEngine;
using System.Collections;

public class BackAndForth : MonoBehaviour {

    public float amplitude = 17.0f;

	// Use this for initialization
	private void Start () {
        amplitude = 4.0f;
	}
	
	// Update is called once per frame
	private void Update () {
        //transform.position = new Vector3(amplitude*Mathf.Sin(Time.time), 0, 0);
        foreach (Touch touch in Input.touches) {
		    if (touch.phase == TouchPhase.Began) {
		    	// Construct a ray from the current touch coordinates
		    	var ray = Camera.main.ScreenPointToRay (touch.position);
		    	if (Physics.Raycast (ray)) {
		    		// Create a particle if hit
                    transform.position = new Vector3(Random.Range(-10.0f, 10.0f), 0, 0);
                    renderer.material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                    AudioSource[] sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
                    sources[Random.Range(0, sources.Length)].Play();
		    	}
		    }
	    }
	}
}
