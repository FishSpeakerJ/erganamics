using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Erganamics;

// This will create a pile of stuff (instances of PieceOfStuff) you can sift through
public class StuffManager : MonoBehaviour {

    private List<PieceOfStuff> listOfStuff = new List<PieceOfStuff>();
    private List<PieceOfCandy> listOfCandy = new List<PieceOfCandy>();

	// Use this for initialization
	void Start () {
        // Make the pieces of stuff
        for (int i = 0; i < Settings.Instance.piecesOfStuff; i++)
        {
            var quad = Util.CreateQuadAtRuntime();
            quad.name = string.Format("PieceOfStuff{0}", i);
            quad.transform.position = new Vector3(Random.Range(Settings.Instance.stuffBoundsLow[0], Settings.Instance.stuffBoundsHigh[0]), Random.Range(Settings.Instance.stuffBoundsLow[1], Settings.Instance.stuffBoundsHigh[1]), Settings.Instance.stuffZ);
            listOfStuff.Add(quad.AddComponent<PieceOfStuff>());

        }
        // Make the pieces of candy
        for (int i = 0; i < Settings.Instance.piecesOfCandy; i++)
        {
            var quad = Util.CreateQuadAtRuntime();
            quad.name = string.Format("PieceOfCandy{0}", i);
            quad.transform.position = new Vector3(Random.Range(Settings.Instance.stuffBoundsLow[0], Settings.Instance.stuffBoundsHigh[0]), Random.Range(Settings.Instance.stuffBoundsLow[1], Settings.Instance.stuffBoundsHigh[1]), Settings.Instance.candyZ);
            listOfCandy.Add(quad.AddComponent<PieceOfCandy>());
        }

	}

    // Update is called once per frame
    void Update()
    {
	
	}
}
