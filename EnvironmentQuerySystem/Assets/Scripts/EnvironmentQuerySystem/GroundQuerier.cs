using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Assertions;

namespace EnvironmentQuerySystem
{
    /// <summary>
    /// Querier for locations on the ground. Performs queries in the -Vector.up direction.
    /// </summary>
    public class GroundQuerier : Querier
    {
        [Tooltip("Height from which the downward ray cast should be performed (allows for uneven ground)")]
        [SerializeField] private float _projectionHeight = 5f;
        [Tooltip("Height at which to set query point position after hitting ground (0 not recommended)")]
        [SerializeField] private float _heightAdjust = 0.5f;
        [Tooltip("How far down to allow ray cast to return points")]
        [SerializeField] private float _maxCastDistance = Mathf.Infinity;

        private void Update()
        {
            UpdateQuery();
        }

        public override void UpdateQuery()
        {
            _maxScoreIndex = -1;
            float maxScore = 0f;
            for (int i = 0; i < _queryFormation.Count; i++)
            {
                _queryFormation[i].Position = transform.TransformPoint(_queryFormation[i].Offset);
                float queryScore = 1f;
                RaycastHit hit;
                _raycastOrigins[i] = _queryFormation[i].Position + _projectionHeight * Vector3.up;
                if (Physics.Raycast(_raycastOrigins[i], Vector3.down, out hit, _maxCastDistance, _groundLayer + _rejectLayer))
                {
                    _queryFormation[i].Position = hit.point + _heightAdjust * Vector3.up;
                    if (hit.collider.gameObject.layer == (int)Mathf.Log(_rejectLayer.value, 2))
                    {
                        _queryFormation[i].Score = 0f;
                        continue;
                    }
                }
                else _queryFormation[i].Score = 0f;
                if (_queryEvaluations.Count > 0)
                {
                    queryScore = EvaluateQueryPoint(_queryFormation[i], queryScore);
                    if (queryScore == 0f)
                    {
                        _queryFormation[i].Score = 0f;
                        continue;
                    }
                    float originalScore = queryScore;
                    float modFactor = 1f - (1f / _queryEvaluations.Count);
                    float makeupValue = (1f - originalScore) * modFactor;
                    queryScore = originalScore + (makeupValue * originalScore);
                    if (queryScore > maxScore)
                    {
                        maxScore = queryScore;
                        _maxScoreIndex = i;
                    }
                }
                _queryFormation[i].Score = queryScore;
            }
        }
    }
}
