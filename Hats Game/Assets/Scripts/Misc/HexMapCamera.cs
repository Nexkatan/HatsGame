using Unity.VisualScripting;
using UnityEngine;

public class HexMapCamera : MonoBehaviour
{

    Transform swivel, stick;

    float zoom = 1f;

    public float stickMinZoom, stickMaxZoom, swivelMinZoom, swivelMaxZoom, moveSpeedMinZoom, moveSpeedMaxZoom, rotationSpeed, rotationAngle, swivelSpeed, swivelAngle;

    public HexGrid grid;

    static HexMapCamera instance;

    void Awake()
    {
        swivel = transform.GetChild(0);
        stick = swivel.GetChild(0);
        instance = this;
    }

    void OnEnable()
    {
        instance = this;
    }


    void Update()
    {
        float zoomDelta = Input.GetAxis("Mouse ScrollWheel");
        if (zoomDelta != 0f)
        {
            AdjustZoom(zoomDelta);
        }

        float rotationDelta = Input.GetAxis("Rotation");
        if (rotationDelta != 0f)
        {
            AdjustRotation(rotationDelta);
        }

        float xDelta = Input.GetAxis("Horizontal");
        float zDelta = Input.GetAxis("Vertical");
        if (xDelta != 0f || zDelta != 0f)
        {
            AdjustPosition(xDelta, zDelta);
        }
        float sDelta = Input.GetAxis("Swivel");
        if (sDelta != 0f)
        {
            AdjustSwivel(sDelta);
        }
    }

    void AdjustZoom(float delta)
    {
        zoom = Mathf.Clamp01(zoom + delta);

        float distance = Mathf.Lerp(stickMinZoom, stickMaxZoom, zoom);
        stick.localPosition = new Vector3(0f, 0f, distance);

        float angle = Mathf.Lerp(swivelMinZoom, swivelMaxZoom, zoom);
        //swivel.localRotation = Quaternion.Euler(angle, 0f, 0f);
    }

    void AdjustSwivel(float sDelta)
    {
        swivelAngle += sDelta * swivelSpeed * Time.deltaTime;
        if (swivelAngle < swivelMinZoom)
        {
            swivelAngle = swivelMinZoom;
        }
        if (swivelAngle > swivelMaxZoom)
        {
            swivelAngle = swivelMaxZoom;
        }
        swivel.localRotation = Quaternion.Euler(swivelAngle, 0f, 0f);
    }

    void AdjustPosition(float xDelta, float zDelta)
    {
        Vector3 camRight = Camera.main.transform.right;
        Vector3 camForwards = Camera.main.transform.up;

        Vector3 rightInput = xDelta * camRight;
        Vector3 forwardInput = zDelta * camForwards;

        Vector3 direction = (rightInput + forwardInput).normalized;
        direction.y = 0f;
        float damping = Mathf.Max(Mathf.Abs(xDelta), Mathf.Abs(zDelta));
        float distance = Mathf.Lerp(moveSpeedMinZoom, moveSpeedMaxZoom, zoom) * damping * Time.deltaTime;

        Vector3 position = transform.localPosition;
        position += direction * distance;
        transform.localPosition = ClampPosition(position);
    }

    void AdjustRotation(float delta)
    {
        rotationAngle += delta * rotationSpeed * Time.deltaTime;
        if (rotationAngle < 0f)
        {
            rotationAngle += 360f;
        }
        else if (rotationAngle >= 360f)
        {
            rotationAngle -= 360f;
        }
        transform.localRotation = Quaternion.Euler(0f, -rotationAngle, 0f);
    }
    Vector3 ClampPosition(Vector3 position)
    {
        float xMax =
            (grid.cellCountX - 0.5f) *
            (2f * HexMetrics.innerRadius);
        position.x = Mathf.Clamp(position.x, 0f, xMax);

        float zMax =
            (grid.cellCountZ * HexMetrics.chunkSizeZ - 1) *
            (1.5f * HexMetrics.outerRadius);
        position.z = Mathf.Clamp(position.z, 0f, zMax);

        return position;
    }

    public static bool Locked
    {
        set
        {
            instance.enabled = !value;
        }
    }

    public static void ValidatePosition(HexMapCamera instance1)
    {
        instance1.AdjustPosition(0f, 0f);
    }
}