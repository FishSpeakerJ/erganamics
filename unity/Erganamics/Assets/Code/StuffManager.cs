using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Erganamics;

// This will create a pile of stuff (instances of PieceOfStuff) you can sift through
public class StuffManager : MonoBehaviour {

    public GameObject candyArea;
    public GameObject candyBag;
    public GameObject bagGlow;
    public GameStateManager gameStateManager;
    public Material candyMaterial;
    public Rect bagRect;

    private List<PieceOfStuff> listOfStuff = new List<PieceOfStuff>();
    private List<PieceOfCandy> listOfCandy = new List<PieceOfCandy>();

	// Use this for initialization
	void Start () {
        //resetLevel();
    }

    public void resetLevel()
    {
        if (listOfCandy.Count > 0 || listOfStuff.Count > 0)
        {
            foreach (PieceOfStuff p in listOfStuff)
            {
                Object.Destroy(p.gameObject);
            }
            foreach (PieceOfCandy p in listOfCandy)
            {
                Object.Destroy(p.gameObject);
            }
        }
        // Pre-compute the candy bag bounds
        Bounds bagBounds = candyBag.collider.bounds;
        bagRect = new Rect(bagBounds.min.x, bagBounds.max.y, bagBounds.size.x, bagBounds.size.y);

        float minX = candyArea.transform.position.x;
        float maxX = minX + candyArea.collider.bounds.size.x;
        float minY = candyArea.transform.position.y;
        float maxY = minY + candyArea.collider.bounds.size.y;

        Debug.Log(maxX + ", " + maxY);

        // Make the pieces of stuff
        for (int i = 0; i < Settings.Instance.piecesOfStuff; i++)
        {
            var quad = Util.CreateQuadAtRuntime();
            quad.name = string.Format("PieceOfStuff{0}", i);
            quad.transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Settings.Instance.stuffZ + i*.1f);
            listOfStuff.Add(quad.AddComponent<PieceOfStuff>());

        }
        // Make the pieces of candy
        for (int i = 0; i < Settings.Instance.piecesOfCandy; i++)
        {
            var quad = Util.CreateQuadAtRuntime();
            quad.name = string.Format("PieceOfCandy{0}", i);
            quad.transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Settings.Instance.candyZ + i * .1f);
            quad.renderer.material = candyMaterial;
            quad.transform.Rotate(new Vector3(0,0, Random.Range(0f, 360f)));
            listOfCandy.Add(quad.AddComponent<PieceOfCandy>());
        }
	}

    public Rect BagRect { get { return this.bagRect; } }

    // Update is called once per frame
    void Update()
    {
	
	}

    public int getNumberOfCandyPieces()
    {
        return listOfCandy.Count;
    }
    private bool glowActive = false;

    public void SetDropFeedbackVisibility(bool visible)
    {
        glowActive = visible;
        this.bagGlow.SetActive(visible);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10f, 300f, 1000f, 20f), "glowActive "+glowActive);
    }

    public void PutOnTop(GameObject piece)
    {
        float minZ = float.MaxValue;
        foreach (PieceOfCandy p in listOfCandy)
        {
            if (p.gameObject != piece && p.transform.position.z < minZ)
            {
                minZ = p.transform.position.z;
            }
            p.transform.position = new Vector3(p.transform.position.x, p.transform.position.y, p.transform.position.z + .1f);
        }
        foreach (PieceOfStuff p in listOfStuff)
        {
            if (p.gameObject != piece && p.transform.position.z < minZ)
            {
                minZ = p.transform.position.z;
            }
            p.transform.position = new Vector3(p.transform.position.x, p.transform.position.y, p.transform.position.z + .1f);
        }
        Debug.Log(minZ);
        piece.transform.position = new Vector3(piece.transform.position.x, piece.transform.position.y, minZ);
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
