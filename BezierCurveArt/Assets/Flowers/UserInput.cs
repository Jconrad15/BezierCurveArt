using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flowers
{
    public class UserInput : MonoBehaviour
    {
        private FlowerMain flowerMain;
        private void Start()
        {
            flowerMain = FindObjectOfType<FlowerMain>();
        }

        public void SeedChanged(float seed)
        {
            flowerMain.SeedChanged((int)seed);
        }

        public void ResolutionChanged(float resolution)
        {
            flowerMain.ResolutionChanged((int)resolution);
        }

        public void AmplitudeChanged(float amplitude)
        {
            flowerMain.AmplitudeChanged(amplitude);
        }

        public void LineCountChanged(float lineCount)
        {
            flowerMain.LineCountChanged((int)lineCount);
        }

        public void InnerRadiusChanged(float innerRadius)
        {
            flowerMain.InnerRadiusChanged(innerRadius);
        }

        public void StemWobbleChanged(float stemWobble)
        {
            flowerMain.StemWobbleChanged(stemWobble);
        }

        public void FlowerCountChanged(float flowerCount)
        {
            flowerCount *= 100;
            flowerMain.FlowerCountChanged((int)flowerCount);
        }

        public void FieldSizeChanged(float fieldSize)
        {
            flowerMain.FieldSizeChanged(fieldSize);
        }

    }
}