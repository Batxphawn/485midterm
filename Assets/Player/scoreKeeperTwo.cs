using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreKeeperTwo : MonoBehaviour
{
    public static scoreKeeperTwo instance;
    public int collectableScore = 0;
    public int totalScore = 0;
    public int remaining = 4;
    public TextMeshProUGUI colText;
    public TextMeshProUGUI remText;
    public TextMeshProUGUI totalText;

    // Update is called once per frame
    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        sceneTwoUpdate();
    }

    public void colScore()
    {
        collectableScore++;
        remaining--;
        totalScore += 5;
    }
    public bool defeat()
    {
        if (totalScore < -1)
        {
            return true;
        }
        return false;
    }
    public void hit()
    {
        totalScore -= 100;
    }

    public bool colSuccess()
    {
        if (remaining == 0) { return true; }
        return false;
    }
    public bool firstCol()
    {
        if (collectableScore > 0) { return true; }
        else
        {
            return false;
        }
    }
    public void sceneTwoUpdate()
    {
        colText.text = "Hyper cubes: " + collectableScore.ToString();
        remText.text = "Remaing: " + remaining.ToString();
        if (totalScore >= 0 && totalScore < 20)
            totalText.text = "Score: " + totalScore.ToString();
        if (totalScore == 20)
            totalText.text = "VICTORY";

    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "next"))
        {
            onGUI();
        }
    }
    void onGUI()
    {
        if (GUI.Button(new Rect(600, 400, 100, 50), "Next Level"))
        {
            Application.LoadLevel(3);
        }
    }
}
