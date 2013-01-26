using UnityEngine;
using System.Collections;
using Erganamics;

public class ClickAndDrag : MonoBehaviour {

    private bool hasPoint = false;
    private Vector3 previousPoint;
	private int dragId = -1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ETouch[] touches = Erganamics.TouchUtils.GetTouches();
		bool shouldMove = false;
		Vector3 hitPoint = new Vector3(0,0);
        foreach (ETouch t in touches)
        {
            Ray ray = Camera.main.ScreenPointToRay(t.positionV3);
            
			hitPoint = ray.origin;
			if (dragId != -1 && dragId == t.fingerId && t.phase != TouchPhase.Ended)
			{
				shouldMove = true;
			}
			else 
			{
				RaycastHit hit;
	            if (Physics.Raycast(ray, out hit))
	            {	
	                if (hit.collider == this.collider)
	                {
	                    if (t.phase == TouchPhase.Began)
	                    {
							dragId = t.fingerId;
							hasPoint = true;
							previousPoint = hitPoint;
	                    }
	                    else if (t.phase == TouchPhase.Moved)
	                    {
	                        shouldMove = true;
	                    }
	                    else if (t.phase == TouchPhase.Ended)
	                    {
	                        shouldMove = false;
							hasPoint = false;
							dragId = -1;
	                    }
	                }   
	            }
			}
        }
		if (shouldMove)
		{
			if (hasPoint)
            {
                Vector2 clickOffset = new Vector2(hitPoint.x - previousPoint.x, hitPoint.y - previousPoint.y);
                this.transform.position = new Vector3(this.transform.position.x + clickOffset.x, this.transform.position.y + clickOffset.y, this.transform.position.z);
				previousPoint = hitPoint;
            }
		}
	}
}
