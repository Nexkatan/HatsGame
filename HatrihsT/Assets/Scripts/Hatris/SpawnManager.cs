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

    public HexMapCamera cam;

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
            // Variable for rotation adjustment
            float rotDelta = 0f;

            // Check if the object is flipped (scale.x is negative)
            if (this.transform.localScale.x < 0)
            {
                // If flipped, adjust yaw to snap to the nearest 60-degree interval starting from +30 degrees
                rotDelta = Mathf.Round((cam.currentYaw + 60f) / 60f) * 60f - 60f;
            }
            else
            {
                // If not flipped, adjust yaw to snap to the nearest 60-degree interval starting from -30 degrees
                rotDelta = Mathf.Round((cam.currentYaw) / 60f) * 60f;
            }

            // Create the yaw rotation based on the snapped yaw
            Quaternion yawRotation = Quaternion.Euler(0f, rotDelta, 0f);

            // Get the saved rotation of the hat
            Quaternion savedRotation = hat.transform.rotation;

            // Combine the saved rotation and the calculated yaw rotation
            Quaternion combinedRotation = savedRotation * yawRotation;

            // Get the mouse position and convert it to world space
            Vector3 mousePos = Input.mousePosition;
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

            // Instantiate the hat object with the calculated position and rotation
            hatObj = Instantiate(hat, spawnPosition, combinedRotation);

            // Mark the tile as selected and assign the spawned object to the selected tile
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
