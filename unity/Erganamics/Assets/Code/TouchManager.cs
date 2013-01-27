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

    private void EndDrag(ETouch touch, Vector3 pt)
    {
        ActiveDrag drag = activeDrags[touch.fingerId];
        activeDrags.Remove(touch.fingerId);
        if (RectContains(stuffManager.BagRect, pt))
        {
            stuffManager.HandleDropPieceOnBag(drag.touchObject.gameObject);
        }
    }

    private void BeginDrag(ETouch touch, Transform t, Vector3 pt)
    {
        ActiveDrag drag = new ActiveDrag(touch.fingerId, t, pt);
        activeDrags.Add(touch.fingerId, drag);
    }

    private void ContinueDrag(ETouch touch, Vector3 newPt)
    {
        ActiveDrag drag = activeDrags[touch.fingerId];
        Vector2 clickOffset = new Vector2(newPt.x - drag.previousPt.x, newPt.y - drag.previousPt.y);
        drag.touchObject.position = new Vector3(drag.touchObject.position.x + clickOffset.x, drag.touchObject.position.y + clickOffset.y, drag.touchObject.position.z);
        drag.previousPt = newPt;
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
        ETouch[] touches = Erganamics.TouchUtils.GetTouches();
        foreach (ETouch t in touches)
        {
            Ray ray = Camera.main.ScreenPointToRay(t.positionV3);

            Vector3 hitPoint = ray.origin;

            if (t.phase == TouchPhase.Ended)
            {
                if (activeDrags.ContainsKey(t.fingerId))
                {
                    EndDrag(t, hitPoint);
                }
            }
            else if (t.phase == TouchPhase.Began)
            {
                if (activeDrags.ContainsKey(t.fingerId))
                {
                    EndDrag(t, hitPoint);
                }
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && IsDraggable(hit.collider.gameObject))
                {
                    BeginDrag(t, hit.transform, hitPoint);
                }
            }
            else if (t.phase == TouchPhase.Moved)
            {
                if (activeDrags.ContainsKey(t.fingerId))
                {
                    ContinueDrag(t, hitPoint);
                }
            }
        }
	}
}
