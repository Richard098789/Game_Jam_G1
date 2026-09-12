using Unity.VisualScripting;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Destroy(gameObject);
        }
    }
}
