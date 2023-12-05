using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PipeSystem pipeSystem;

    public float velocity;
    public float rotationVelocity;

    public Pipe currentPipe;

    private float distanceTraveled;

    private float deltaToRotation;
    private float systemRotation;

    private Transform world, rotater;
    private float worldRotation, avatarRotation;

    public float worldRot;

    private Camera mainCam;

    private void Start()
    {
        world = pipeSystem.transform.parent;
        rotater = transform.GetChild(0);
        currentPipe = pipeSystem.SetupFirstPipe();
        deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
        SetupCurrentPipe();
        mainCam  = GameObject.Find("Main Camera").GetComponent<Camera>();
    }


    private void Update()
    {
        float delta = velocity * Time.deltaTime;
        distanceTraveled += delta;
        systemRotation += delta * deltaToRotation;
       
        if (systemRotation >= currentPipe.CurveAngle)
        {
            delta = (systemRotation - currentPipe.CurveAngle) / deltaToRotation;
            currentPipe = pipeSystem.SetupNextPipe();
            SetupCurrentPipe();
            systemRotation = delta * deltaToRotation;
        }

        pipeSystem.transform.localRotation =
           Quaternion.Euler(0f, 0f, systemRotation);
        FlipHat();
        UpdateAvatarRotation();

        worldRot = UnityEditor.TransformUtils.GetInspectorRotation(world.transform).x;
    }

    private void SetupCurrentPipe()
    {
        deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
        worldRotation += currentPipe.RelativeRotation;
        if (worldRotation < 0f)
        {
            worldRotation += 360f;
        }
        else if (worldRotation >= 360f)
        {
            worldRotation -= 360f;
        }
        world.localRotation = Quaternion.Euler(worldRotation, 0f, 0f);
    }

    private void UpdateAvatarRotation()
    {
        avatarRotation +=
            rotationVelocity * Time.deltaTime * Input.GetAxis("Horizontal");
        if (avatarRotation < 0f)
        {
            avatarRotation += 360f;
        }
        else if (avatarRotation >= 360f)
        {
            avatarRotation -= 360f;
        }
        rotater.localRotation = Quaternion.Euler(avatarRotation, 0f, 0f);
    }

    private void FlipHat()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Transform playerHat = rotater.GetChild(0).GetChild(0).transform;
            Vector3 m_EulerAngleVelocityPos = new Vector3(0, 60, 0);
            Vector3 m_EulerAngleVelocityNeg = new Vector3(0, -60, 0);
            Quaternion deltaRotationPos = Quaternion.Euler(m_EulerAngleVelocityPos);
            Quaternion deltaRotationNeg = Quaternion.Euler(m_EulerAngleVelocityNeg);

            if (playerHat.localScale.x < 0)
            {
                playerHat.rotation *= deltaRotationNeg;
            }
            else
            {
                playerHat.rotation *= deltaRotationPos;
            }
            playerHat.localScale = new Vector3(-playerHat.localScale.x, 0.1f, 0.1f);
        }
    }
}
