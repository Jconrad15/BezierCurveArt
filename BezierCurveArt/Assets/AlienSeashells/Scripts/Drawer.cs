using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    [SerializeField]
    private GameObject dotPrefab;

    private List<GameObject> createdCurves = new List<GameObject>();
    private readonly float granularity = 0.01f;

    private int seed;

    public void Draw(List<CubicBezierCurve> curves, int seed)
    {
        this.seed = seed;

        ClearAll();

        for (int i = 0; i < curves.Count; i++)
        {
            DrawCurve(curves[i]);
        }
    }

    private void DrawCurve(CubicBezierCurve curve)
    {
        GameObject curveObject = new GameObject("Curve");
        curveObject.transform.SetParent(transform);

        float t = 0f;

        while (t <= 1f)
        {
            Vector3 position = curve.GetPosition(t);

            CreateSphere(position, curveObject);
            t += granularity;
        }

        createdCurves.Add(curveObject);
    }

    private void CreateSphere(Vector3 position, GameObject parent)
    {
        GameObject sphere = Instantiate(dotPrefab, parent.transform);
        sphere.transform.position = position;

        sphere.GetComponent<MeshRenderer>().material
            .SetVector("_Seed", new Vector2(seed, -seed));
    }

    private void ClearAll()
    {
        foreach (GameObject curveObject in createdCurves)
        {
            Destroy(curveObject);
        }
        createdCurves.Clear();
    }

}
