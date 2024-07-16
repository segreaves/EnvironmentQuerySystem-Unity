using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnvironmentQuerySystem
{
    public class FibonacciDisc : FormationBuilder
    {
        [SerializeField] [Range(3, 500)] private int _numberOfPoints = 10;
        [SerializeField] private float _discRadius = 10.0f;

        public override List<Vector3> BuildFormation()
        {
            return FormationBuilders.FibonacciDisc(_numberOfPoints, _discRadius);
        }
    }
}
