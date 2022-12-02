using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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

        public void Color1Changed(float index)
        {
            flowerMain.Color1Changed(IndexToColor((int)index));
        }

        public void Color2Changed(float index)
        {
            flowerMain.Color2Changed(IndexToColor((int)index));
        }

        private Color IndexToColor(int index)
        {
            if (index == 0)
            {
                return new Color32(76, 6, 29, 255);
            }
            else if (index == 1)
            {
                return new Color32(209, 122, 34, 255);
            }
            else if (index == 2)
            {
                return new Color32(180, 194, 146, 255);
            }
            else if (index == 3)
            {
                return new Color32(115, 111, 78, 255);
            }
            else if (index == 4)
            {
                return new Color32(59, 57, 35, 255);
            }

            return Color.white;
        }
    }
}