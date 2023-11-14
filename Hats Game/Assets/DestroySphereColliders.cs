using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroySphereColliders : MonoBehaviour
{

    void Start()
    {
        foreach (Transform child in transform)
        {
            Collider[] childCollider = child.GetComponentsInChildren<SphereCollider>();
            foreach (SphereCollider collider in childCollider)
            {
                DestroyImmediate(collider);
            }
        }
        foreach (Transform child in transform)
        {
            Collider[] childCollider = child.GetComponentsInChildren<CapsuleCollider>();
            foreach (CapsuleCollider collider in childCollider)
            {
                DestroyImmediate(collider);
            }
        }

    }

    
}
