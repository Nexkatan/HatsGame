using UnityEngine;
using System.IO;

public class HexCell : MonoBehaviour
{
    public HexCoordinates coordinates;

    public int terrainTypeIndex;

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

    public RectTransform uiRect;

    public HexGridChunk chunk;



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

    public Material Color
    {
        get
        {
            return HexMetrics.materials[terrainTypeIndex];
        }
    }

    public Color MatToColor
    {
        get
        {
            return HexMetrics.materials[terrainTypeIndex].color;
        }
    }

    public int TerrainTypeIndex
    {
        get
        {
            return terrainTypeIndex;
        }
        set
        {
            if (terrainTypeIndex != value)
            {
                terrainTypeIndex = value;
                Refresh();
            }
        }
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write((byte)terrainTypeIndex);
        
    }

    public void Load(BinaryReader reader)
    {
        terrainTypeIndex = reader.ReadByte();
        GetComponent<MeshRenderer>().material = HexMetrics.materials[terrainTypeIndex];
    }
    void Refresh()
    {
        chunk.Refresh();
    }
}