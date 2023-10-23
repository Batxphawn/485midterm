using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class gameOverButton : MonoBehaviour
{
    public Button returnToMenuButton; 
    private bool buttonVisible = false;

    private void Start()
    {
        returnToMenuButton.gameObject.SetActive(false); 
    }

    public void ShowButton()
    {
        returnToMenuButton.gameObject.SetActive(true); 
        buttonVisible = true;
    }

    public void ReturnToMenu()
    {
        if (buttonVisible)
        {
            SceneManager.LoadScene("Scene 1 - Menu"); 
        }
    }
}