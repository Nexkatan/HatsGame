using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selecter : MonoBehaviour
{

    private GameManager gameManager;
    private HexGrid hexGrid;

    private GameObject HatTab;
    public List<Button> buttons = new List<Button>();
    public Button birthButton;

    string[] colourStrings = new string[9];


    // Update is called once per frame
    void Start()
    {
        hexGrid = GameObject.FindObjectOfType<HexGrid>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        HatTab = GameObject.FindGameObjectWithTag("Hat Tab");
        if (HatTab)
        {
            foreach (Button button in HatTab.transform.GetChild(0).GetChild(1).GetComponentsInChildren<Button>())
            {
                buttons.Add(button);
            }
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
    }

    private void OnMouseDown()
    {
        GameObject hat = transform.root.gameObject;
        if (!gameManager.tileSelected)
        {
            Debug.Log("Select");
            gameManager.tileSelected = true;


            if (hat.GetComponent<HatPlacer>())
            {
                HexCell landCell = hexGrid.GetCell(transform.position);
                landCell.hasHat = false;
                landCell.hasReverseHat = false;
                landCell.hatRot = 0;
                landCell.hatRotInt = 0;
                landCell.hatAbove = null;
                if (gameManager.GetComponent<TilingHoleMaker>())
                {

                }
                else
                {
                    landCell.hatAboveMat = null;
                }
            }
            else if (hat.GetComponent<MultiHatPlacer>())
            {
                foreach (ChecksValid check in hat.GetComponent<MultiHatPlacer>().validHats)
                {
                    HexCell landCell = hexGrid.GetCell(check.transform.position);
                    landCell.hasHat = false;
                    landCell.hasReverseHat = false;
                    landCell.hatRot = 0;
                    landCell.hatRotInt = 0;
                    landCell.hatAbove = null; 
                    if (gameManager.GetComponent<TilingHoleMaker>())
                    {

                    }
                    else
                    {
                        landCell.hatAboveMat = null;
                    }
                }
            }



            if (birthButton != null && birthButton.GetComponent<SpawnManager>().oppositeButton != null)
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].interactable = false;
                }
                birthButton.GetComponent<SpawnManager>().oppositeButton.interactable = true;
            }
            if (gameManager.GetComponent<TilingHoleMaker>())
            {
                if (this.transform.root.CompareTag("Hat"))
                {
                    gameManager.GetComponent<TilingHoleMaker>().hats += 1;
                }
                else if (this.transform.root.CompareTag("Reverse Hat"))
                {
                    gameManager.GetComponent<TilingHoleMaker>().reverseHats += 1;
                }

                if (gameManager.GetComponent<TilingHoleMaker>().colourMode)
                {
                    for (int i = 0;  i < 9 ; i++)
                    {
                        if (transform.root.GetComponentInChildren<MeshRenderer>().material.ToString() == colourStrings[i])
                        {
                            gameManager.GetComponent<TilingHoleMaker>().colourHats[i] += 1;
                        }
                        if (gameManager.GetComponent<TilingHoleMaker>().colourHats[i] > 0)
                        {
                            buttons[i].interactable = true;
                        }
                    }
                }
            }
            StartCoroutine(TrueSelecta(hat));
            gameManager.selectedTile = hat.gameObject;
        }
    }

    IEnumerator TrueSelecta(GameObject hat)
    {
        yield return new WaitForSeconds(0.05f);
        if (hat.GetComponent<HatPlacer>())
        {
            hat.GetComponent<HatPlacer>().isSelected = true;
        }
        if (hat.GetComponent<MultiHatPlacer>())
        {
            hat.GetComponent<MultiHatPlacer>().isSelected = true;
        }
    }

}
