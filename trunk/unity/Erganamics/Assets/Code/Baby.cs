using UnityEngine;
using System.Collections;

public class Baby : MonoBehaviour {
	
    public enum BabyState
    {
        ASLEEP,
        ANGRY,
        AWAKE
    }

    public ForceIndicator forceIndicator;
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

        if (forceIndicator.GetState() == ForceState.TooLow)
        {
            State = BabyState.AWAKE;
        }
        else if (forceIndicator.GetState() == ForceState.JustRight)
        {
            State = BabyState.ASLEEP;
        }
        else if (forceIndicator.GetState() == ForceState.TooHigh)
        {
            State = BabyState.ANGRY;
        }
	}

    public BabyState State
    {
        get { return this.babyState; }
        set
        {
            babyState = value;
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

}
