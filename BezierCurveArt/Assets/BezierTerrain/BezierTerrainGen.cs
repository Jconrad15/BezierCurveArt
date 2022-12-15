using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terrain
{
    public class BezierTerrainGen : MonoBehaviour
    {
        private readonly float width = 1f;
        private readonly float length = 1f;
        [SerializeField]
        private Material terrainMaterial;

        public GameObject Generate(
            int seed,
            int xResolution, int zResolution,
            Vector3 xc1, Vector3 xc2, Vector3 xc3, Vector3 xc4,
            Vector3 zc1, Vector3 zc2, Vector3 zc3, Vector3 zc4,
            bool createMesh = true,
            bool createSpheres = false)
        {
            Random.State oldState = Random.state;
            Random.InitState(seed);

            // Create container gameobject
            GameObject terrainGO = new GameObject("Terrain");

            // Determine all points
            Vector3[] points = new Vector3[xResolution * zResolution];
            for (int z = 0, i = 0; z < zResolution; z++)
            {
                for (int x = 0; x < xResolution; x++, i++)
                {
                    points[i] = new Vector3(
                        x / (float)xResolution * width,
                        0,
                        z / (float)zResolution * length);
                }
            }

            // Generate Bezier curves between edge points
            CubicBezierCurve[] xCurves = new CubicBezierCurve[xResolution];
            for (int x = 0; x < xResolution; x++)
            {
                float scaledX = x / xResolution * width;
                Vector3[] controlPoints = new Vector3[4]
                {
                new Vector3(
                    scaledX,
                    xc1.x + (xc1.y * scaledX) + (xc1.z * scaledX * scaledX),
                    0),
                new Vector3(
                    scaledX,
                    xc2.x + (xc2.y * scaledX) + (xc2.z * scaledX * scaledX),
                    0),
                new Vector3(
                    scaledX,
                    xc3.x + (xc3.y * scaledX) + (xc3.z * scaledX * scaledX),
                    length),
                new Vector3(
                    scaledX,
                    xc4.x +(xc4.y * scaledX) +(xc4.z * scaledX * scaledX),
                    length)
                };

                xCurves[x] = new CubicBezierCurve(controlPoints);
            }
            CubicBezierCurve[] zCurves = new CubicBezierCurve[zResolution];
            for (int z = 0; z < zResolution; z++)
            {
                Vector3[] controlPoints = new Vector3[4]
                {
                new Vector3(
                    0,
                    zc1.x + (zc1.y * z) + (zc1.z * z * z),
                    z / (float)zResolution * length),
                new Vector3(
                    0,
                    zc2.x + (zc2.y * z) + (zc2.z * z * z),
                    z / (float)zResolution * length),
                new Vector3(
                    length,
                    zc3.x + (zc3.y * z) + (zc3.z * z * z),
                    z / (float)zResolution * length),
                new Vector3(
                    length,
                    zc4.x + (zc4.y * z) + (zc4.z * z * z),
                    z / (float)zResolution * length)
                };

                zCurves[z] = new CubicBezierCurve(controlPoints);
            }

            // Sample grid at points using curves
            for (int z = 0, i = 0; z < zResolution; z++)
            {
                for (int x = 0; x < xResolution; x++, i++)
                {
                    // Get curves at this point
                    // -- getting the t isn't mathematically correct
                    float xCurveHeight = xCurves[x].GetPosition(
                        z / (float)zResolution * length).y;
                    float zCurveHeight = zCurves[z].GetPosition(
                        x / (float)xResolution * width).y;

                    // Adjust height of the point
                    points[i].y += (xCurveHeight + zCurveHeight) / 2f;
                }
            }

            // Generate mesh
            if (createMesh)
            {
                GameObject meshGO = CreateMesh(
                    xResolution, zResolution, points);
                meshGO.transform.SetParent(terrainGO.transform);
            }

            if (createSpheres)
            {
                GameObject sphereGOs = GenerateSpheres(points);
                sphereGOs.transform.SetParent(terrainGO.transform);
            }

            Random.state = oldState;
            return terrainGO;
        }

        private GameObject CreateMesh(
            int xResolution, int zResolution, Vector3[] verticies)
        {
            //Vector2[] newUV;
            int[] triangles = new int[
                (xResolution - 1) * (zResolution - 1) * 6 * 2];

            // Top side of triangles
            int tIndex = 0;
            for (int z = 0; z < zResolution - 1; z++)
            {
                for (int x = 0; x < xResolution - 1; x++)
                {
                    int i = (z * zResolution) + x;

                    if (tIndex >= triangles.Length)
                    {
                        Debug.Log("Too long");
                    }

                    triangles[tIndex] = i;
                    triangles[tIndex + 1] = i + zResolution + 1;
                    triangles[tIndex + 2] = i + 1;
                    triangles[tIndex + 3] = i;
                    triangles[tIndex + 4] = i + zResolution;
                    triangles[tIndex + 5] = i + zResolution + 1;

                    tIndex += 6;
                }
            }
            // Bottom side of triangles
            for (int z = 0; z < zResolution - 1; z++)
            {
                for (int x = 0; x < xResolution - 1; x++)
                {
                    int i = (z * zResolution) + x;

                    if (tIndex >= triangles.Length)
                    {
                        Debug.Log("Too long");
                    }

                    triangles[tIndex] = i;
                    triangles[tIndex + 1] = i + 1;
                    triangles[tIndex + 2] = i + zResolution + 1;
                    triangles[tIndex + 3] = i;
                    triangles[tIndex + 4] = i + zResolution + 1;
                    triangles[tIndex + 5] = i + zResolution;

                    tIndex += 6;
                }
            }

            Mesh mesh = new Mesh();
            mesh.vertices = verticies;
            //mesh.uv = newUV;
            mesh.triangles = triangles;

            GameObject meshGO = new GameObject("Mesh");
            MeshRenderer mr = meshGO.AddComponent<MeshRenderer>();
            MeshFilter mf = meshGO.AddComponent<MeshFilter>();
            mf.mesh = mesh;
            mr.material = terrainMaterial;

            return meshGO;
        }

        private GameObject GenerateSpheres(Vector3[] points)
        {
            GameObject sphereParent = new GameObject("Spheres");
            // Generate Gameobjects at points
            for (int i = 0; i < points.Length; i++)
            {
                GameObject sphere = GameObject.CreatePrimitive(
                    PrimitiveType.Sphere);

                sphere.transform.SetParent(sphereParent.transform);
                sphere.transform.position = points[i];
                sphere.transform.localScale =
                    new Vector3(0.05f, 0.05f, 0.05f);
            }
            return sphereParent;
        }
    }
}