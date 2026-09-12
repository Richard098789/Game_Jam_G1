using UnityEngine;

public class HoleCollider : MonoBehaviour
{
    [Header("Hole Settings")]
    public float radius = 1f;
    public float depth = 2f;
    public int segments = 32;
    public float wallThickness = 0.1f;

    void Start()
    {
        CreateHole();
    }

    void CreateHole()
    {
        float segmentWidth = (2f * Mathf.PI * radius) / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = (360f / segments) * i;
            float radians = angle * Mathf.Deg2Rad;

            GameObject wall = new GameObject("Wall " + i);
            wall.transform.SetParent(transform);

            wall.transform.localPosition = new Vector3(
                Mathf.Cos(radians) * radius,
                -depth / 2f,
                Mathf.Sin(radians) * radius
            );

            wall.transform.localRotation =
                Quaternion.Euler(0, -angle + 90f, 0);

            BoxCollider collider = wall.AddComponent<BoxCollider>();

            collider.size = new Vector3(
                segmentWidth,
                depth,
                wallThickness
            );
        }

        // Bottom
        GameObject bottom = new GameObject("Bottom");
        bottom.transform.SetParent(transform);
        bottom.transform.localPosition = new Vector3(0, -depth, 0);

        BoxCollider bottomCollider = bottom.AddComponent<BoxCollider>();
        bottomCollider.size = new Vector3(
            radius * 2f,
            wallThickness,
            radius * 2f
        );
    }
}