using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnvironmentQuerySystem
{
    public abstract class FormationBuilder : MonoBehaviour, IFormationBuilder
    {
        public abstract List<Vector3> BuildFormation();
    }
}
