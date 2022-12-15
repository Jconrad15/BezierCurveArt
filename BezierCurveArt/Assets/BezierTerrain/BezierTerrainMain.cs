using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierTerrainMain : MonoBehaviour
{
    [SerializeField]
    private BezierTerrainGen bezierTerrainGen;

    [SerializeField]
    private float xc1 = 0f;
    [SerializeField]
    private float xc2 = 0f;
    [SerializeField]
    private float xc3 = 0f;
    [SerializeField]
    private float xc4 = 0f;

    [SerializeField]
    private float yc1 = 0f;
    [SerializeField]
    private float yc2 = 0f;
    [SerializeField]
    private float yc3 = 0f;
    [SerializeField]
    private float yc4 = 0f;

    private int seed = -1;

    private GameObject createdTerrain;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryCleanUp();

            createdTerrain = bezierTerrainGen.Generate(
                seed, 100, 100,
                xc1, xc2, xc3, xc4,
                yc1, yc2, yc3, yc4,
                true, false);

            // Position terrain for camera
            createdTerrain.transform.position = new Vector3(
                -0.5f, -0.5f, -0.5f);
        }
    }

    private void TryCleanUp()
    {
        if (createdTerrain != null)
        {
            Destroy(createdTerrain);
        }
    }
}
