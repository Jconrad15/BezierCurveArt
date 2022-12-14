using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierTerrainMain : MonoBehaviour
{
    [SerializeField]
    private BezierTerrainGen bezierTerrainGen;

    private int seed = -1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bezierTerrainGen.Generate(seed, 10, 10, true);
        }
    }
}
