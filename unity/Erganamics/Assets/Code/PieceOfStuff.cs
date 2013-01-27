using UnityEngine;
using System.Collections;
using Erganamics;

public class PieceOfStuff : MonoBehaviour {

    private GameObject quad;

	// Use this for initialization
	void Start () {
        transform.localScale = Settings.Instance.stuffScale;
        float stuffColor = Random.Range(.7f, 1f);
        renderer.material.color = new Color(stuffColor, stuffColor, stuffColor);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
