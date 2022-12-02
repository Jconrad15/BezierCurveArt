using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flowers
{
    public class FlowerMain : MonoBehaviour
    {
        [SerializeField]
        private FlowerGenerator flowerGenerator;
        [SerializeField]
        private FlowerField flowerField;

        private GameObject largeFlower;
        private GameObject field;

        int seed = -1;
        // Flower Variables
        float amplitude = 1f;
        int resolution = 25;
        int lineCount = 10;
        float innerRadius = 0.5f;
        float stemWobble = 0.5f;
        Color color1 = new Color32(115, 111, 78, 255);
        Color color2 = new Color32(115, 111, 78, 255);

        // Field Variables
        int flowerCount = 100;
        float fieldSize = 10f;

        private void Start()
        {
            Create();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Create();
            }
        }

        private void Create()
        {
            Clear();

            largeFlower = flowerGenerator.CreateFlower(
                amplitude, seed, resolution, lineCount, innerRadius,
                stemWobble, color1, color2);

            field = flowerField.CreateField(
                seed, amplitude, largeFlower, flowerCount, fieldSize);
        }

        private void Clear()
        {
            if (largeFlower != null)
            {
                Destroy(largeFlower);
            }
            if (field != null)
            {
                Destroy(field);
            }
        }

        // Changing variables
        public void SeedChanged(int seed)
        {
            this.seed = seed;
            Create();
        }

        public void ResolutionChanged(int resolution)
        {
            this.resolution = resolution;
            Create();
        }

        public void AmplitudeChanged(float amplitude)
        {
            this.amplitude = amplitude;
            Create();
        }

        public void LineCountChanged(int lineCount)
        {
            this.lineCount = lineCount;
            Create();
        }

        public void InnerRadiusChanged(float innerRadius)
        {
            this.innerRadius = innerRadius;
            Create();
        }

        public void StemWobbleChanged(float stemWobble)
        {
            this.stemWobble = stemWobble;
            Create();
        }

        public void FlowerCountChanged(int flowerCount)
        {
            this.flowerCount = flowerCount;
            Create();
        }
        
        public void FieldSizeChanged(float fieldSize)
        {
            this.fieldSize = fieldSize;
            Create();
        }

        public void Color1Changed(Color color1)
        {
            this.color1 = color1;
            Create();
        }

        public void Color2Changed(Color color2)
        {
            this.color2 = color2;
            Create();
        }

    }
}