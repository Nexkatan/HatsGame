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
        if (GameObject.FindObjectOfType<StartChain>())
        {
            startChain = GameObject.FindObjectOfType<StartChain>().gameObject;
            chainList.Add(startChain.transform.parent.parent.gameObject);
        }
        if (GameObject.FindObjectOfType<EndChain>())
        {
            endChain = GameObject.FindObjectOfType<EndChain>().transform.root.gameObject;
        }
        
        
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
            
            
                Debug.Log(hatCount);
                for (int i = 1; i < hatCount + 1; i++)
                    {
                    if (i < chainList.Count)
                    {
                        if (chainList[i].transform.GetChild(1).GetChild(0).GetComponent<EndChain>())
                        {
                            Debug.Log("Complete");
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
     