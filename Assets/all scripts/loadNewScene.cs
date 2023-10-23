using UnityEngine;
using UnityEngine.SceneManagement;

public class loadNewScene : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            SceneManager.LoadScene("Scene 4 - AI Enemies");
        }
    }
}
