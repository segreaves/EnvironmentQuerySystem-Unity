using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnvironmentQuerySystem
{
    public interface IQueryEvaluation
    {
        float EvaluateQueryPoint(Vector3 point);
    }
}
