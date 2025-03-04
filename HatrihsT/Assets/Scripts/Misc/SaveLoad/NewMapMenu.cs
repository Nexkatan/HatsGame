using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewMapMenu : MonoBehaviour
{

    public HexGrid hexGrid;
    public HexMapCamera cam;
    public GameObject backgrounds;
    private GameObject HatTab;

    public List<Button> buttons = new List<Button>();

    private void Start()
    {
        HatTab = GameObject.FindGameObjectWithTag("Hat Tab");

        if (HatTab)
        {
            foreach (Button button in HatTab.transform.GetChild(0).GetChild(1).GetComponentsInChildren<Button>())
            {
                buttons.Add(button);
            }
        }
    }
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

    public void LoadMap(int x)
    {
        if (x == 8)
        {
            CreateTinyMap();
        }
        else if (x == 20)
        {
            CreateSmallMap();
        }
        else if (x == 40)
        {
            CreateMediumMap();
        }
        else if (x == 90)
        {
            CreateLargeMap();
        }
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

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;
        }

        HexMapCamera.ValidatePosition(cam);
        
    }
    public void CreateTinyMap()
    {
        CreateMap(8, 6);
        GameObject bg = backgrounds.transform.GetChild(0).gameObject;
        bg.SetActive(true);
        hexGrid.backgroundNumber = 0;
        cam.transform.position = new Vector3(65, 135, 85);
        cam.transform.GetChild(0).GetChild(0).localPosition = new Vector3(0, 0, 60);
    }
    public void CreateSmallMap()
    {
        CreateMap(20, 15);
        GameObject bg = backgrounds.transform.GetChild(1).gameObject;
        bg.SetActive(true);
        hexGrid.backgroundNumber = 1;
        cam.transform.position = new Vector3(170, 135, 135);
        cam.transform.GetChild(0).GetChild(0).localPosition = new Vector3(0, 0, -52);
    }

    public void CreateMediumMap()
    {
        CreateMap(40, 30);
        GameObject bg = backgrounds.transform.GetChild(2).gameObject;
        bg.SetActive(true);
        hexGrid.backgroundNumber = 2;
        cam.transform.position = new Vector3(340, 135, 212.5f);
        cam.transform.GetChild(0).GetChild(0).localPosition = new Vector3(0, 0, -220);
    }

    public void CreateLargeMap()
    {
        CreateMap(80, 60);
        GameObject bg = backgrounds.transform.GetChild(3).gameObject;
        bg.SetActive(true);
        hexGrid.backgroundNumber = 3;
        cam.transform.position = new Vector3(680, 135, 350);
        cam.transform.GetChild(0).GetChild(0).localPosition = new Vector3(0, 0, -500);
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