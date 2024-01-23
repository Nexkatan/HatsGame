using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BinButton : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject HatTab;
    public List<Button> buttons = new List<Button>();
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        HatTab = GameObject.FindGameObjectWithTag("Hat Tab");
        if (HatTab)
        {
            foreach (Button button in HatTab.transform.GetChild(0).GetChild(1).GetComponentsInChildren<Button>())
            {
                buttons.Add(button);
            }
        }
    }


  public void deleteButton()
    {
        if (gameManager.tileSelected)
        {

            Debug.Log(gameManager.selectedTile.gameObject.GetComponentInChildren<Selecter>());
            Button spawnButton = gameManager.selectedTile.gameObject.GetComponentInChildren<Selecter>().birthButton;
            spawnButton.interactable = true;
            Destroy(gameManager.selectedTile.gameObject);
            gameManager.tileSelected = false;
            gameManager.selectedTile = null;
        }
        if (gameManager.GetComponent<TilingHoleMaker>())
        {
            Debug.Log("Bin");
        }
        else
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].interactable = true;
            }
        }
    }
}
