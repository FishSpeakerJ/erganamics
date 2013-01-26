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
        transform.position = new Vector3(amplitude*Mathf.Sin(Time.time), 0, 0);
	}
}
