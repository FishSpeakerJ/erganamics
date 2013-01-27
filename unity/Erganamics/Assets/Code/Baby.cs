using UnityEngine;
using System.Collections;

public class Baby : MonoBehaviour {
	
    public enum BabyState
    {
        ASLEEP,
        ANGRY,
        AWAKE,
        STARTLED,
        DISTURBED
    }

    public TouchManager touchManager;
    public ForceIndicator forceIndicator;
	public GameObject babyBody;
    public GameObject babyHead;
    public Material asleep1Material;
    public Material asleep2Material;
    public Material asleep3Material;
    public Material angryMaterial;
    public Material halfAwakeMaterial;
    public Material startledMaterial;


    private BabyState babyState = BabyState.ASLEEP;
    private BabyState previousState = BabyState.ASLEEP;
    private float timeInState = 0;
    private bool playingLullaby = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        timeInState += Time.deltaTime;
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
        UpdateTextures();
	}

    private void UpdateTextures()
    {
        if (babyState == BabyState.ASLEEP)
        {
            float animTime = timeInState % 5f;
            if (animTime < 2)
            {
                babyHead.renderer.material = asleep1Material;
            }
            else if (animTime < 2.25)
            {
                babyHead.renderer.material = asleep2Material;
            }
            else if (animTime < 2.5)
            {
                babyHead.renderer.material = asleep3Material;
            }
            else if (animTime < 2.75)
            {
                babyHead.renderer.material = asleep2Material;
            }
            else if (animTime < 3)
            {
                babyHead.renderer.material = asleep1Material;
            }
            else
            {
                babyHead.renderer.material = asleep1Material;
            }
        }
        else if (babyState == BabyState.AWAKE)
        {
            if (timeInState < .5)
            {
                babyHead.renderer.material = halfAwakeMaterial;
            }
            else if (timeInState < 1.25)
            {
                babyHead.renderer.material = asleep1Material;
            }
            else if (timeInState < 2)
            {
                babyHead.renderer.material = halfAwakeMaterial;
            }
            else if (timeInState < 2.5)
            {
                babyHead.renderer.material = asleep1Material;
            }
            else if (timeInState < 3)
            {
                babyHead.renderer.material = halfAwakeMaterial;
            }
            else if (timeInState < 3.25)
            {
                babyHead.renderer.material = asleep1Material;
            }
            else
            {
                babyHead.renderer.material = halfAwakeMaterial;
            }
        }
        else if (babyState == BabyState.ANGRY)
        {
            if (timeInState < 1.5)
            {
                babyHead.renderer.material = startledMaterial;
            }
            else
            {
                babyHead.renderer.material = angryMaterial;
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
                    babyHead.renderer.material = asleep1Material;
                    AudioManager.Instance.FadeToMusic(1, "GGJ13_Theme_Slow");
                }
                else if (value == BabyState.AWAKE)
                {
                    babyHead.renderer.material = startledMaterial;
                    AudioManager.Instance.FadeToMusic(1, "GGJ13_Theme_Normal");
                    if (babyState == BabyState.ASLEEP)
                    {
                        AudioManager.Instance.PlaySoundEffect("stirring1");
                    }
                }
                else if (value == BabyState.ANGRY)
                {
                    babyHead.renderer.material = angryMaterial;
                    AudioManager.Instance.FadeToMusic(1, "GGJ13_Theme_Fast");
                    AudioManager.Instance.PlaySoundEffect("crying1");
                }
                babyState = value;
            }
        }
    }

}
