using UnityEngine;

public class BouncableObstacles : MonoBehaviour
{
    [SerializeField] private float bounceCoefficient = 0.5f;
    [SerializeField] private float maxBounceSpeed = 15f;
    [SerializeField] private float minBounceSpeed = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            float impactSpeed = rb.linearVelocity.magnitude;
            float bounceSpeed = Mathf.Clamp(impactSpeed * bounceCoefficient, minBounceSpeed, maxBounceSpeed);

            
            rb.AddForce(Vector3.up * bounceSpeed, ForceMode.Impulse);
        }
    }
}
