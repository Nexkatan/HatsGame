using UnityEngine;

public class NewMapMenu : MonoBehaviour
{

    public HexGrid hexGrid;
    public HexMapCamera cam;
    public void Open()
    {
        gameObject.SetActive(true);
        HexMapCamera.Locked = true;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        HexMapCamera.Locked = false;
    }

    void CreateMap(int x, int z)
    {
        hexGrid.CreateMap(x, z);
        Close();
        var hats = GameObject.FindObjectsOfType<HatPlacer>();
        foreach (HatPlacer hat in hats)
        {
            Destroy(hat.gameObject);
        }
        HexMapCamera.ValidatePosition(cam);
    }
    public void CreateTinyMap()
    {
        CreateMap(8, 6);
    }
    public void CreateSmallMap()
    {
        CreateMap(20, 15);
    }

    public void CreateMediumMap()
    {
        CreateMap(40, 30);
    }

    public void CreateLargeMap()
    {
        CreateMap(80, 60);
    }
}