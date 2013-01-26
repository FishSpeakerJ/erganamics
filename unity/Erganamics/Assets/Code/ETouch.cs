using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Erganamics
{
    

	class ETouch
	{
		public static int MOUSE_ID = -2;
		
        private Vector2 _deltaPosition;
        private float _deltaTime;
        private int _fingerId;
        private TouchPhase _phase;
        private Vector2 _position;
        private Vector3 _position3;
        private int _tapCount;

        public ETouch(Vector2 position, TouchPhase phase) : this(position, phase, new Vector2(0,0))
        {
        }

        public ETouch(Vector2 position, TouchPhase phase, Vector2 deltaPosition)
            : this(position, phase, deltaPosition, -1, MOUSE_ID, 1)
        {
        }

        public ETouch(Touch t)
            : this(t.position, t.phase, t.deltaPosition, t.deltaTime, t.fingerId, t.tapCount)
        {
        }

        public ETouch(Vector2 position, TouchPhase phase, Vector2 deltaPosition, float deltaTime, int fingerId, int tapCount)
        {
            this._deltaPosition = deltaPosition;
            this._deltaTime = deltaTime;
            this._phase = phase;
            this._position = position;
            this._fingerId = fingerId;
            this._tapCount = tapCount;
            this._position3 = new Vector3(this._position.x, this._position.y, 0);
        }

        public Vector2 deltaPosition { get { return this._deltaPosition; } }
        public float deltaTime { get { return this._deltaTime; } }
        public int fingerId { get { return this._fingerId; } }
        public TouchPhase phase { get { return this._phase; } }
        public Vector2 position { get { return this._position; } }
        public Vector2 positionV3 { get { return this._position3; } }
        public int tapCount { get { return this._tapCount; } } 
	}
}
