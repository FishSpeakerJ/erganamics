using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Erganamics {

	class TouchUtils
	{
        private static ETouch mouseTouch = null;

        public static ETouch[] GetTouches()
        {
            int touchIndex = 0;
            if (Input.GetMouseButton(0) && Input.touchCount == 0)
            {
                touchIndex = 1;
                if (mouseTouch == null)
                {
                    mouseTouch = new ETouch(new Vector2(Input.mousePosition.x, Input.mousePosition.y), TouchPhase.Began);
                }
                else
                {
                    Vector2 newPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    Vector2 deltaPosition = new Vector2(newPos.x - mouseTouch.position.x, newPos.y - mouseTouch.position.y);
                    TouchPhase phase;
                    if (mouseTouch.deltaPosition.x == 0 && mouseTouch.deltaPosition.y == 0)
                    {
                        phase = TouchPhase.Stationary;
                    }
                    else
                    {
                        phase = TouchPhase.Moved;
                    }
                    mouseTouch = new ETouch(newPos, phase, deltaPosition);
                }
            }
            else
            {
                if (mouseTouch != null)
                {
                    touchIndex = 1;
                    Vector2 newPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    Vector2 deltaPosition = new Vector2(newPos.x - mouseTouch.position.x, newPos.y - mouseTouch.position.y);
                    mouseTouch = new ETouch(newPos, TouchPhase.Ended, deltaPosition);
                }
            }
            ETouch[] touches = new ETouch[touchIndex + Input.touchCount];
            if (touchIndex != 0)
            {
                touches[0] = mouseTouch;
            }
            foreach (Touch t in Input.touches) {
                touches[touchIndex] = new ETouch(t);
                touchIndex++;
            }
            if (mouseTouch != null && mouseTouch.phase == TouchPhase.Ended)
            {
                mouseTouch = null;
            }
            return touches;
        }

	}
}
