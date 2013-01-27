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
    public GameObject babyHead;
    public GameObject finalBaby;
    public GameObject finalBabyAngry;
    public Material asleep1Material;
    public Material asleep2Material;
    public Material asleep3Material;
    public Material angryMaterial;
    public Material halfAwakeMaterial;
    public Material startledMaterial;
    public Material awakeMaterial;

    // tutorial vars
    public GameObject rockingTutorial1;
    public GameObject rockingTutorial2;
    public float timeLeftToDisplayRockingTutorial = 0.0f;
    public bool fadeInTutorial = false;
    public bool tutorialVisible = false;
    public float tutorialFadeTimeCount = 0.0f;
    public float timeOnThisTutorialFrame = 0.0f;
    public int currentTutorialFrame = 0;

    private BabyState babyState = BabyState.ASLEEP;
    private BabyState previousState = BabyState.ASLEEP;
    private float timeInState = 0;
    private bool playingLullaby = false;

	// Use this for initialization
	void Start () {
        rockingTutorial1.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        rockingTutorial2.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
	}
	
    public void startRockingTutorial()
    {
        if (timeLeftToDisplayRockingTutorial > 0)
        {
            return;
        }
        fadeInTutorial = true;
        tutorialVisible = true;
        timeLeftToDisplayRockingTutorial = Mathf.Min(Settings.Instance.maxTimeBacklogAllowedForViewingRockingTutorial, timeLeftToDisplayRockingTutorial + Settings.Instance.timeToAddToViewingTimeBacklogForRockingTutorialEachIncident);
    }

	// Update is called once per frame
	void Update () {
        timeInState += Time.deltaTime;

        // If I've been awake long enough show the tutorial
        if (timeInState > Settings.Instance.timeSpentInAwakeStateBeforeShowingTutorial && (State == BabyState.AWAKE || State == BabyState.ANGRY))
        {
            startRockingTutorial();
        }
        if (tutorialVisible)
        {
            timeLeftToDisplayRockingTutorial -= Time.deltaTime;
            if (fadeInTutorial)
            {
                tutorialFadeTimeCount += Time.deltaTime;
                if (tutorialFadeTimeCount > Settings.Instance.timeToFadeInTutorialOver)
                {
                    fadeInTutorial = false;
                    rockingTutorial1.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    rockingTutorial2.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
                rockingTutorial1.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, tutorialFadeTimeCount / Settings.Instance.timeToFadeInTutorialOver);
                rockingTutorial2.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, tutorialFadeTimeCount / Settings.Instance.timeToFadeInTutorialOver);
            }
            timeOnThisTutorialFrame += Time.deltaTime;
            if (timeOnThisTutorialFrame > Settings.Instance.timeToPauseOnEachFrameOfTutorial)
            {
                if (currentTutorialFrame == 0)
                {
                    rockingTutorial1.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                    rockingTutorial2.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
                else
                {
                    rockingTutorial1.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    rockingTutorial2.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                }
                currentTutorialFrame = 1 - currentTutorialFrame;
                timeOnThisTutorialFrame = 0.0f;
            }
            if (timeLeftToDisplayRockingTutorial < 0)
            {
                tutorialVisible = false;
                fadeInTutorial = false;
            }

        }   

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

    public void ShowFinalBaby()
    {
        this.babyBody.SetActive(false);
        this.babyHead.SetActive(false);
        this.finalBaby.SetActive(true);
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
                if (animTime - Time.deltaTime < 2)
                {
                    AudioManager.Instance.PlaySoundEffect("snoring");
                }
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
                babyHead.renderer.material = awakeMaterial;
            }
            else if (timeInState < 3.25)
            {
                babyHead.renderer.material = halfAwakeMaterial;
            }
            else
            {
                babyHead.renderer.material = awakeMaterial;
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
