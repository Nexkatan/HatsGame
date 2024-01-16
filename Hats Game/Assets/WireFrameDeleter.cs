using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireFrameDeleter : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            //transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).GetComponent<Selecter>().buttons.Clear();
            if (transform.GetChild(i).childCount > 1)
            {
                Destroy(transform.GetChild(i).GetChild(1).gameObject);
            }
        }
    }


    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).GetComponent<Selecter>().buttons.Clear();
        }
    }
}
