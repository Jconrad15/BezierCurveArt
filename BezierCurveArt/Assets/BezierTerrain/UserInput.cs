using UnityEngine;

namespace Terrain
{
    public class UserInput : MonoBehaviour
    {
        private BezierTerrainMain terrainMain;
        private void Start()
        {
            terrainMain = FindObjectOfType<BezierTerrainMain>();
        }

        public void SeedChanged(float seed)
        {
            terrainMain.SeedChanged((int)seed);
        }

        public void RandomInfluenceChanged(float value)
        {
            terrainMain.RandomInfluenceChanged(value);
        }

        // X Curves
        public void XCurve1xChanged(float value)
        {
            terrainMain.XCurve1xChanged(value);
        }

        public void XCurve1yChanged(float value)
        {
            terrainMain.XCurve1yChanged(value);
        }

        public void XCurve1zChanged(float value)
        {
            terrainMain.XCurve1zChanged(value);
        }

        public void XCurve2xChanged(float value)
        {
            terrainMain.XCurve2xChanged(value);
        }

        public void XCurve2yChanged(float value)
        {
            terrainMain.XCurve2yChanged(value);
        }

        public void XCurve2zChanged(float value)
        {
            terrainMain.XCurve2zChanged(value);
        }

        public void XCurve3xChanged(float value)
        {
            terrainMain.XCurve3xChanged(value);
        }

        public void XCurve3yChanged(float value)
        {
            terrainMain.XCurve3yChanged(value);
        }

        public void XCurve3zChanged(float value)
        {
            terrainMain.XCurve3zChanged(value);
        }

        public void XCurve4xChanged(float value)
        {
            terrainMain.XCurve4xChanged(value);
        }

        public void XCurve4yChanged(float value)
        {
            terrainMain.XCurve4yChanged(value);
        }

        public void XCurve4zChanged(float value)
        {
            terrainMain.XCurve4zChanged(value);
        }

        // Z Curves
        public void ZCurve1xChanged(float value)
        {
            terrainMain.ZCurve1xChanged(value);
        }

        public void ZCurve1yChanged(float value)
        {
            terrainMain.ZCurve1yChanged(value);
        }

        public void ZCurve1zChanged(float value)
        {
            terrainMain.ZCurve1zChanged(value);
        }

        public void ZCurve2xChanged(float value)
        {
            terrainMain.ZCurve2xChanged(value);
        }

        public void ZCurve2yChanged(float value)
        {
            terrainMain.ZCurve2yChanged(value);
        }

        public void ZCurve2zChanged(float value)
        {
            terrainMain.ZCurve2zChanged(value);
        }

        public void ZCurve3xChanged(float value)
        {
            terrainMain.ZCurve3xChanged(value);
        }

        public void ZCurve3yChanged(float value)
        {
            terrainMain.ZCurve3yChanged(value);
        }

        public void ZCurve3zChanged(float value)
        {
            terrainMain.ZCurve3zChanged(value);
        }

        public void ZCurve4xChanged(float value)
        {
            terrainMain.ZCurve4xChanged(value);
        }

        public void ZCurve4yChanged(float value)
        {
            terrainMain.ZCurve4yChanged(value);
        }

        public void ZCurve4zChanged(float value)
        {
            terrainMain.ZCurve4zChanged(value);
        }
    }
}