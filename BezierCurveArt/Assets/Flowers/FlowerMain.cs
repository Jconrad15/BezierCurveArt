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

        private int seed = -1;
        // Flower Variables
        private float amplitude = 1f;
        private int resolution = 25;
        private int lineCount = 10;
        private float innerRadius = 0.5f;
        private float stemWobble = 0.5f;
        private Color color1 = new Color32(115, 111, 78, 255);
        private Color color2 = new Color32(115, 111, 78, 255);
        private float randomInfluence = 0;
        private float bezierCurveAugment = 0;

        // Field Variables
        private int flowerCount = 100;
        private float fieldSize = 10f;

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
                stemWobble, color1, color2, randomInfluence,
                bezierCurveAugment);

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

        public void RandomInfluenceChanged(float randomInfluence)
        {
            this.randomInfluence = randomInfluence;
            Create();
        }

        public void bezierCurveAugmentChanged(float bezierCurveAugment)
        {
            this.bezierCurveAugment = bezierCurveAugment;
            Create();
        }
    }
}