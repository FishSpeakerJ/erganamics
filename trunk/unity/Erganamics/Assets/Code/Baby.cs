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
    private bool playingLullaby = false;

	// Use this for initialization
	void Start () {
	    startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        timeInState += Time.deltaTime;
        bool touchedCandy = touchManager.BadTouchCandy;
        if (forceIndicator.GetState() == ForceState.JustRight)
        {
            if (!playingLullaby)
            {
                AudioManager.Instance.FadeToMusic(0, "brahmsMusicBox1");
                playingLullaby = true;
            }
        }
        else if (playingLullaby)
        {
            AudioManager.Instance.FadeToMusic(0, null);
            playingLullaby = false;
        }

        if (touchManager.BadTouchCandy)
        {
            touchManager.BadTouchCandy = false;
            State = BabyState.ANGRY;
        }
        else if (touchManager.CandyTooFast)
        {
            touchManager.CandyTooFast = false;
            if (State == BabyState.ASLEEP)
            {
                State = BabyState.AWAKE;
            }
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
                
                if (value == BabyState.ASLEEP)
                {
                    babyBody.renderer.material = asleepMaterial;
                    AudioManager.Instance.FadeToMusic(1, "GGJ13_Theme_Slow");
                }
                else if (value == BabyState.AWAKE)
                {
                    babyBody.renderer.material = awakeMaterial;
                    AudioManager.Instance.FadeToMusic(1, "GGJ13_Theme_Normal");
                    if (babyState == BabyState.ASLEEP)
                    {
                        AudioManager.Instance.PlaySoundEffect("stirring1");
                    }
                }
                else if (value == BabyState.ANGRY)
                {
                    babyBody.renderer.material = angryMaterial;
                    AudioManager.Instance.FadeToMusic(1, "GGJ13_Theme_Fast");
                    AudioManager.Instance.PlaySoundEffect("crying1");
                }
                babyState = value;
            }
        }
    }

}
