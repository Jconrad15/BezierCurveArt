using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BezierCurveWalk
{
    public class CurveCreator : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefabLine;

        private List<GameObject> curveGOs = new List<GameObject>();

        private float curveResolution = 0.01f;

        private float delay;
        private float Delay
        {
            get => delay;
            set
            {
                delay = value;
            }
        }

        private readonly float yTranslationChance = 0.2f;
        private bool isLowInk = false;

        private void Start()
        {
            Delay = 0.001f;
        }

        public void CreateNewCurves()
        {
            StopAllCoroutines();
            ClearAll();

            _ = StartCoroutine(Create());
        }

        private IEnumerator Create()
        {
            int curveCount = Random.Range(8, 14);
            int currentCurveIndex = 0;

            Vector3 currentPosition = Vector3.zero;

            while (currentCurveIndex < curveCount)
            {
                // Generate curve
                Vector3[] controlPoints =
                    GenerateControlPoints(currentPosition, currentCurveIndex);
                CubicBezierCurve curve =
                    new CubicBezierCurve(controlPoints);

                GameObject curveGO =
                    new GameObject("curve " + currentCurveIndex.ToString());
                curveGO.transform.SetParent(transform);
                curveGOs.Add(curveGO);

                // place each line of the curve
                float t = 0;
                while (t <= 1)
                {
                    GameObject line = Instantiate(prefabLine);
                    line.transform.position = currentPosition;
                    line.transform.SetParent(curveGO.transform);
                    LineRenderer lr = line.GetComponent<LineRenderer>();

                    Vector3[] linePositions = new Vector3[2]
                    {
                        curve.GetPosition(t),
                        // Note: don't clamp t + curveResolution
                        // to allow slight overshoots of the line
                        curve.GetPosition(t + curveResolution)
                    };
                    lr.SetPositions(linePositions);

                    t += curveResolution;


                    if (isLowInk == false)
                    {
                        // Change return conditions based on build type
                        // WebGL performs very badly trying to show drawing
                        // for each line segment
                        #if UNITY_WEBGL
                        // Return here some of the time
                        if (Random.value > 0.7f)
                        {
                            yield return new WaitForSeconds(Delay);
                        }
                        #else
                        yield return new WaitForSeconds(Delay);
                        #endif
                    }

                }

                // Set current pos to end of last curve
                currentPosition = curve.GetPosition(t);
                currentCurveIndex += 1;

                #if UNITY_WEBGL
                // Don't return here
                yield return new WaitForEndOfFrame();
                #else
                if (isLowInk)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                #endif

            }

        }

        private Vector3[] GenerateControlPoints(
            Vector3 startPos, int currentCurveIndex)
        {
            Vector3[] controlPoints = new Vector3[4];

            Vector3 endPos = DetermineEndPos(startPos);

            controlPoints[0] = startPos;
            controlPoints[1] = MutateVector(startPos, currentCurveIndex);
            controlPoints[2] = MutateVector(endPos, currentCurveIndex);
            controlPoints[3] = endPos;

            return controlPoints;
        }

        private Vector3 DetermineEndPos(Vector3 startPos)
        {
            Vector3 endPos = startPos;
            if (Random.value < 0.95f)
            {
                // Generally move towards the right
                endPos.x += Random.Range(0.5f, 1f);
            }
            else
            {
                // Chance to loop backwards
                endPos.x += Random.Range(-0.5f, -0.25f);
            }

            endPos = TryYTranslation(endPos);

            return endPos;
        }

        private Vector3 TryYTranslation(Vector3 endPos)
        {
            if (Random.value < yTranslationChance)
            {
                endPos.y += Random.Range(-0.3f, 0.3f);
            }

            return endPos;
        }

        private Vector3 MutateVector(
            Vector3 v, int currentCurveIndex)
        {
            float scale = 1f;
            if (currentCurveIndex >= 5) { scale = 0.5f; }

            v.x += Random.Range(-1.5f, 1f);
            v.y += Random.Range(-2 * scale, 2 * scale);
            //v.z += Random.Range(-1, 1);
            return v;
        }

        private void ClearAll()
        {
            foreach (GameObject curve in curveGOs)
            {
                Destroy(curve);
            }
            curveGOs.Clear();
        }

        public void SetLowInkPen(bool isLowInkPen)
        {
            if (isLowInkPen)
            {
                curveResolution = 0.001f;
                isLowInk = true;
            }
            else
            {
                curveResolution = 0.01f;
                isLowInk = false;
            }
        }

        public void SetDrawSpeed(float value)
        {
            Delay = value;
        }

    }
}