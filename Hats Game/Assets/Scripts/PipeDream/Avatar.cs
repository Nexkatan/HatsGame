using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Avatar : MonoBehaviour
{
    private Player player;

    private ParticleSystem burst;
    private float deathCountdown = -1f;


    private void Awake()
    {
        player = transform.root.GetComponent<Player>();
        burst = player.burst;
    }
    private void Update()
    {
        if (deathCountdown >= 0f)
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
            ParticleSystem.MainModule main = burst.main;
            var em = main.maxParticles;
            burst.Emit(em);
            player.acceleration = -(player.velocity * player.velocity);
            deathCountdown -= Time.deltaTime;
            if (deathCountdown <= 0f)
            {
                deathCountdown = -1f;
                player.Die();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        float wrap = ((UnityEditor.TransformUtils.GetInspectorRotation(this.transform).x + 360) % 360);
        float gateWrap = ((UnityEditor.TransformUtils.GetInspectorRotation(other.transform.parent).x + 240) % 360);
        float reverseGateWrap = ((UnityEditor.TransformUtils.GetInspectorRotation(other.transform.parent).x + 300) % 360);

        if (other.transform.GetComponentInParent<PipeItem>().isReverse)
        {
            if (transform.GetChild(0).localScale.x < 0)
            {
                if (wrap == reverseGateWrap)
                {
                    Debug.Log("Pass");
                }
                else
                {
                    if (deathCountdown < 0)
                    {
                        Dying(other);
                    }
                }
            }
            else
            {
                Dying(other);
            }
            
        }
        else if (!other.transform.GetComponentInParent<PipeItem>().isReverse)
        {
            if (transform.GetChild(0).localScale.x > 0)
            {
                if (wrap == gateWrap)
                {
                    Debug.Log("Pass");
                }
                else
                {
                    if (deathCountdown < 0)
                    {
                        Dying(other);
                    }
                }
            }
            else
            {
                Dying(other);
            }
        }
       
    }

    private void Dying(Collider other)
    {
        Debug.Log(other.transform.parent.parent.GetChild(0).gameObject);
        other.transform.parent.parent.GetChild(1).gameObject.SetActive(true);
        other.transform.parent.gameObject.SetActive(false);

        ParticleSystem.MainModule main = burst.main;
        deathCountdown = main.startLifetime.constant;
    }

}
