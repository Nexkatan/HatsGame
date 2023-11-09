using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyColliders : MonoBehaviour
{
    public GameObject wall;
    public GameObject StartChain;
    public GameObject EndChain;

    private void Start()
    {
        WallifyHats();
    }
    public void WallifyHats()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("Hat") || child.gameObject.CompareTag("Reverse Hat"))
            {
                if (child.GetComponent<HatPlacer>())
                {
                    Destroy(child.GetComponent<HatPlacer>());
                }
                if (child.GetComponent<ChecksValid>())
                {
                    Destroy(child.GetComponent<ChecksValid>());
                }

                Debug.Log(child.gameObject);

                if (!child.GetComponent<WallHat>())
                {
                    child.AddComponent<WallHat>();
                }
                

                if (child.transform.GetChild(0).GetChild(0).childCount > 0)
                {
                    Destroy(child.transform.GetChild(0).GetChild(0).GetChild(0).gameObject);
                }
               

                Destroy(child.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Chain>());

                if (child.gameObject != StartChain && child.gameObject != EndChain)
                {
                    Destroy(child.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Rigidbody>());
                    Collider[] childCollider = child.GetComponentsInChildren<Collider>();
                    foreach (Collider collider in childCollider)
                    {
                        DestroyImmediate(collider);
                    }
                }
                else if (child.gameObject == StartChain)
                {
                    Debug.Log("StartChain");
                    child.transform.GetChild(1).GetChild(0).gameObject.AddComponent<StartChain>();
                }
                else if (child.gameObject == EndChain)
                {
                    Debug.Log("EndChain");
                    child.transform.GetChild(1).GetChild(0).gameObject.AddComponent<EndChain>();
                }
            }

        }
    }
}
