using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class TilingHoleMaker : MonoBehaviour
{
    public GameObject tiling;
    public HexCell randomCell;
    public GameObject cam;
    private HexGrid hexGrid;

    List<HexCell> hatsList = new List<HexCell>();
    List<HexCell> reverseHatsList = new List<HexCell>();

    public int widthStart;
    public int heightStart;
    public int widthEnd;
    public int heightEnd;

    [Range(1.0f, 4.0f)]
    public int difficulty;
    public int multiplier;

    public int hats;
    public int reverseHats;
    public TextMeshProUGUI hatsNumber;
    public TextMeshProUGUI reverseHatsNumber;

    void Start()
    {
        difficulty = GameManager.tilingDifficulty;
        hexGrid = GameObject.Find("HexGrid").GetComponent<HexGrid>();
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

    void DestroyHats()
    {
        List<GameObject> hatsToDestroy = new List<GameObject>();
        int xVal = Random.Range(widthStart + (difficulty * multiplier)/2, widthEnd - (difficulty * multiplier) / 2);
        int zVal = Random.Range(heightStart + (difficulty * multiplier), heightEnd - (difficulty * multiplier));
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
           
        for (int i = 0; i < hatsToDestroy.Count; i++)
        {
            hexGrid.GetCell(hatsToDestroy[i].transform.position).hasHat = false;
            hexGrid.GetCell(hatsToDestroy[i].transform.position).hasReverseHat = false;
            hexGrid.GetCell(hatsToDestroy[i].transform.position).hatRot = 0;
            hexGrid.GetCell(hatsToDestroy[i].transform.position).hatRotInt = 0;
            hexGrid.GetCell(hatsToDestroy[i].transform.position).hatAbove = null;
            Destroy(hatsToDestroy[i]);
        }
    }
}
