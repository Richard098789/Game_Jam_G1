using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleTrigger : MonoBehaviour
{
    public string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered hole: " + other.gameObject.name);

        if (other.CompareTag("Ball"))
        {
            Debug.Log("BALL IN HOLE! Loading: " + sceneName);
            SceneManager.LoadScene(sceneName);
        }
    }
}