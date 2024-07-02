using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnvironmentQuerySystem
{
    public class DistanceToObjective_QE : MonoBehaviour, IQueryEvaluation
    {
        [SerializeField] private Transform _objective;
        [SerializeField] [Range(0f, 25f)] float _decayParam = 5f;

        public float EvaluateQueryPoint(Vector3 point)
        {
            float distance = Vector3.Distance(_objective.position, point);
            return Mathf.Exp(-distance/_decayParam);
        }
    }
}
