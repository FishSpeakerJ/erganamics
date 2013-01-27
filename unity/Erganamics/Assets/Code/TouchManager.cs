using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Erganamics;

public class TouchManager : MonoBehaviour {

    private class ActiveDrag
    {
        public int touchId;
        public Transform touchObject;
        public Vector3 previousPt;

        public ActiveDrag(int _touchId, Transform _touchObject, Vector3 _previousPt)
        {
            touchId = _touchId;
            touchObject = _touchObject;
            previousPt = _previousPt;
        }
    }

    private Dictionary<int, ActiveDrag> activeDrags = new Dictionary<int, ActiveDrag>();

    public StuffManager stuffManager;
    public Baby baby;
    public GameStateManager gameStateManager;


    public float speed = 0.0f;
    private bool badTouchCandy = false;
    public bool BadTouchCandy
    {
        get { return this.badTouchCandy; }
        set { this.badTouchCandy = value; }
    }

    private bool candyTooFast = false;
    public bool CandyTooFast
    {
        get { return this.candyTooFast; }
        set { this.candyTooFast = value; }
    }

	// Use this for initialization
	void Start () {
	    
	}

    private static bool RectContains(Rect rect, Vector3 pt)
    {
        if (pt.x > rect.xMin && pt.x < rect.xMax)
        {
            if (pt.y < rect.yMin && pt.y > (rect.yMin - rect.height))
            {
                return true;
            }
        }
        return false;
    }

    private void EndDrag(int key, Vector3 pt)
    {
        ActiveDrag drag = activeDrags[key];
        drag.touchObject.gameObject.transform.localScale /= Settings.Instance.pickupScale;
        stuffManager.PutOnTop(drag.touchObject.gameObject);
        if (RectContains(stuffManager.BagRect, pt))
        {
            stuffManager.HandleDropPieceOnBag(drag.touchObject.gameObject);
        }
    }

    private void BeginDrag(ETouch touch, Transform t, Vector3 pt)
    {
        if (baby.State == Baby.BabyState.ASLEEP)
        {
            ActiveDrag drag = new ActiveDrag(touch.fingerId, t, pt);
            activeDrags.Add(touch.fingerId, drag);
            t.gameObject.transform.localScale = t.gameObject.transform.localScale * Settings.Instance.pickupScale;
            t.gameObject.transform.position = new Vector3(t.gameObject.transform.position.x, t.gameObject.transform.position.y, Settings.Instance.stuffZ - 5f);
        }
        else
        {
            badTouchCandy = true;
            baby.startRockingTutorial();
        }

    }

    private bool ContinueDrag(ETouch touch, Vector3 newPt)
    {
        ActiveDrag drag = activeDrags[touch.fingerId];
        Vector2 clickOffset = new Vector2(newPt.x - drag.previousPt.x, newPt.y - drag.previousPt.y);
        drag.touchObject.position = new Vector3(drag.touchObject.position.x + clickOffset.x, drag.touchObject.position.y + clickOffset.y, drag.touchObject.position.z);
        drag.previousPt = newPt;
        speed = clickOffset.magnitude / Time.deltaTime;
        if (speed > Settings.Instance.babySpeedAwakeThreshold && stuffManager.getNumberOfCandyPieces() > 1)
        {
            candyTooFast = true;
        }
        else
        {
            candyTooFast = false;
        }
        if (RectContains(stuffManager.BagRect, newPt))
        {
            return true;
        }
        return false;
    }

    private bool IsDraggable(GameObject g)
    {
        if (g.GetComponent<PieceOfCandy>() != null || g.GetComponent<PieceOfStuff>() != null)
        {
            return true;
        }
        return false;
    }

	// Update is called once per frame
	void Update () {

        // Drop all current drags if the baby wakes up!
        if (baby.State == Baby.BabyState.ANGRY || baby.State == Baby.BabyState.AWAKE)
        {
            foreach (int key in activeDrags.Keys)
            {
                EndDrag(key, activeDrags[key].touchObject.gameObject.transform.position);
            }
            activeDrags.Clear();
        }
        ETouch[] touches = Erganamics.TouchUtils.GetTouches();
        bool isOverBag = false;
        foreach (ETouch t in touches)
        {
            Ray ray = Camera.main.ScreenPointToRay(t.positionV3);

            Vector3 hitPoint = ray.origin;

            if (t.phase == TouchPhase.Ended)
            {
                if (activeDrags.ContainsKey(t.fingerId))
                {
                    EndDrag(t.fingerId, hitPoint);
                    activeDrags.Remove(t.fingerId);
                }
            }
            else if (t.phase == TouchPhase.Began)
            {
                if (activeDrags.ContainsKey(t.fingerId))
                {
                    EndDrag(t.fingerId, hitPoint);
                    activeDrags.Remove(t.fingerId);
                }
                RaycastHit hit;
                bool hasHit = Physics.Raycast(ray, out hit);
                if (hasHit)
                {
                    if (IsDraggable(hit.collider.gameObject))
                    {
                        BeginDrag(t, hit.transform, hitPoint);
                    }
                    else if (hit.collider.gameObject.name == "replayButton")
                    {
                        gameStateManager.changeStateTo(Settings.GameState.Title);
                    }
                }
            }
            else if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary)
            {
                if (activeDrags.ContainsKey(t.fingerId))
                {
                    bool overBag = ContinueDrag(t, hitPoint);
                    if (overBag)
                    {
                        isOverBag = true;
                    }
                }
            }
        }
        stuffManager.SetDropFeedbackVisibility(isOverBag);
	}

    private void OnGUI()
    {
        //GUI.Label(new Rect(10f, 80f, 1000f, 20f), string.Format("Speed: {0}", speed));
    }
}
