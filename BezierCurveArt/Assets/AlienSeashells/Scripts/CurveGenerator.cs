using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AlienSeashells
{
    public enum ControlPointType { Random, Spherical };
    public class CurveGenerator
    {
        private float randomShiftMax;

        public List<CubicBezierCurve> GenerateCurves(int seed)
        {
            Random.State oldState = Random.state;
            Random.InitState(seed);

            List<CubicBezierCurve> curves = new List<CubicBezierCurve>();

            int curveCount = 120;
            float innerRadius = 0f;
            float outerRadius = Random.Range(2f, 6f);

            if (Random.value > 0.7f)
            {
                innerRadius = Random.Range(0.5f, outerRadius / 2f);
            }

            randomShiftMax = Random.Range(
                outerRadius / 4f, outerRadius * 3f / 4f);

            Vector3[] startPoints = new Vector3[curveCount];
            Vector3[] endPoints = new Vector3[curveCount];

            // Determine type of control points
            /*        ControlPointType controlPointType =
                        Utility.GetRandomEnum<ControlPointType>();
                    Debug.Log("Control point type " + controlPointType.ToString());*/

            // Currently only to spherical,
            // since these look like alien sea shells
            ControlPointType controlPointType = ControlPointType.Spherical;

            for (int i = 0; i < curveCount; i++)
            {
                // Parameterize around circle
                float t = (i / (float)curveCount) * 2 * Mathf.PI;

                // Determine start points
                startPoints[i] = new Vector3(
                    innerRadius * Mathf.Cos(t),
                    innerRadius * Mathf.Sin(t),
                    0);

                // Determine end points
                endPoints[i] = new Vector3(
                    outerRadius * Mathf.Cos(t),
                    outerRadius * Mathf.Sin(t),
                    0);

                Vector3[] controlPoints;
                // Determine other control points
                switch (controlPointType)
                {
                    case ControlPointType.Random:
                        controlPoints = DetermineRandomShiftControlPoints(
                            endPoints[i], startPoints[i], t);
                        break;
                    case ControlPointType.Spherical:
                        controlPoints = DetermineSphericalShiftControlPoints(
                            endPoints[i], startPoints[i], t, seed);
                        break;
                    default:
                        controlPoints = null;
                        break;
                }

                // Create curve based on control points
                CubicBezierCurve curve = new CubicBezierCurve(controlPoints);

                // Add to return list
                curves.Add(curve);
            }

            Random.state = oldState;
            return curves;
        }

        private Vector3[] DetermineRandomShiftControlPoints(
            Vector3 end, Vector3 start, float t)
        {
            Vector3[] controlPoints = new Vector3[4]
            {
            start,
            new Vector3(
                start.x + Random.Range(-randomShiftMax, randomShiftMax),
                start.y + Random.Range(-randomShiftMax, randomShiftMax),
                start.z + Random.Range(-randomShiftMax, randomShiftMax)),
            new Vector3(
                end.x + Random.Range(-randomShiftMax, randomShiftMax),
                end.y + Random.Range(-randomShiftMax, randomShiftMax),
                end.z + Random.Range(-randomShiftMax, randomShiftMax)),
            end
            };

            return controlPoints;
        }

        private Vector3[] DetermineSphericalShiftControlPoints(
            Vector3 end, Vector3 start, float t, int seed)
        {
            Random.State oldState = Random.state;
            Random.InitState(seed);

            // Chance to edit start position
            if (Random.value > 0.9f)
            {
                //Shift
                start.x = Random.value >= 0.5f ?
                    start.x + t : start.x - t;
            }
            if (Random.value > 0.9f)
            {
                //Shift
                start.y = Random.value >= 0.5f ?
                    start.y + t : start.y - t;
            }
            if (Random.value > 0.9f)
            {
                //Shift
                start.z = Random.value >= 0.5f ?
                    start.z + t : start.z - t;
            }

            // Determine second point
            Vector3 secondPoint;
            secondPoint.x = start.x;
            if (Random.value > 0.5f)
            {
                //Shift
                secondPoint.x = Random.value >= 0.5f ?
                    secondPoint.x + t : secondPoint.x - t;
            }

            secondPoint.y = start.y;
            if (Random.value > 0.5f)
            {
                //Shift
                secondPoint.y = Random.value >= 0.5f ?
                    secondPoint.y + t : secondPoint.y - t;
            }

            secondPoint.z = end.z;
            if (Random.value > 0.5f)
            {
                //Shift
                secondPoint.z = Random.value >= 0.5f ?
                    secondPoint.z + t : secondPoint.z - t;
            }

            // Determine third point
            Vector3 thirdPoint;
            thirdPoint.x = end.x;
            if (Random.value > 0.5f)
            {
                //Shift
                thirdPoint.x = Random.value >= 0.5f ?
                    thirdPoint.x + t : thirdPoint.x - t;
            }

            thirdPoint.y = end.y;
            if (Random.value > 0.5f)
            {
                //Shift
                thirdPoint.y = Random.value >= 0.5f ?
                    thirdPoint.y + t : thirdPoint.y - t;
            }

            thirdPoint.z = end.z;
            if (Random.value > 0.5f)
            {
                //Shift
                thirdPoint.z = Random.value >= 0.5f ?
                    thirdPoint.z + t : thirdPoint.z - t;
            }

            // Chance to edit end position
            if (Random.value > 0.9f)
            {
                //Shift
                end.x = Random.value >= 0.5f ?
                    end.x + t : end.x - t;
            }
            if (Random.value > 0.9f)
            {
                //Shift
                end.y = Random.value >= 0.5f ?
                    end.y + t : end.y - t;
            }
            if (Random.value > 0.9f)
            {
                //Shift
                end.z = Random.value >= 0.5f ?
                    end.z + t : end.z - t;
            }

            Vector3[] controlPoints = new Vector3[4]
            {
            start,
            secondPoint,
            thirdPoint,
            end
            };

            Random.state = oldState;
            return controlPoints;
        }

    }
}