using UnityEngine;
using System.Collections;

public class PieceOfCandy : MonoBehaviour
{
    private int candyType = 0;
    public Vector3 originalPosition = new Vector3(0, 0, 0);

	// Use this for initialization
	void Start () {
        candyType = Random.Range(0, Settings.Instance.candyColors.Length);
        //renderer.material.color = Settings.Instance.candyColors[candyType];
        transform.localScale = Settings.Instance.candyScale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
