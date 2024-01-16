using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChainManager : MonoBehaviour
{
    public GameObject startChainHat;
    public GameObject endChainHat;
    private int hatCount;
    public List<GameObject> chainList = new List<GameObject>();
    private UniversalLevelManager levelManager;
    

    private void Start()
    {
        levelManager = GetComponent<UniversalLevelManager>();
    }
    public void AddHatsToList()
    {
        hatCount = GameObject.FindObjectsByType<Chain>(FindObjectsSortMode.None).Length + GameObject.FindObjectsByType<EndChain>(FindObjectsSortMode.None).Length;
        chainList.Clear();
        if (GameObject.FindObjectOfType<StartChain>())
        {
            GameObject startChain = GameObject.FindObjectOfType<StartChain>().gameObject;
            startChainHat = startChain.transform.parent.parent.gameObject;
            GameObject endChain = GameObject.FindObjectOfType<EndChain>().gameObject;
            endChainHat = endChain.transform.parent.parent.gameObject;
            Debug.Log(startChain);
            chainList.Add(startChainHat);

            Debug.Log(startChain.GetComponent<StartChain>().connectedHats.Count);
            Debug.Log(startChain.GetComponent<StartChain>().connectedHats[0]);

            if (startChain.GetComponent<StartChain>().connectedHats.Count > 0) 
            {
                for (int k = 0; k < startChain.GetComponent<StartChain>().connectedHats.Count; k++)
                {
                    chainList.Add(startChain.GetComponent<StartChain>().connectedHats[k]);
                }
                for (int i = 1; i < hatCount + 1; i++)
                    {
                    if (i < chainList.Count)
                    {
                        if (chainList[i].gameObject.GetComponentInChildren<EndChain>())
                        {
                            Debug.Log("Complete");
                            levelManager.LevelComplete();
                        }
                        else if (chainList[i].GetComponentInChildren<Chain>().touchingHats.Count > 1)
                        {
                            for (int j = 0; j < chainList[i].GetComponentInChildren<Chain>().touchingHats.Count; j++)
                            {
                                if (!chainList.Contains(chainList[i].GetComponentInChildren<Chain>().touchingHats[j]))
                                {
                                        chainList.Add(chainList[i].GetComponentInChildren<Chain>().touchingHats[j]);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

   
}
     