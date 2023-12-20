using UnityEngine;

public static class HexMetrics
{
    public const float outerRadius = 10f;

    public const float innerRadius = outerRadius * 0.866025404f;

    public const int chunkSizeX = 4, chunkSizeZ = 3;

    public static Material[] materials;

    public static Vector3[] corners = {
        new Vector3(0f, 0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(0f, 0f, outerRadius)
    };

    public static Vector3[] sideMidPoints =
    {
        corners[0] - (0.5f * corners[1]),
        corners[1] - (0.5f * corners[2]),
        corners[2] - (0.5f * corners[3]),
        corners[3] - (0.5f * corners[4]),
        corners[4] - (0.5f * corners[5]),
        corners[5] - (0.5f * corners[0]),
        corners[0] - (0.5f * corners[1])
    };


}