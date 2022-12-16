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
            int xResolution, int zResolution,
            float randomInfluence, float scale,
            Vector3 xc1, Vector3 xc2, Vector3 xc3, Vector3 xc4,
            Vector3 zc1, Vector3 zc2, Vector3 zc3, Vector3 zc4,
            bool createMesh = true,
            bool createSpheres = false)
        {
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
                float scaledX = x / (float)xResolution * width;
                Vector3[] controlPoints = new Vector3[4]
                {
                new Vector3(
                    scaledX,
                    xc1.x + (xc1.y * scaledX) + Mathf.Sin(scaledX * xc1.z),
                    0),
                new Vector3(
                    scaledX,
                    xc2.x + (xc2.y * scaledX) + Mathf.Sin(scaledX * xc2.z),
                    0),
                new Vector3(
                    scaledX,
                    xc3.x + (xc3.y * scaledX) + Mathf.Sin(scaledX * xc3.z),
                    length),
                new Vector3(
                    scaledX,
                    xc4.x + (xc4.y * scaledX) + Mathf.Sin(scaledX * xc4.z),
                    length)
                };

                xCurves[x] = new CubicBezierCurve(controlPoints);
            }
            CubicBezierCurve[] zCurves = new CubicBezierCurve[zResolution];
            for (int z = 0; z < zResolution; z++)
            {
                float scaledZ = z / (float)zResolution * length;
                Vector3[] controlPoints = new Vector3[4]
                {
                new Vector3(
                    0,
                    zc1.x + (zc1.y * scaledZ) + Mathf.Sin(scaledZ * zc1.z),
                    scaledZ),
                new Vector3(
                    0,
                    zc2.x + (zc2.y * scaledZ) + Mathf.Sin(scaledZ * zc2.z),
                    scaledZ),
                new Vector3(
                    width,
                    zc3.x + (zc3.y * scaledZ) + Mathf.Sin(scaledZ * zc3.z),
                    scaledZ),
                new Vector3(
                    width,
                    zc4.x + (zc4.y * scaledZ) + Mathf.Sin(scaledZ * zc4.z),
                    scaledZ)
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
                    points[i].y +=
                        ((xCurveHeight + zCurveHeight) / 2f) +
                        (randomInfluence * Perlin(x, z, scale));
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

            return terrainGO;
        }

        private GameObject CreateMesh(
            int xResolution, int zResolution, Vector3[] points)
        {
            // Assign the points to vertices twice. once for each side
            Vector3[] vertices = new Vector3[points.Length * 2];
            for (int i = 0; i < points.Length; i++)
            {
                vertices[i] = points[i];
                vertices[i + points.Length] = points[i];
            }

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
                    int i = (z * zResolution) + x + points.Length;

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
            mesh.vertices = vertices;
            //mesh.uv = newUV;
            mesh.triangles = triangles;

            mesh.RecalculateNormals();

            GameObject meshGO = new GameObject("Mesh");
            MeshRenderer mr = meshGO.AddComponent<MeshRenderer>();
            MeshFilter mf = meshGO.AddComponent<MeshFilter>();
            mf.mesh = mesh;
            mr.material = terrainMaterial;
            mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

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

        private float Perlin(float x, float z, float scale)
        {
            float value = Mathf.PerlinNoise(x * scale, z * scale);
            value = (value / 10f) - (1 / 10f / 2f);
            return value;
        }
    }
}