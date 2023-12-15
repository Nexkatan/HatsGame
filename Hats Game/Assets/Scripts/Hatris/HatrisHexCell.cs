using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HatrisHexCell : MonoBehaviour
{
    public HatrisTriangleCoordinates triCoordinates;
    public Color color;
   
    public GameObject hatPieceAbove;

    public float height;

    void OnEnable()
    {
        var mesh = new Mesh
        {
            name = "Procedural Hat Diamond Mesh"
        };
        mesh.vertices = new Vector3[] {
            new Vector3(0f, .1f, 0f),
            new Vector3(0f, .1f, 2f * height),
            new Vector3((Mathf.Sqrt(3f) / 2f) * height, .1f, 1.5f * height),
            new Vector3(-(Mathf.Sqrt(3f) / 2f) * height, .1f, 1.5f * height),
        };
        mesh.triangles = new int[] {
            0, 1, 2,
            0, 3, 1
        };
        mesh.normals = new Vector3[] {
            Vector3.back, Vector3.back, Vector3.back, Vector3.back
        };
        mesh.uv = new Vector2[] {
            new Vector2(0f, 0f),
            new Vector2(0f, 2f * height),
            new Vector2((Mathf.Sqrt(3f) / 2f) * height, 1.5f * height),
            new Vector2(-(Mathf.Sqrt(3f) / 2f) * height, 1.5f * height),
        };
        mesh.tangents = new Vector4[] {
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f)

        };
        GetComponent<MeshFilter>().mesh = mesh;
    }
}