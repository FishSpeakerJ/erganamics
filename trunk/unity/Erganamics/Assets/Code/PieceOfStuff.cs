using UnityEngine;
using System.Collections;
using Erganamics;

public class PieceOfStuff : MonoBehaviour {

    private GameObject quad;
    private int stuffType = 0;

	// Use this for initialization
	void Start () {
        transform.localScale = Settings.Instance.stuffScale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
