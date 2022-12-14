using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierTerrainMain : MonoBehaviour
{
    [SerializeField]
    private BezierTerrainGen bezierTerrainGen;

    private int seed = -1;

    private GameObject createdTerrain;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryCleanUp();

            createdTerrain = bezierTerrainGen.Generate(
                seed, 100, 100, true, false);

            // Position terrain for camera
            createdTerrain.transform.position = new Vector3(
                -0.5f, -0.25f, -0.5f);
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
