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

    private GameObject hatObj;

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

    public void SpawnHat()
    {
        if (gameManager.tileSelected == false)
        {
            SpawnHatInternal();
        }
        else
        {
            Destroy(gameManager.selectedTile.gameObject);
            SpawnHatInternal();
        }
    }

    private void SpawnHatInternal()
    {
        if (!isSelected)
        {
            Vector3 mousePos = Input.mousePosition;
            hatObj = Instantiate(hat, mousePos, hat.transform.rotation);

            gameManager.tileSelected = true;
            gameManager.selectedTile = hatObj.gameObject;
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
