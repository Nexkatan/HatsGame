using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{

    public GameObject hat;
    public bool isSelected;
    public bool isPlaced;
    private Vector3 buttonPos;

    private GameManager gameManager;

    private GameObject HatTab;
    private List<Button> buttons = new List<Button>();

    public Button oppositeButton;

    public int numberHats;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        buttonPos = transform.position;
    }

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

    private void OnMouseDown()
    {
        SpawnHat();
    }


    public void SpawnHat()
    {
        if (gameManager.selectedTile != null)
        {
            if (gameManager.GetComponent<TilingHoleMaker>() == null)
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].interactable = true;
                }
            }
            else
            {
                string[] colourStrings = new string[9];
                colourStrings[0] = "Pink_mat (Instance) (UnityEngine.Material)";
                colourStrings[1] = "Pink_Darker_mat (Instance) (UnityEngine.Material)";
                colourStrings[2] = "Pink_DarkerStill_mat (Instance) (UnityEngine.Material)";
                colourStrings[3] = "Yellow_mat (Instance) (UnityEngine.Material)";
                colourStrings[4] = "Blue_Light_mat (Instance) (UnityEngine.Material)";
                colourStrings[5] = "Blue_Dark_mat (Instance) (UnityEngine.Material)";
                colourStrings[6] = "Green_Light_mat (Instance) (UnityEngine.Material)";
                colourStrings[7] = "Green_Dark_mat (Instance) (UnityEngine.Material)";
                colourStrings[8] = "Purple_mat (Instance) (UnityEngine.Material)";

                for (int i = 0; i < colourStrings.Length;i++)
                {
                    if (gameManager.selectedTile.GetComponentInChildren<MeshRenderer>().material.ToString() == colourStrings[i])
                    {
                        buttons[i].interactable = true;
                    }
                }
            }
            Destroy(gameManager.selectedTile.gameObject);
        }
        
        if (!isSelected) 
        {
            GetComponent<Button>().interactable = false;
            Vector3 mousePos = Input.mousePosition;
            GameObject hatObj = Instantiate(hat, mousePos, hat.transform.rotation);
            hatObj.transform.GetComponentInChildren<Selecter>().birthButton = GetComponent<Button>();

            if (this.CompareTag("Hat") || this.CompareTag("Reverse Hat"))
            {
                hatObj.gameObject.GetComponent<HatPlacer>().isSelected = true;
            }
            else if ((this.CompareTag("Multi Hat"))) 
            {
                hatObj.gameObject.GetComponent<MultiHatPlacer>().isSelected = true;
            }
            gameManager.tileSelected = true;
            gameManager.selectedTile = hatObj.gameObject;

            /*
            if (oppositeButton != null)
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].interactable = false;
                }
                oppositeButton.interactable = true;
            }
            */
        }
        
    }
    HexCell GetCellUnderCursor()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 200;
        Ray inputRay = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            //return hexGrid.ColorCell(hit.point);
        }
        return null;
    }

    public void ResetHatTab()
    {
        /*gameManager.GetComponent<TilingHoleMaker>().HatTab.SetActive(true);
        Debug.Log(gameManager.GetComponent<TilingHoleMaker>().HatTab);
        foreach (Button button in gameManager.GetComponent<TilingHoleMaker>().HatTab.transform.GetChild(0).GetChild(1).GetComponentsInChildren<Button>())
        {
            buttons.Add(button);
        }
        Debug.Log(buttons.Count);
        for (int i = 0; i < buttons.Count; i++)
        {
            Debug.Log(buttons[i].GetComponent<SpawnManager>().numberHats);
            if (buttons[i].GetComponent<SpawnManager>().numberHats > 0)
            {
                buttons[i].interactable = true;
            }
        }
        */
        gameManager.GetComponent<TilingHoleMaker>().HatTab.SetActive(true);
        gameManager.GetComponent<TilingHoleMaker>().ResetButtons();
    }

    public void ResetHatrisHatTab()
    {
        HatTab.SetActive(true);
        HatTab.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<Button>().interactable = true;
        HatTab.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<UnityEngine.UI.Button>().interactable = true;
    }

    public void FlashButtonFunction(int amountTimes, float flashDuration)
    {
        StartCoroutine(FlashButton(amountTimes, flashDuration));
    }

    IEnumerator FlashButton(int amountTimes, float flashDuration)
    {
        Debug.Log("Flash");
        Button button = this.GetComponent<Button>();
        for (int i = 0; i < amountTimes; i++)
        {
            button.interactable = true;
            yield return new WaitForSeconds(flashDuration);
            button.interactable = false;
            yield return new WaitForSeconds(flashDuration);
        }
    }
        
}
