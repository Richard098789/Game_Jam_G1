using UnityEngine;

// Move on X axis

public class MovingObstacles : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float distance = 1f;

    private Vector3 startPosition;


    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * distance;
        transform.position = startPosition + new Vector3(offset, 0, 0);
    }
}
