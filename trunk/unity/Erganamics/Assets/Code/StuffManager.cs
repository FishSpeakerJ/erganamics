using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Erganamics;

// This will create a pile of stuff (instances of PieceOfStuff) you can sift through
public class StuffManager : MonoBehaviour {

    public GameObject dreamBubble;
    public GameObject candyBag;
    public GameObject candyToFind;
    public GameStateManager gameStateManager;
    public Rect bagRect;

    private List<PieceOfStuff> listOfStuff = new List<PieceOfStuff>();
    private List<PieceOfCandy> listOfCandy = new List<PieceOfCandy>();

	// Use this for initialization
	void Start () {
        // Pre-compute the candy bag bounds
        Bounds bagBounds = candyBag.collider.bounds;
        bagRect = new Rect(bagBounds.min.x, bagBounds.max.y, bagBounds.size.x, bagBounds.size.y);

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

    public Rect BagRect { get { return this.bagRect; } }

    // Update is called once per frame
    void Update()
    {
	
	}

    public void HandleDropPieceOnBag(GameObject piece)
    {
        PieceOfCandy pOc = piece.GetComponent<PieceOfCandy>();
        PieceOfStuff pOs = piece.GetComponent<PieceOfStuff>();
        if (pOc != null)
        {
            listOfCandy.Remove(pOc);
        }
        if (pOs != null)
        {
            listOfStuff.Remove(pOs);
        }
        Destroy(piece);

        if (listOfCandy.Count == 0)
        {
            gameStateManager.changeStateTo(Settings.GameState.Win);
        }
    }
}
