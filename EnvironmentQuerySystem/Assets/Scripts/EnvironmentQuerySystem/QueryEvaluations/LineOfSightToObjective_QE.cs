using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace EnvironmentQuerySystem
{
    public class LineOfSightToObjective_QE : MonoBehaviour, IQueryEvaluation
    {
        [SerializeField] private Transform _objective;
        [SerializeField] private LayerMask _sightBlockLayers;
        [SerializeField] [Range(0f, 1f)] private float _sphereCastRadius = 0.2f;
        [Tooltip("How much to penalize utility of points with no LOS, rather than zeroing them out")]
        [SerializeField][Range(0f, 1f)] private float _penalty = 1f;

        public float EvaluateQueryPoint(Vector3 point)
        {
            bool hasLineOfSight = EvaluateLineOfSight(point, _objective.position);
            return hasLineOfSight ? 1f : 1f - _penalty;
        }

        public bool EvaluateLineOfSight(Vector3 start, Vector3 end)
        {
            RaycastHit hitInfo;
            bool los = !Physics.Raycast(start, end - start, out hitInfo, (end - start).magnitude, _sightBlockLayers);
            Collider collider = hitInfo.collider;
            return los ? true : collider.transform.root == _objective;
        }
    }
}
