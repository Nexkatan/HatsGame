using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shatter : MonoBehaviour
{
    public float radius;

    private void OnEnable()
    {
        Player player = FindObjectOfType<Player>();

        Vector3 explosionPos = transform.position;

        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);


        foreach (Collider hit in colliders)
        {
            if (hit.GetComponent<Rigidbody>())
            {
                hit.GetComponent<Rigidbody>().AddExplosionForce(player.velocity, explosionPos, radius, 1);
            }
        }
    }
}
