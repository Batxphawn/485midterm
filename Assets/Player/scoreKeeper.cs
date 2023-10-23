using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreKeeper : MonoBehaviour
{
    public static scoreKeeper instance;
    public int collectableScore = 0;
    public int totalScore = 0;
    public int enemyCount = -1;
    public int remaining = 4;
    public TextMeshProUGUI colText;
    public TextMeshProUGUI eneText;
    public TextMeshProUGUI remText;
    public TextMeshProUGUI totalText;

    // Update is called once per frame
    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        sceneUpdate();
    }

    public void colScore()
    {
        collectableScore++;
        remaining--;
        totalScore += 5;
    }
    public void enemyDown()
    {
        enemyCount--;
        totalScore += 20;
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
        if (remaining == 0 || totalScore == 100) { 
            return true; 
        }
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
    public void sceneUpdate()
    {
        colText.text = "Hyper cubes: " + collectableScore.ToString();
        if (enemyCount >= 0)
        {
            eneText.text = "Enemies: " + enemyCount.ToString();
        }
        if (remaining >= 0)
            remText.text = "Remaing: " + remaining.ToString();
        if (totalScore >= 0 && totalScore < 100)
            totalText.text = "Score: " + totalScore.ToString();
        if (totalScore < 0)
            totalText.text = "DEFEAT";
        if ((totalScore == 20 && collectableScore == 4 && enemyCount == -1) || totalScore == 100)
        {
            totalText.text = "VICTORY";
        }

    }
}