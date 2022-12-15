using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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

        public void XResolutionChanged(float resolution)
        {
            terrainMain.XResolutionChanged((int)resolution);
        }

        public void ZResolutionChanged(float resolution)
        {
            terrainMain.ZResolutionChanged((int)resolution);
        }

        public void XCurve1aChanged(float value)
        {
            terrainMain.XCurve1xChanged((int)value);
        }




    }
}