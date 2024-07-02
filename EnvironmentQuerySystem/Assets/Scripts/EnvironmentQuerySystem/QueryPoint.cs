using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnvironmentQuerySystem
{
    public class QueryPoint
    {
        public Vector3 Offset { get; private set; }
        public Vector3 Position { get; set; }
        private float _score;

        public float Score
        {
            get => _score;
            set => _score = Mathf.Clamp01(value);
        }

        public QueryPoint(Vector3 offset)
        {
            Offset = offset;
        }
    }
}
