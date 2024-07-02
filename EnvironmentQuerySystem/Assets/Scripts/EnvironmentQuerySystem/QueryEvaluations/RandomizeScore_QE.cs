using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnvironmentQuerySystem
{
    public class RandomizeScore_QE : MonoBehaviour, IQueryEvaluation
    {
        [SerializeField][Range(0f, 1f)] float _randomness = 0.1f;

        public float EvaluateQueryPoint(Vector3 point)
        {
            float distance = Vector3.Distance(transform.position, point);
            return Random.Range(1f - _randomness, 1f);
        }
    }
}
