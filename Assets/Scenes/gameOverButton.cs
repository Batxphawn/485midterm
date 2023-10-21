using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class gameOverButton : MonoBehaviour
{
    public Button returnToMenuButton; // Reference to the Button component
    private bool buttonVisible = false;

    private void Start()
    {
        returnToMenuButton.gameObject.SetActive(false); // Disable the entire button GameObject
    }

    public void ShowButton()
    {
        returnToMenuButton.gameObject.SetActive(true); // Enable the entire button GameObject
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