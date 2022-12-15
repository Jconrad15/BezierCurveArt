using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Experimental.TerrainAPI.TerrainUtility;

namespace Terrain
{
    public class BezierTerrainMain : MonoBehaviour
    {
        [SerializeField]
        private BezierTerrainGen bezierTerrainGen;

        private Vector3 xc1 = Vector3.zero;
        private Vector3 xc2 = Vector3.zero;
        private Vector3 xc3 = Vector3.zero;
        private Vector3 xc4 = Vector3.zero;

        private Vector3 zc1 = Vector3.zero;
        private Vector3 zc2 = Vector3.zero;
        private Vector3 zc3 = Vector3.zero;
        private Vector3 zc4 = Vector3.zero;

        private int xResolution = 100;
        private int zResolution = 100;
        private float randomInfluence = 0f;

        [SerializeField]
        private int seed = -1;

        private GameObject createdTerrain;

        private void Start()
        {
            GenerateTerrain();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GenerateTerrain();
            }
        }

        private void GenerateTerrain()
        {
            TryCleanUp();

            createdTerrain = bezierTerrainGen.Generate(
                seed, xResolution, zResolution, randomInfluence,
                xc1, xc2, xc3, xc4,
                zc1, zc2, zc3, zc4,
                true, false);

            // Position terrain for camera
            createdTerrain.transform.position = new Vector3(
                -0.5f, -0.5f, -0.5f);
        }

        private void TryCleanUp()
        {
            if (createdTerrain != null)
            {
                Destroy(createdTerrain);
            }
        }

        public void SeedChanged(int seed)
        {
            this.seed = seed;
            GenerateTerrain();
        }

        public void RandomInfluenceChanged(float randomInfluence)
        {
            this.randomInfluence = randomInfluence;
            GenerateTerrain();
        }

        // X curves
        public void XCurve1xChanged(float value)
        {
            xc1.x = value;
            GenerateTerrain();
        }

        public void XCurve1yChanged(float value)
        {
            xc1.y = value;
            GenerateTerrain();
        }

        public void XCurve1zChanged(float value)
        {
            xc1.z = value;
            GenerateTerrain();
        }

        public void XCurve2xChanged(float value)
        {
            xc2.x = value;
            GenerateTerrain();
        }

        public void XCurve2yChanged(float value)
        {
            xc2.y = value;
            GenerateTerrain();
        }

        public void XCurve2zChanged(float value)
        {
            xc2.z = value;
            GenerateTerrain();
        }

        public void XCurve3xChanged(float value)
        {
            xc3.x = value;
            GenerateTerrain();
        }

        public void XCurve3yChanged(float value)
        {
            xc3.y = value;
            GenerateTerrain();
        }

        public void XCurve3zChanged(float value)
        {
            xc3.z = value;
            GenerateTerrain();
        }

        public void XCurve4xChanged(float value)
        {
            xc4.x = value;
            GenerateTerrain();
        }

        public void XCurve4yChanged(float value)
        {
            xc4.y = value;
            GenerateTerrain();
        }

        public void XCurve4zChanged(float value)
        {
            xc4.z = value;
            GenerateTerrain();
        }

        // Z curves
        public void ZCurve1xChanged(float value)
        {
            zc1.x = value;
            GenerateTerrain();
        }

        public void ZCurve1yChanged(float value)
        {
            zc1.y = value;
            GenerateTerrain();
        }

        public void ZCurve1zChanged(float value)
        {
            zc1.z = value;
            GenerateTerrain();
        }

        public void ZCurve2xChanged(float value)
        {
            zc2.x = value;
            GenerateTerrain();
        }

        public void ZCurve2yChanged(float value)
        {
            zc2.y = value;
            GenerateTerrain();
        }

        public void ZCurve2zChanged(float value)
        {
            zc2.z = value;
            GenerateTerrain();
        }

        public void ZCurve3xChanged(float value)
        {
            zc3.x = value;
            GenerateTerrain();
        }

        public void ZCurve3yChanged(float value)
        {
            zc3.y = value;
            GenerateTerrain();
        }

        public void ZCurve3zChanged(float value)
        {
            zc3.z = value;
            GenerateTerrain();
        }

        public void ZCurve4xChanged(float value)
        {
            zc4.x = value;
            GenerateTerrain();
        }

        public void ZCurve4yChanged(float value)
        {
            zc4.y = value;
            GenerateTerrain();
        }

        public void ZCurve4zChanged(float value)
        {
            zc4.z = value;
            GenerateTerrain();
        }




    }
}