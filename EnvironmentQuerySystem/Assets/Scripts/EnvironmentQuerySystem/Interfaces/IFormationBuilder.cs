using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnvironmentQuerySystem
{
    public interface IFormationBuilder
    {
        List<Vector3> BuildFormation();
    }
}
