using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienSeashells
{
    public class MainController : MonoBehaviour
    {
        [SerializeField]
        private Drawer drawer;

        private CurveGenerator curveGenerator;

        private int seed;

        void Start()
        {
            curveGenerator = new CurveGenerator();
            Generate();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Generate();
            }
        }

        private void Generate()
        {
            seed = Random.Range(-10000, 10000);

            List<CubicBezierCurve> curves =
                curveGenerator.GenerateCurves(seed);

            drawer.Draw(curves, seed);
        }
    }
}