using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnvironmentQuerySystem
{
    public class FibonacciSphere : FormationBuilder
    {
        [SerializeField][Range(3, 100)] private int _numberOfPoints = 25;

        public override List<Vector3> BuildFormation()
        {
            return FormationBuilders.FibonacciSphere(_numberOfPoints, 1f);
        }
    }
}
