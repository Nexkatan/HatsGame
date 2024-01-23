using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TilingHoleMaker : MonoBehaviour
{
    public GameObject tiling;
    public HexCell randomCell;
    public GameObject cam;
    private HexGrid hexGrid;

    public int colours;

    List<HexCell> hatsList = new List<HexCell>();
    List<HexCell> reverseHatsList = new List<HexCell>();

    List<HexCell>[] colourHatsList = new List<HexCell>[9];

    string[] colourStrings = new string[9];


    public int width;
    public int height;

    [Range(1.0f, 4.0f)]
    public int difficulty;
    public int multiplier;

    public int hats;
    public int reverseHats;
    public int[] colourHats = new int[9];

    public TextMeshProUGUI hatsNumber;
    public TextMeshProUGUI reverseHatsNumber;

    public TextMeshProUGUI[] colourHatNumbers = new TextMeshProUGUI[9];



    public GameObject HatTab;
    public GameObject HatTabColour;
    public GameObject levelCompleteButtons;
    private List<Button> buttons = new List<Button>();

    public bool colourMode;

    private void Awake()
    {
        colourMode = GameManager.tilingFillColourMode;

        if (colourMode)
        {
            HatTab.SetActive(false);
        }
        else
        {
            HatTabColour.SetActive(false);
        }
    }

    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            colourHatsList[i] = new List<HexCell>();
        }

        colourStrings[0] = "Pink_mat (Instance) (UnityEngine.Material)";
        colourStrings[1] = "Pink_Darker_mat (Instance) (UnityEngine.Material)";
        colourStrings[2] = "Pink_DarkerStill_mat (Instance) (UnityEngine.Material)";
        colourStrings[3] = "Yellow_mat (Instance) (UnityEngine.Material)";
        colourStrings[4] = "Blue_Light_mat (Instance) (UnityEngine.Material)";
        colourStrings[5] = "Blue_Dark_mat (Instance) (UnityEngine.Material)";
        colourStrings[6] = "Green_Light_mat (Instance) (UnityEngine.Material)";
        colourStrings[7] = "Green_Dark_mat (Instance) (UnityEngine.Material)";
        colourStrings[8] = "Purple_mat (Instance) (UnityEngine.Material)";


        tiling = GameObject.FindGameObjectWithTag("Tiling");
        difficulty = GameManager.tilingDifficulty;
        hexGrid = GameObject.FindObjectOfType<HexGrid>();
        HatTab = GameObject.FindGameObjectWithTag("Hat Tab");

        if (HatTab != null)
        {
            foreach (Button button in HatTab.transform.GetChild(0).GetChild(1).GetComponentsInChildren<Button>())
            {
                buttons.Add(button);
            }
        }

        StartCoroutine("CountDown");
    }

    private void Update()
    {
        hatsNumber.SetText(hats.ToString());
        reverseHatsNumber.SetText(reverseHats.ToString());

        for (int i = 0; i < 9; i++)
        {
            colourHatNumbers[i].SetText(colourHats[i].ToString());
        }
    }


    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(.01f);
        DestroyHats();
    }

    public void DestroyHats()
    {
        List<GameObject> hatsToDestroy = new List<GameObject>();
        float divisor = difficulty;
        float divisor2 = (2 + divisor) / divisor;
        if (divisor2 > 2)
        {
            divisor2 = 2;
        }

        float xVal = Random.Range(tiling.transform.position.x - (width * divisor2 * 1.5f), tiling.transform.position.x + (width * divisor2));
        float zVal = Random.Range(tiling.transform.position.z - (height * divisor2 * 1.2f), tiling.transform.position.z + (height * divisor2));

        Vector3 cellCoords = new Vector3(xVal, 0, zVal);
        randomCell = hexGrid.GetCell(cellCoords);
        cam.transform.position = new Vector3(xVal, 150, zVal);
        if (randomCell.hatAbove != null)
        {
            hatsToDestroy.Add(randomCell.hatAbove);
        }
        for (int i = 0; i < 6; i++)
        {
            if (randomCell.GetNeighbor((HexDirection)(i)))
            {
                if (randomCell.GetNeighbor((HexDirection)(i)).hatAbove != null)
                {
                    if (!hatsToDestroy.Contains(randomCell.GetNeighbor((HexDirection)(i)).hatAbove.gameObject))
                    {
                        hatsToDestroy.Add(randomCell.GetNeighbor((HexDirection)(i)).hatAbove.gameObject);
                    }
                }
                if (difficulty > 1)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (randomCell.GetNeighbor((HexDirection)(i)).GetNeighbor((HexDirection)(j)))
                        {
                            if (randomCell.GetNeighbor((HexDirection)(i)).GetNeighbor((HexDirection)(j)).hatAbove != null)
                            {
                                if (!hatsToDestroy.Contains(randomCell.GetNeighbor((HexDirection)(i)).GetNeighbor((HexDirection)(j)).hatAbove.gameObject))
                                {
                                    hatsToDestroy.Add(randomCell.GetNeighbor((HexDirection)(i)).GetNeighbor((HexDirection)(j)).hatAbove.gameObject);
                                }
                            }
                        }
                        if (difficulty > 2)
                        {
                            for (int k = 0; k < 6; k++)
                            {
                                if (randomCell.GetNeighbor((HexDirection)(i)).GetNeighbor((HexDirection)(j)).GetNeighbor((HexDirection)(k)))
                                {
                                    if (randomCell.GetNeighbor((HexDirection)(i)).GetNeighbor((HexDirection)(j)).GetNeighbor((HexDirection)(k)).hatAbove != null)
                                    {
                                        if (!hatsToDestroy.Contains(randomCell.GetNeighbor((HexDirection)(i)).GetNeighbor((HexDirection)(j)).GetNeighbor((HexDirection)(k)).hatAbove.gameObject))
                                        {
                                            hatsToDestroy.Add(randomCell.GetNeighbor((HexDirection)(i)).GetNeighbor((HexDirection)(j)).GetNeighbor((HexDirection)(k)).hatAbove.gameObject);
                                        }
                                    }
                                }
                                if (difficulty > 3)
                                {
                                    for (int l = 0; l < 6; l++)
                                    {
                                        if (randomCell.GetNeighbor((HexDirection)(i)).GetNeighbor((HexDirection)(j)).GetNeighbor((HexDirection)(k)).GetNeighbor((HexDirection)(l)))
                                        {
                                            if (randomCell.GetNeighbor((HexDirection)(i)).GetNeighbor((HexDirection)(j)).GetNeighbor((HexDirection)(k)).GetNeighbor((HexDirection)(l)).hatAbove != null)
                                            {
                                                if (!hatsToDestroy.Contains(randomCell.GetNeighbor((HexDirection)(i)).GetNeighbor((HexDirection)(j)).GetNeighbor((HexDirection)(k)).GetNeighbor((HexDirection)(l)).hatAbove.gameObject))
                                                {
                                                    hatsToDestroy.Add(randomCell.GetNeighbor((HexDirection)(i)).GetNeighbor((HexDirection)(j)).GetNeighbor((HexDirection)(k)).GetNeighbor((HexDirection)(l)).hatAbove.gameObject);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        hatsList.Clear();
        reverseHatsList.Clear();

        for (int i = 0; i < 9; i++)
        {
            colourHatsList[i].Clear();
        }

        for (int i = 0; i < hatsToDestroy.Count; i++)
        {
            if (hatsToDestroy[i].CompareTag("Hat"))
            {
                hatsList.Add(hexGrid.GetCell(hatsToDestroy[i].transform.position));
            }
            else if (hatsToDestroy[i].CompareTag("Reverse Hat"))
            {
                reverseHatsList.Add(hexGrid.GetCell(hatsToDestroy[i].transform.position));
            }

            for (int j = 0; j < 9; j++)
            {
                if (hatsToDestroy[i].GetComponentInChildren<MeshRenderer>().material.ToString() == colourStrings[j])
                {
                    colourHatsList[j].Add(hexGrid.GetCell(hatsToDestroy[i].transform.position));
                }
            }
        }
        hats = hatsList.Count;
        reverseHats = reverseHatsList.Count;
        hatsNumber.SetText(hatsList.Count.ToString());
        reverseHatsNumber.SetText(reverseHatsList.Count.ToString());

        if (colourMode)
        {
            ResetColourButtons();
        }

        StartCoroutine(DestroyHatsList(hatsToDestroy));
    }

    public void LevelComplete()
    {
        HatTab.SetActive(false);
        levelCompleteButtons.SetActive(true);
        foreach (HatPlacer placedHat in GameObject.FindObjectsOfType<HatPlacer>())
        {
            Destroy(placedHat.GetComponent<HatPlacer>());
            Destroy(placedHat.GetComponent<ChecksValid>());
            placedHat.AddComponent<WallHat>();
            placedHat.GetComponent<WallHat>().validityCheck = null;
            Destroy(placedHat.transform.GetChild(0).GetChild(0).GetChild(0).gameObject);
            placedHat.transform.SetParent(tiling.transform);
        }
    }

    IEnumerator DestroyHatsList(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            yield return new WaitForSeconds(15 / (list.Count + 10));
            hexGrid.GetCell(list[i].transform.position).hasHat = false;
            hexGrid.GetCell(list[i].transform.position).hasReverseHat = false;
            hexGrid.GetCell(list[i].transform.position).hatRot = 0;
            hexGrid.GetCell(list[i].transform.position).hatRotInt = 0;
            hexGrid.GetCell(list[i].transform.position).hatAbove = null;
            Destroy(list[i]);
        }
    }

    public void ResetButtons()
    {
        if (!colourMode)
        {
            hatsNumber.SetText(hats.ToString());
            reverseHatsNumber.SetText(reverseHats.ToString());
            if (hats > 0)
            {
                buttons[1].interactable = true;
            }
            if (reverseHats > 0)
            {
                buttons[0].interactable = true;
            }
        }
      else
        {
            ResetColourButtons();
        }
    }

    public void ResetColourButtons()
    {
        for (int i = 0; i < 9; i++)
        {
            colourHats[i] = colourHatsList[i].Count;
            colourHatNumbers[i].SetText(colourHatsList[i].Count.ToString());

            buttons[i].GetComponent<SpawnManager>().numberHats = colourHatsList[i].Count;

            if (colourHats[i] < 1)
            {
                buttons[i].interactable = false;
            }
        }
    }

    public void pReset()
    {
        if (colourMode)
        for (int i = 0; i < 9; i++)
        {
            buttons[i].interactable = true;
        }
    }
}
