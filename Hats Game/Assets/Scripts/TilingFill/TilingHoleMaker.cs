using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TilingHoleMaker : MonoBehaviour
{
    public GameObject tiling;
    public HexCell randomCell;
    public GameObject cam;
    private HexGrid hexGrid;

    List<HexCell> hatsList = new List<HexCell>();
    List<HexCell> reverseHatsList = new List<HexCell>();

    public int width;
    public int height;

    [Range(1.0f, 4.0f)]
    public int difficulty;
    public int multiplier;

    public int hats;
    public int reverseHats;
    public TextMeshProUGUI hatsNumber;
    public TextMeshProUGUI reverseHatsNumber;

    public GameObject HatTab;
    public GameObject levelCompleteButtons;

    void Start()
    {

        tiling = GameObject.FindGameObjectWithTag("Tiling");
        difficulty = GameManager.tilingDifficulty;
        hexGrid = GameObject.FindObjectOfType<HexGrid>();
        HatTab = GameObject.Find("HatTab");
        StartCoroutine("CountDown");
    }

    private void Update()
    {
        hatsNumber.SetText(hats.ToString());
        reverseHatsNumber.SetText(reverseHats.ToString());
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

        /*
        float xVal = tiling.transform.position.x - (width * divisor2 * 1.5f);
        float zVal = tiling.transform.position.z - (height * divisor2 * 1.2f);
       */


        Vector3 cellCoords = new Vector3(xVal,0,zVal);
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
        }

        hats = hatsList.Count;
        reverseHats = reverseHatsList.Count;
        hatsNumber.SetText(hatsList.Count.ToString());
        reverseHatsNumber.SetText(reverseHatsList.Count.ToString());


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
            placedHat.GetComponent<WallHat>().validityCheck = null;
            Destroy(placedHat.transform.GetChild(0).GetChild(0).GetChild(0).gameObject);
            placedHat.transform.SetParent(tiling.transform);
        }
    }

    public void ResetHats()
    {
        HatTab.SetActive(true);
        HatTab.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<UnityEngine.UI.Button>().interactable = true;
        HatTab.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<UnityEngine.UI.Button>().interactable = true;
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
}
