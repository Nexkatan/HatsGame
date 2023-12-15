using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shatter : MonoBehaviour
{
    public float radius, upForce;

    public GameObject brokenGate;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        GameObject brokenModel = Instantiate(brokenGate, transform);
        this.gameObject.SetActive(false);
        foreach (Collider hit in colliders)
        {
            if (hit.GetComponent<Rigidbody>())
            {
                Debug.Log("Hit");
                hit.gameObject.AddComponent<Rigidbody>();
                hit.GetComponent<Rigidbody>().AddExplosionForce(5 * collision.relativeVelocity.magnitude, explosionPos, radius, 1);
            }
        }
    }
}
