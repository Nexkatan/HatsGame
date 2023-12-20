using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.IO;
using System;

public class HexGrid : MonoBehaviour
{
    public bool HexCoordinatesOn;

    int chunkCountX, chunkCountZ;

    public HexCell cellPrefab;
    public HatrisHexCell cellCellPrefab;

    HexCell[] cells;
    HatrisHexCell[] cellCells;

    public TextMeshProUGUI cellLabelPrefab;
    
    public Material[] materials;

    public bool cellCell;

    public HexGridChunk chunkPrefab;

    public int cellCountX = 20, cellCountZ = 15;

    HexGridChunk[] chunks;



    public bool isHatris;
    public Vector2 centrePoint;
    public int HexMapRadius;

    public SaveLoadMenu SaveLoadMenu;

    public HexMapCamera cam;

    void Awake()
    {
        cellCountX = chunkCountX * HexMetrics.chunkSizeX;
        cellCountZ = chunkCountZ * HexMetrics.chunkSizeZ;
        
        HexMetrics.materials = materials;
        
        if (isHatris )
        {
            loadHatrisHex();
            cam.transform.position = new Vector3(100f, 0f, 50f);
        }
    }

    void OnEnable()
    {
            HexMetrics.materials = materials;
    }

    public void ColorCell(Vector3 position, Color color)
    {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position); 
        int index = coordinates.X + coordinates.Z * cellCountX + coordinates.Z / 2;
        HexCell cell = cells[index];
    }

    public HexCell GetCell(Vector3 position)
    {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        int index = coordinates.X + coordinates.Z * cellCountX + coordinates.Z / 2;
        if (index > -1 && index < cells.Length)
        {
            HexCell cell = cells[index];
            return cell;
        }
        return null;
    }

    public HatrisHexCell GetHatrisHexCell(Vector3 position)
    {
        return null;
    }

    public void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f * cellPrefab.inlaySize);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f * cellPrefab.inlaySize);



        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);


        if (x > 0)
        {
            cell.SetNeighbor(HexDirection.W, cells[i-1]);
        }
        if (z > 0)
        {
            if ((z & 1) == 0)
            {
                cell.SetNeighbor(HexDirection.SE, cells[i - cellCountX]);
                if (x > 0)
                {
                    cell.SetNeighbor(HexDirection.SW, cells[i - cellCountX - 1]);
                }
            }
            else
            {
                cell.SetNeighbor(HexDirection.SW, cells[i - cellCountX]);
                if (x < cellCountX - 1)
                {
                    cell.SetNeighbor(HexDirection.SE, cells[i - cellCountX + 1]);
                }
            }
        }


        TextMeshProUGUI label = Instantiate<TextMeshProUGUI>(cellLabelPrefab);
       
        label.rectTransform.anchoredPosition =
        new Vector2(position.x, position.z / cellPrefab.inlaySize);
        cell.uiRect = label.rectTransform;

        AddCellToChunk(x, z, cell);

        if (HexCoordinatesOn)
        {
            label.text = cell.coordinates.ToStringOnSeparateLines();
        };

        if (cellCell)
        {
            CreateCellCellsHex(cell);
        }
    }

    public void CreateCells()
    {
        cells = new HexCell[cellCountZ * cellCountX];

        for (int z = 0, i = 0; z < cellCountZ; z++)
        {
            for (int x = 0; x < cellCountX; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }


    void CreateChunks()
    {
        chunks = new HexGridChunk[chunkCountX * chunkCountZ];

        for (int z = 0, i = 0; z < chunkCountZ; z++)
        {
            for (int x = 0; x < chunkCountX; x++)
            {
                HexGridChunk chunk = chunks[i++] = Instantiate(chunkPrefab);
                chunk.transform.SetParent(transform);
            }
        }
    }

    void AddCellToChunk(int x, int z, HexCell cell)
    {
        int chunkX = x / HexMetrics.chunkSizeX;
        int chunkZ = z / HexMetrics.chunkSizeZ;
        HexGridChunk chunk = chunks[chunkX + chunkZ * chunkCountX];

        int localX = x - chunkX * HexMetrics.chunkSizeX;
        int localZ = z - chunkZ * HexMetrics.chunkSizeZ;
        chunk.AddCell(localX + localZ * HexMetrics.chunkSizeX, cell);
    }

    public bool CreateMap(int x, int z)
    {
        if (
            x <= 0 || x % HexMetrics.chunkSizeX != 0 ||
            z <= 0 || z % HexMetrics.chunkSizeZ != 0
        )
        {
            Debug.LogError("Unsupported map size.");
            return false;
        }

        if (chunks != null)
        {
            for (int i = 0; i < chunks.Length; i++)
            {
                Destroy(chunks[i].gameObject);
            }
        }

        cellCountX = x;
        cellCountZ = z;
        chunkCountX = cellCountX / HexMetrics.chunkSizeX;
        chunkCountZ = cellCountZ / HexMetrics.chunkSizeZ;
        CreateChunks();
        CreateCells();
        return true;
    }
    
    public void CreateCellCellsHex(HexCell cell)
    {
            Transform cellChild = cell.transform.GetChild(0);

        if (cell.coordinates.X > centrePoint.x - HexMapRadius && cell.coordinates.X < centrePoint.x + HexMapRadius && cell.coordinates.Z > centrePoint.y - HexMapRadius && cell.coordinates.Z < centrePoint.y + HexMapRadius && -(cell.coordinates.X + cell.coordinates.Z) > -(centrePoint.x + centrePoint.y) - HexMapRadius && -(cell.coordinates.X + cell.coordinates.Z) < -(centrePoint.x + centrePoint.y) + HexMapRadius)
        {
            for (int j = 0; j < 6; j++)
            {
                HatrisHexCell hatrisCell = Instantiate<HatrisHexCell>(cellCellPrefab);

                TextMeshProUGUI label = Instantiate<TextMeshProUGUI>(cellLabelPrefab);

                label.rectTransform.SetParent(hatrisCell.transform);
                label.rectTransform.anchoredPosition = new Vector3(1 * cellCountZ, 0.5f, 0);

                hatrisCell.transform.position = cell.transform.position;

                if (HexCoordinatesOn)
                {
                    label.text = hatrisCell.triCoordinates.ToStringOnSeparateLines();
                };

                hatrisCell.name = ("hatrisCell" + j);
                hatrisCell.transform.SetParent(cellChild);
                hatrisCell.transform.RotateAround(cell.transform.position, Vector3.up, 60 * j);
            }
        }
        
    }


    public void Save(BinaryWriter writer)
    {
        writer.Write(cellCountX);
        writer.Write(cellCountZ);

        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].Save(writer);
        }
    }

    public void Load(BinaryReader reader, int header)
    {
        int x = cellCountX, z = cellCountZ;
        if (header >= 1)
        {
            x = reader.ReadInt32();
            z = reader.ReadInt32();
        }

        if (x != cellCountX || z != cellCountZ)
        {
            if (!CreateMap(x, z))
            {
                return;
            }
        }

        CreateMap(x, z);

        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].Load(reader);
        }
        for (int i = 0; i < chunks.Length; i++)
        {
            chunks[i].Refresh();
        }
    }

    void loadHatrisHex()
    {
        String path = "C:/Users/Gabe/AppData/LocalLow/DefaultCompany/Hats Game\\Hatris Map.map";
        SaveLoadMenu.Load(path);
        
    }

}
