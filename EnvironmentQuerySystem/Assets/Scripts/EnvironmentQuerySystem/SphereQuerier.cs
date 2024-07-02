using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnvironmentQuerySystem
{
    /// <summary>
    /// Querier for locations around a sphere. Performs queries towards the owning object's transform.
    /// </summary>
    public class SphereQuerier : Querier
    {
        [Tooltip("Distance away from the point at which to initiate the ray cast")]
        [SerializeField] private float _projectionHeight = 5f;
        [Tooltip("Radial height at which to set query point position after hitting ground (0 not recommended)")]
        [SerializeField] private float _heightAdjust = 0.5f;

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
                Vector3 projectionDirection = transform.position - _queryFormation[i].Position;
                _queryFormation[i].Position -= _projectionHeight * projectionDirection.normalized;
                float queryScore = 1f;
                RaycastHit hit;
                _raycastOrigins[i] = _queryFormation[i].Position;
                if (Physics.Raycast(_queryFormation[i].Position, projectionDirection, out hit, _projectionHeight, _groundLayer + _rejectLayer))
                {
                    _queryFormation[i].Position = hit.point - _heightAdjust * projectionDirection.normalized;
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
