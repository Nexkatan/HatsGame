using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField] public Vector3 point1;
    [SerializeField] public Vector3 point2;
    [SerializeField] public Vector3 point3;
    [SerializeField] public Vector3 point4;
    [SerializeField] public Vector3 point5;
    [SerializeField] public Vector3 point6;
    [SerializeField] public Vector3 point7;
    [SerializeField] public Vector3 point8;
    [SerializeField] public Vector3 point9;
    [SerializeField] public Vector3 point10;
    [SerializeField] public Vector3 point11;
    [SerializeField] public Vector3 point12;
    [SerializeField] public Vector3 point13;
    [SerializeField] public Vector3 point14;
    [SerializeField] public Vector3 point15;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Set some positions
        Vector3[] positions = new Vector3[15];
        positions[0] = point1;
        positions[1] = point2;
        positions[2] = point3;
        positions[3] = point4;
        positions[4] = point5;
        positions[5] = point6;
        positions[6] = point7;
        positions[7] = point8;
        positions[8] = point9;
        positions[9] = point10;
        positions[10] = point11;
        positions[11] = point12;
        positions[12] = point13;
        positions[13] = point14;
        positions[14] = point15;

        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }
}