using UnityEngine;
using System.Collections;

public class Baby : MonoBehaviour {
	
    public enum BabyState
    {
        ASLEEP,
        ANGRY,
        AWAKE
    }

	public GameObject babyBody;
    public Material asleepMaterial;
    public Material angryMaterial;
    public Material awakeMaterial;


    private BabyState babyState = BabyState.ASLEEP;
    private float startTime;

	// Use this for initialization
	void Start () {
	    startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - startTime > 2f)
        {
            startTime = Time.time;
            if (babyState == BabyState.ASLEEP)
            {
                SetBabyState(BabyState.AWAKE);
            }
            else if (babyState == BabyState.AWAKE)
            {
                SetBabyState(BabyState.ANGRY);
            }
            else if (babyState == BabyState.ANGRY)
            {
                SetBabyState(BabyState.ASLEEP);
            }
        }
	}

    public void SetBabyState(BabyState state)
    {
        babyState = state;
        if (babyState == BabyState.ASLEEP)
        {
            babyBody.renderer.material = asleepMaterial;
        }
        else if (babyState == BabyState.AWAKE)
        {
            babyBody.renderer.material = awakeMaterial;
        }
        else if (babyState == BabyState.ANGRY)
        {
            babyBody.renderer.material = angryMaterial;
        }
    }
}
