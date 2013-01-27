using UnityEngine;
using System.Collections;

public class Baby : MonoBehaviour {
	
    public enum BabyState
    {
        ASLEEP,
        ANGRY,
        AWAKE
    }

    public TouchManager touchManager;
    public ForceIndicator forceIndicator;
	public GameObject babyBody;
    public Material asleepMaterial;
    public Material angryMaterial;
    public Material awakeMaterial;


    private BabyState babyState = BabyState.ASLEEP;
    private float timeInState = 0;
    private float startTime;

	// Use this for initialization
	void Start () {
	    startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        timeInState += Time.deltaTime;
        bool touchedCandy = touchManager.BadTouchCandy;


        if (touchManager.BadTouchCandy)
        {
            touchManager.BadTouchCandy = false;
            State = BabyState.ANGRY;
        }
        else
        {
            switch (State)
            {
                case BabyState.ASLEEP:
                    {
                        if (forceIndicator.GetState() == ForceState.TooLow)
                        {
                            State = BabyState.AWAKE;
                        }
                        else if (forceIndicator.GetState() == ForceState.TooHigh)
                        {
                            State = BabyState.ANGRY;
                        }
                    }
                    break;
                case BabyState.AWAKE:
                    {
                        if (timeInState > 2.0)
                        {
                            if (forceIndicator.GetState() == ForceState.JustRight)
                            {
                                State = BabyState.ASLEEP;
                            }
                            else if (forceIndicator.GetState() == ForceState.TooHigh)
                            {
                                State = BabyState.ANGRY;
                            }
                        }
                    }
                    break;
                case BabyState.ANGRY:
                    {
                        if (timeInState > 2.0)
                        {
                            if (forceIndicator.GetState() == ForceState.JustRight)
                            {
                                State = BabyState.AWAKE;
                            }
                        }
                    }
                    break;
            }
        }
	}

    public BabyState State
    {
        get { return this.babyState; }
        set
        {
            if (babyState != value)
            {
                timeInState = 0;
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

}
