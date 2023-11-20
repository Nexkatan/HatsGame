using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMultiHats : MonoBehaviour
{
   
    void Start()
    {
        foreach (Transform transform in this.transform)
        {
            if (transform.CompareTag("Multi Hat"))
            {
                Debug.Log("Error");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
