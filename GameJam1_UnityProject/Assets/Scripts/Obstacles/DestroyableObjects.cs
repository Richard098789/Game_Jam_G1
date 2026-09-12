using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public float destroySpeed = 8f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            float hitSpeed = collision.relativeVelocity.magnitude;

            if (hitSpeed >= destroySpeed)
            {
                Destroy(gameObject);
            }
        }
    }
}
