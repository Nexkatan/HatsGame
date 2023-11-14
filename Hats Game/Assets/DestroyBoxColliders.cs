using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBoxColliders : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.GetChild(0).GetChild(0).GetChild(0).gameObject);
        }

    }
}
