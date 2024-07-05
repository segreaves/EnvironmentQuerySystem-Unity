using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnvironmentQuerySystem
{
    public static class FormationBuilders
    {
        /// <summary>
        /// Creates a formation of n x n point along a grid.
        /// Sides of the grid are of sizeLength length.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="sizeLength"></param>
        /// <param name="subDivisions"></param>
        /// <param name="noise"></param>
        /// <returns></returns>
        public static List<Vector3> Grid(
            float sizeLength,
            int n,
            float noise)
        {
            List<Vector3> formationPoints = new List<Vector3>(n * n);
            float delta = sizeLength / n;
            for (int col = 0; col < n; col++)
            {
                for (int row = 0; row < n; row++)
                {
                    float x = -0.5f * sizeLength + col * delta;
                    float z = -0.5f * sizeLength + row * delta;
                    Vector3 noiseVector = UnityEngine.Random.insideUnitSphere * Mathf.Clamp01(noise);
                    noiseVector.y = 0;
                    formationPoints.Add(new Vector3(x + noiseVector.x, 0, z + noiseVector.z));
                }
            }
            return formationPoints;
        }

        /// <summary>
        /// Creates a formation of Vector3 positions in the shape of concentric rings
        /// </summary>
        /// <param name="rings"></param>
        /// <param name="pointsPerRing"></param>
        /// <param name="innerRing"></param>
        /// <param name="donutThickness"></param>
        /// <param name="offsetPoints"></param>
        /// <param name="spiral"></param>
        /// <param name="noise"></param>
        /// <returns></returns>
        public static List<Vector3> Donut(
            int rings,
            int pointsPerRing,
            float innerRing,
            float donutThickness,
            bool offsetPoints,
            bool spiral,
            float noise)
        {
            List<Vector3> formationPoints = new List<Vector3>(pointsPerRing * rings);
            float angleDelta = 360f / pointsPerRing;
            float separationDelta = rings > 1 ? donutThickness / (rings - 1) : donutThickness;
            float offsetDelta;
            if (offsetPoints) offsetDelta = spiral ? angleDelta / rings : angleDelta / 2f;
            else offsetDelta = 0f;
            for (int ring = 0; ring < rings; ring++)
            {
                for (int pointIndex = 0; pointIndex < pointsPerRing; pointIndex++)
                {
                    Vector3 baseVector = Vector3.forward;
                    baseVector *= innerRing + ring * separationDelta;
                    Vector3 offset = Quaternion.Euler(0, ring * offsetDelta + pointIndex * angleDelta, 0) * baseVector;
                    Vector3 noiseVector = UnityEngine.Random.insideUnitSphere * Mathf.Clamp01(noise);
                    noiseVector.y = 0;
                    formationPoints.Add(offset + noiseVector);
                }
            }
            return formationPoints;
        }

        /// <summary>
        /// Creates a formation of n points uniformly distanced from each other using a Fibonacci disc.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static List<Vector3> FibonacciDisc(int n, float radius)
        {
            List<Vector3> formationPoints = new List<Vector3>(n);
            float goldenAngle = Mathf.PI * (3f - Mathf.Sqrt(5f));
            for (int i = 0; i < n; i++)
            {
                float rad = radius * Mathf.Sqrt(i + 0.5f) / Mathf.Sqrt(n);
                float theta = i * goldenAngle;
                Vector3 position = new Vector3(rad * Mathf.Cos(theta), 0f, rad * Mathf.Sin(theta));
                formationPoints.Add(position);
            }
            return formationPoints;
        }

        /// <summary>
        /// Creates a formation of n points uniformly distanced from each other using a Fibonacci sphere.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static List<Vector3> FibonacciSphere(int n, float radius)
        {
            List<Vector3> formationPoints = new List<Vector3>(n);
            float goldenAngle = Mathf.PI * (3f - Mathf.Sqrt(5f));

            for (int i = 0; i < n; i++)
            {
                float t = (float)i / n;
                float inclination = Mathf.Acos(1 - 2 * t);
                float azimuth = goldenAngle * i;

                float x = radius * Mathf.Sin(inclination) * Mathf.Cos(azimuth);
                float y = radius * Mathf.Sin(inclination) * Mathf.Sin(azimuth);
                float z = radius * Mathf.Cos(inclination);

                formationPoints.Add(new Vector3(x, y, z));
            }
            return formationPoints;
        }
    }
}
