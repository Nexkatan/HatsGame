using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HatrisHexCellMesh : MonoBehaviour
{

    Mesh hatrisHexCellMesh;
    public List<Vector3> vertices;
    public List<int> triangles;
    MeshCollider meshCollider;
    public List<Color> colors;


    void Awake()
    {
        GetComponent<MeshFilter>().mesh = hatrisHexCellMesh = new Mesh();
        meshCollider = gameObject.AddComponent<MeshCollider>();

        hatrisHexCellMesh.name = "hatrisHexCellMesh";
        vertices = new List<Vector3>();
        colors = new List<Color>();
        triangles = new List<int>();
    }

    void Start()
    {
        Triangulate(this.GetComponent<HexCell>());
    }
    public void Triangulate(HexCell cell)
    {
        hatrisHexCellMesh.Clear();
        vertices.Clear();
        colors.Clear();
        triangles.Clear();

        TriangulateSingle(cell);
        
        hatrisHexCellMesh.vertices = vertices.ToArray();
        hatrisHexCellMesh.colors = colors.ToArray();
        hatrisHexCellMesh.triangles = triangles.ToArray();
        hatrisHexCellMesh.RecalculateNormals();
        meshCollider.sharedMesh = hatrisHexCellMesh;
        
    }

    public void TriangulateSingle(HexCell cell)
    {
        Vector3 center = Vector3.zero;
        for (int i = 0; i < 6; i++)
        {
            AddTriangle(
                center,
                center + HexMetrics.sideMidPoints[i],
                center + HexMetrics.corners[i]
            );
            AddTriangle(
                center,
                center + HexMetrics.corners[i],
                center + HexMetrics.sideMidPoints[i+1]
                );
            //AddTriangleColor(cell.color);
        }
    }

    void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }

    void AddTriangleColor(Color color)
    {
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
    }

}