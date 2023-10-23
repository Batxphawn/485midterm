using UnityEngine;
using UnityEngine.SceneManagement;

public class loadSceneThree : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            SceneManager.LoadScene("Scene 3 - Slow Enemies");
        }
    }
}
