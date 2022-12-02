using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Flowers
{
    public class FlowerGenerator : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefabLine;

        private Color32 stemColor = new Color32(17, 72, 12, 255);

        public GameObject CreateFlower(float amplitude,
            int seed,
            int resolution, 
            int lineCount,
            float innerRadius, float stemWobble,
            Color color1, Color color2,
            float randomInfluence, float bezierCurveAugment)
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
                /*Vector3 outwardDirection = new Vector3(
                    Mathf.Cos(angle), Mathf.Sin(angle));*/

                Vector3 randomVec = Random.onUnitSphere * randomInfluence;

                Vector3 pos1 =
                    innerRadius * Vector3.Normalize(new Vector3(
                        Mathf.Cos(angle - halfAngle),
                        Mathf.Sin(angle - halfAngle)));
                
                Vector3 pos2 =
                    (amplitude * new Vector3(Mathf.Cos(angle - bezierCurveAugment), Mathf.Sin(angle - bezierCurveAugment))) + randomVec;

                Vector3 pos3 =
                    (amplitude * new Vector3(Mathf.Cos(angle + bezierCurveAugment), Mathf.Sin(angle + bezierCurveAugment))) + randomVec;
                
                Vector3 pos4 =
                    innerRadius * Vector3.Normalize(new Vector3(
                        Mathf.Cos(angle + halfAngle),
                        Mathf.Sin(angle + halfAngle)));

                Vector3[] controlPoints = new Vector3[4]
                {
                    pos1, pos2, pos3, pos4
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

                lr.startColor = color1;
                lr.endColor = color2;

                // Add lines to flower GO
                newLine.transform.SetParent(flower.transform);
            }

            // Add stem to flower GO
            GameObject stemLine = GenerateStem(
                resolution, innerRadius, stemWobble,
                height, color1, color2);
            stemLine.transform.SetParent(flower.transform);

            Random.state = oldState;
            return flower;
        }

        private GameObject GenerateStem(
            int resolution, float innerRadius,
            float stemWobble, float height,
            Color color1, Color color2)
        {
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

            stemLine_lr.startColor = stemColor;
            stemLine_lr.endColor = stemColor;

            return stemLine;
        }
    }
}