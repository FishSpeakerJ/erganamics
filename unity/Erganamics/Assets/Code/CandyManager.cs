using UnityEngine;
using System.Collections;

public class CandyManager : MonoBehaviour {

    public GameObject dreamBubble;
    public GameObject candyBag;
    public GameObject candyToFind;

    public Rect bagRect;

	// Use this for initialization
	void Start () {
        Bounds bagBounds = candyBag.collider.bounds;
        bagRect = new Rect(bagBounds.min.x, bagBounds.max.y, bagBounds.size.x, bagBounds.size.y);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DropCandyOnBag(GameObject candy)
    {
        Destroy(candy);
    }

    public Rect BagRect { get { return this.bagRect; } }
}
