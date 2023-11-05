using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainManager : MonoBehaviour
{
    private GameObject startChain;
    private int hatCount;
    public GameObject endChain;
    public List<GameObject> chainList = new List<GameObject>();

    private void Awake()
    {
        startChain = GameObject.FindObjectOfType<StartChain>().gameObject;
        endChain = GameObject.FindObjectOfType<EndChain>().transform.root.gameObject;
        chainList.Add(startChain.transform.root.gameObject);  
    }
   

    public void checkComplete()
    {
        AddHatsToList();
    }

   
    public void AddHatsToList()
    {
        hatCount = GameObject.FindObjectsByType<Chain>(FindObjectsSortMode.None).Length + GameObject.FindObjectsByType<EndChain>(FindObjectsSortMode.None).Length;
        chainList.Clear();
        chainList.Add(startChain.transform.root.gameObject);
        if (startChain.transform.root.GetChild(1).GetChild(0).GetComponent<StartChain>().connectedHats.Count > 0) 
        {
            chainList.Add(startChain.transform.root.GetChild(1).GetChild(0).GetComponent<StartChain>().connectedHats[0]);
            Debug.Log(hatCount);
            for (int i = 1; i < hatCount; i++)
            {
                if (i < chainList.Count)
                {
                    if (chainList[i].transform.root.GetChild(1).GetChild(0).GetComponent<EndChain>())
                    {
                        Debug.Log("Complete");
                    }
                    else if (chainList[i].transform.root.GetChild(1).GetChild(0).GetComponent<Chain>().touchingHats.Count > 1)
                        {
                            for (int j = 0; j < chainList[i].transform.root.GetChild(1).GetChild(0).GetComponent<Chain>().touchingHats.Count; j++)
                            {
                                if (!chainList.Contains(chainList[i].transform.root.GetChild(1).GetChild(0).GetComponent<Chain>().touchingHats[j]))
                                {
                                    chainList.Add(chainList[i].transform.root.GetChild(1).GetChild(0).GetComponent<Chain>().touchingHats[j]);
                                }
                            }
                        }
                }
            }
        }
    }
}
     