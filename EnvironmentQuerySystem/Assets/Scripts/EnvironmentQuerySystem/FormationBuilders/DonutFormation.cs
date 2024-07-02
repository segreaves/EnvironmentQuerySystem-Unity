using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnvironmentQuerySystem
{
    public class DonutFormation : FormationBuilder
    {
        [Tooltip("Number of rings that comprise the donut shape")]
        [SerializeField] private int _rings = 3;
        [Tooltip("Number of points in each ring")]
        [SerializeField] private int _pointsPerRing = 5;
        [Tooltip("Distance from donut center to inner ring")]
        [SerializeField] private float _innerRing = 2f;
        [Tooltip("Thickness of donut shape (outer ring minus inner ring)")]
        [SerializeField] private float _donutThickness = 3f;
        [Tooltip("Offset point so they all have a line of sight to the center")]
        [SerializeField] private bool _offsetPoints = true;
        [Tooltip("Offset point in a spiral")]
        [SerializeField] private bool _spiral = true;
        [Tooltip("Apply noise to the query point position")]
        [SerializeField] [Range(0f, 1f)] private float _noise = 0f;

        public override List<Vector3> BuildFormation()
        {
            return FormationBuilders.Donut(_rings, _pointsPerRing, _innerRing, _donutThickness, _offsetPoints, _spiral, _noise);
        }
    }
}
