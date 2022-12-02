using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Flowers
{
    public class FlowerGenerator : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefabLine;

        public GameObject CreateFlower(float amplitude,
            int seed,
            int resolution, 
            int lineCount,
            float innerRadius, float stemWobble)
        {
            Random.State oldState = Random.state;
            Random.InitState(seed);

            GameObject flower = new GameObject("Flower");

            // Determine shape and inner circle points
            float halfAngle = (360f / lineCount) * Mathf.Deg2Rad;
            float height = amplitude * 2f;

            // Create bezier
            CubicBezierCurve[] curves = new CubicBezierCurve[lineCount];
            for (int i = 0; i < lineCount; i++)
            {
                float angle = ((float)i / lineCount) * 360f * Mathf.Deg2Rad;
                Vector3 outwardDirection = new Vector3(
                    Mathf.Cos(angle), Mathf.Sin(angle));

                Vector3[] controlPoints = new Vector3[4]
                {
                    innerRadius * Vector3.Normalize(new Vector3(
                        Mathf.Cos(angle - halfAngle), Mathf.Sin(angle - halfAngle))),
                    amplitude * outwardDirection,
                    amplitude * outwardDirection,
                    innerRadius * Vector3.Normalize(new Vector3(
                        Mathf.Cos(angle + halfAngle), Mathf.Sin(angle + halfAngle)))
                };
                curves[i] = new CubicBezierCurve(controlPoints);

                // Create line Gameobjects
                GameObject newLine = Instantiate(prefabLine);
                LineRenderer lr = newLine.GetComponent<LineRenderer>();
                // Add 1 to resolution to end curve at
                // the same location as starting point
                Vector3[] positions = new Vector3[resolution + 1];
                for (int j = 0; j <= resolution; j++)
                {
                    float t = j / (float)resolution;
                    positions[j] = curves[i].GetPosition(t);
                }
                lr.positionCount = positions.Length;
                lr.SetPositions(positions);

                // Add lines to flower GO
                newLine.transform.SetParent(flower.transform);
            }

            // Add line to ground for stem
            GameObject stemLine = Instantiate(prefabLine);
            LineRenderer stemLine_lr = stemLine.GetComponent<LineRenderer>();
            float stemAngle = 270f * Mathf.Deg2Rad;
            Vector3 stemTop = innerRadius * Vector3.Normalize(new Vector3(
                        Mathf.Cos(stemAngle), Mathf.Sin(stemAngle)));
            Vector3[] stemPoints = new Vector3[4]
            {
                stemTop,
                stemTop - new Vector3(
                    Random.Range(-stemWobble, stemWobble),
                    height * 0.33f,
                    Random.Range(-stemWobble, stemWobble)),
                stemTop - new Vector3(
                    Random.Range(-stemWobble, stemWobble),
                    height * 0.66f,
                    Random.Range(-stemWobble, stemWobble)),
                stemTop - new Vector3(0, height, 0)
            };
            CubicBezierCurve stemCurve = new CubicBezierCurve(stemPoints);
            Vector3[] stemPositions = new Vector3[resolution + 1];
            for (int j = 0; j <= resolution; j++)
            {
                float t = j / (float)resolution;
                stemPositions[j] = stemCurve.GetPosition(t);
            }
            stemLine_lr.positionCount = stemPositions.Length;
            stemLine_lr.SetPositions(stemPositions);

            // Add stem to flower GO
            stemLine.transform.SetParent(flower.transform);

            Random.state = oldState;
            return flower;
        }


    }
}