using UnityEngine;
using System.Collections;

public class Baby : MonoBehaviour {
	
	public GameObject babyBody;
	public GameObject babyHead;
    public GameObject babyHeadBlend;
    public float babyState = 0; //0 is calm and 1 is angry


    private float startTime;
	// Use this for initialization
	void Start () {
	    startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	    babyState = Mathf.Cos(Time.time - startTime) * .5f + .5f;
        babyHeadBlend.renderer.material.color = new Color(babyHeadBlend.renderer.material.color.r, babyHeadBlend.renderer.material.color.g, babyHeadBlend.renderer.material.color.b, babyState);
	}
}
