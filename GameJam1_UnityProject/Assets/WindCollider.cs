using UnityEngine;

public class WindCollider : MonoBehaviour
{
    [SerializeField] private float windStrength = 10f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Rigidbody rb = other.attachedRigidbody;

            if (rb != null)
            {
                Vector3 worldForce = transform.forward * windStrength;
                rb.AddForce(worldForce);
            }
        }
    }
}
