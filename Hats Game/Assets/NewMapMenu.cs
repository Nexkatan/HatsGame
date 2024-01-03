using UnityEngine;

public class NewMapMenu : MonoBehaviour
{

    public HexGrid hexGrid;
    public HexMapCamera cam;
    public GameObject backgrounds;
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
        ResetBackgrounds();

        var multiHats = GameObject.FindObjectsOfType<MultiHatPlacer>();

        foreach (var hat in multiHats)
        {
            Destroy(hat.gameObject);
        }

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
        backgrounds.transform.GetChild(0).gameObject.SetActive(true);
        hexGrid.backgroundNumber = 0;
    }
    public void CreateSmallMap()
    {
        CreateMap(20, 15);
        backgrounds.transform.GetChild(1).gameObject.SetActive(true);
        hexGrid.backgroundNumber = 1;
    }

    public void CreateMediumMap()
    {
        CreateMap(40, 30);
        backgrounds.transform.GetChild(2).gameObject.SetActive(true);
        hexGrid.backgroundNumber = 2;
    }

    public void CreateLargeMap()
    {
        CreateMap(80, 60);
        backgrounds.transform.GetChild(3).gameObject.SetActive(true);
        hexGrid.backgroundNumber = 3;
    }

    void ResetBackgrounds()
    {
        for (int i = 0; i < backgrounds.transform.childCount; i++)
        {
            backgrounds.transform.GetChild(i).gameObject.SetActive(false);
            hexGrid.backgroundNumber = 0;
        }
    }
}