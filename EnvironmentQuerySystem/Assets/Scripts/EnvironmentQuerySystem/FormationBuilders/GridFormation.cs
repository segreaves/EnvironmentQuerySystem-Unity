using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace EnvironmentQuerySystem
{
    public class GridFormation : FormationBuilder
    {
        [Tooltip("Length of grid sides")]
        [SerializeField] private float _sizeLength = 10;
        [Tooltip("Number of points per row")]
        [SerializeField] private int _n = 10;
        [Tooltip("Apply noise to the query point position")]
        [SerializeField][Range(0f, 1f)] private float _noise;

        public override List<Vector3> BuildFormation()
        {
            return FormationBuilders.Grid(_sizeLength, _n, _noise);
        }
    }
}
