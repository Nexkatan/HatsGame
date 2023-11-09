using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChainManager : MonoBehaviour
{
    public GameObject startChain;
    public GameObject endChain;
    private int hatCount;
    public List<GameObject> chainList = new List<GameObject>();
    private UniversalLevelManager levelManager;
    

    private void Start()
    {
        Debug.Log("Awake");
        if (GameObject.FindObjectOfType<StartChain>())
        {
            startChain = GameObject.FindObjectOfType<StartChain>().gameObject;
            chainList.Add(startChain.transform.parent.parent.gameObject);
        }
        if (GameObject.FindObjectOfType<EndChain>())
        {
            endChain = GameObject.FindObjectOfType<EndChain>().transform.parent.parent.gameObject;
        }
        levelManager = GetComponent<UniversalLevelManager>();
    }
    public void AddHatsToList()
    {
        hatCount = GameObject.FindObjectsByType<Chain>(FindObjectsSortMode.None).Length + GameObject.FindObjectsByType<EndChain>(FindObjectsSortMode.None).Length;
        chainList.Clear();
        if (GameObject.FindObjectOfType<StartChain>())
        {
            startChain = GameObject.FindObjectOfType<StartChain>().gameObject;
            chainList.Add(startChain.transform.parent.parent.gameObject);

            if (startChain.transform.parent.parent.gameObject.transform.GetChild(1).GetChild(0).GetComponent<StartChain>().connectedHats.Count > 0) 
            {
                for (int k = 0; k < startChain.transform.parent.parent.gameObject.transform.GetChild(1).GetChild(0).GetComponent<StartChain>().connectedHats.Count; k++)
                {
                    chainList.Add(startChain.transform.parent.parent.gameObject.transform.GetChild(1).GetChild(0).GetComponent<StartChain>().connectedHats[k]);
                }
                for (int i = 1; i < hatCount + 1; i++)
                    {
                    if (i < chainList.Count)
                    {
                        if (chainList[i].transform.GetChild(1).GetChild(0).GetComponent<EndChain>())
                        {
                            Debug.Log("Complete");
                            levelManager.LevelComplete();
                        }
                        else if (chainList[i].transform.GetChild(1).GetChild(0).GetComponent<Chain>().touchingHats.Count > 1)
                        {
                            for (int j = 0; j < chainList[i].transform.GetChild(1).GetChild(0).GetComponent<Chain>().touchingHats.Count; j++)
                            {
                                if (!chainList.Contains(chainList[i].transform.GetChild(1).GetChild(0).GetComponent<Chain>().touchingHats[j]))
                                {
                                        chainList.Add(chainList[i].transform.GetChild(1).GetChild(0).GetComponent<Chain>().touchingHats[j]);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

   
}
     