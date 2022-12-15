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

        [SerializeField]
        private Vector3 xc1 = Vector3.zero;
        [SerializeField]
        private Vector3 xc2 = Vector3.zero;
        [SerializeField]
        private Vector3 xc3 = Vector3.zero;
        [SerializeField]
        private Vector3 xc4 = Vector3.zero;

        [SerializeField]
        private Vector3 yc1 = Vector3.zero;
        [SerializeField]
        private Vector3 yc2 = Vector3.zero;
        [SerializeField]
        private Vector3 yc3 = Vector3.zero;
        [SerializeField]
        private Vector3 yc4 = Vector3.zero;

        [SerializeField]
        private int xResolution = 100;
        [SerializeField]
        private int zResolution = 100;

        [SerializeField]
        private int seed = -1;

        private GameObject createdTerrain;

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
                seed, xResolution, zResolution,
                xc1, xc2, xc3, xc4,
                yc1, yc2, yc3, yc4,
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
        }

        public void XResolutionChanged(int xResolution)
        {
            this.xResolution = xResolution;
        }

        public void ZResolutionChanged(int zResolution)
        {
            this.zResolution = zResolution;
        }

        public void XCurve1xChanged(int value)
        {
            xc1.x = value;
        }

        public void XCurve1yChanged(int value)
        {
            xc1.y = value;
        }

        public void XCurve1zChanged(int value)
        {
            xc1.z = value;
        }












    }
}