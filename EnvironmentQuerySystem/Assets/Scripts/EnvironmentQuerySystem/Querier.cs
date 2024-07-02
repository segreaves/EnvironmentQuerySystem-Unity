using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace EnvironmentQuerySystem
{
    /// <summary>
    /// Base class for any querier class.
    /// Extend this class to create queriers of different functionalities.
    /// </summary>
    public abstract class Querier : MonoBehaviour
    {
        [Tooltip("If true then query points are visualized (gizmos must be enabled)")]
        [SerializeField] protected bool _debug = true;
        [Tooltip("Provides the formation of the query points")]
        [SerializeField] protected FormationBuilder _formationBuilder;
        [Tooltip("Layer to be accepted as ground (score remains intact)")]
        [SerializeField] protected LayerMask _groundLayer;
        [Tooltip("Layer to be rejected (score slashed to 0)")]
        [SerializeField] protected LayerMask _rejectLayer;
        [Tooltip("List of methods to evaluate the query points. If empty then unpredictable point will be selected")]
        [SerializeField] protected List<IQueryEvaluation> _queryEvaluations = new List<IQueryEvaluation>();
        protected List<QueryPoint> _queryFormation = new List<QueryPoint>();
        protected int _maxScoreIndex;
        protected List<Vector3> _raycastOrigins = new List<Vector3>();

        public abstract void UpdateQuery();

        private void Awake()
        {
            Assert.IsNotNull(_formationBuilder, "Warning: Ground querier must have a formation builder assigned.");
            List<Vector3> points = _formationBuilder.BuildFormation();
            foreach (var point in points)
            {
                QueryPoint qp = new QueryPoint(point);
                _queryFormation.Add(qp);
                _raycastOrigins.Add(point);
            }
            _queryEvaluations = new List<IQueryEvaluation>(GetComponents<IQueryEvaluation>());
        }

        protected float EvaluateQueryPoint(QueryPoint queryPoint, float queryScore)
        {
            foreach (IQueryEvaluation evaluation in _queryEvaluations)
            {
                queryScore *= evaluation.EvaluateQueryPoint(queryPoint.Position);
                if (queryScore == 0f) return 0f;
            }
            return queryScore;
        }

        private void OnDrawGizmos()
        {
            if (!_debug) return;
            if (Application.isPlaying)
            {
                if (_queryFormation.Count == 0) return;
                for (int i = 0; i < _queryFormation.Count; i++)
                {
                    if (_maxScoreIndex == i)
                    {
                        Gizmos.color = Color.green;
                        Gizmos.DrawWireSphere(_queryFormation[i].Position, 0.5f);
                    }
                    else
                    {
                        if (_queryFormation[i].Score < 0.05f)
                        {
                            Gizmos.color = Color.red;
                            Gizmos.DrawWireSphere(_queryFormation[i].Position, 0.05f);
                        }
                        else
                        {
                            Gizmos.color = Color.blue;
                            Gizmos.DrawWireSphere(_queryFormation[i].Position, _queryFormation[i].Score / 2f);
                        }
                    }
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(_raycastOrigins[i], _queryFormation[i].Position);
                }
            }
        }
    }
}
