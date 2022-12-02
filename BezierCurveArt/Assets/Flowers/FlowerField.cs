using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flowers
{
    public class FlowerField : MonoBehaviour
    {
        private readonly float scale = 0.2f;

        public GameObject CreateField(
            int seed,
            float amplitude, GameObject largeFlower,
            int flowerCount, float fieldSize)
        {
            Random.State oldState = Random.state;
            Random.InitState(seed);

            GameObject field = new GameObject("Field");

            for (int i = 0; i < flowerCount; i++)
            {
                GameObject newFlower = Instantiate(largeFlower);
                newFlower.transform.SetParent(field.transform);
                newFlower.transform.localScale =
                    newFlower.transform.localScale * scale;
                Vector3 newPos = new Vector3(
                    Random.Range(-fieldSize, fieldSize),
                    -2 + (amplitude * scale * 2f),
                    Random.Range(-fieldSize, fieldSize));
                newFlower.transform.position = newPos;
                // Random y rotation
                newFlower.transform.Rotate(0, Random.Range(0, 360), 0);
            }

            Random.state = oldState;
            return field;
        }
    }
}
