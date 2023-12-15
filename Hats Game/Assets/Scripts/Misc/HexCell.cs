using UnityEngine;

public class HexCell : MonoBehaviour
{
    public HexCoordinates coordinates;
    public Color color;
    public float inlaySize;

    public bool hasHat;
    public bool hasReverseHat;
    public bool isBinHat;
    public float hatRot;
    public int hatRotInt;
    public GameObject hatAbove;

    [SerializeField]
    HexCell[] neighbors;
    HexCell[] longbois;

    private void Start()
    {
    }

    public HexCell GetNeighbor(HexDirection direction)
    {
        return neighbors[(int)direction];
    }

    public HexCell GetLongboi(LongboiDirection direction)
    {
        return longbois[(int)direction];
    }

    public void SetNeighbor(HexDirection direction, HexCell cell)
    {
        neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;
    }

}