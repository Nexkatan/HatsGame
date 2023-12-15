using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HatrisHexGrid : MonoBehaviour
{


    List<Vector3> vertices;
    List<int> triangles;


    HexMesh hexMesh;

    public Color defaultColor = Color.white;
    public Color touchedColor = Color.magenta;

    private void Awake()
    {
        HexGrid hexGrid = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        Vector3 pos = transform.position;
        HexCell currentCell = hexGrid.GetCell(transform.position);
        Debug.Log(currentCell.coordinates);
        TriangulateSingle(hexGrid.GetCell(transform.position));
    }

   

    public void TriangulateSingle(HexCell cell)
    {
        Vector3 center = cell.transform.localPosition;
        for (int i = 0; i < 6; i++)
        {
            AddTriangle(
                center,
                center + HexMetrics.corners[i],
                center + HexMetrics.corners[i + 1]
            );
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

        Debug.Log(triangles);
    }






}
