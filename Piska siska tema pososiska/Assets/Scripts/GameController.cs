using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    private float levelProgress = 0f;
    private int currentLevel = 1;
    private float countTime = 0.1f;
    public Image levelProgressBar;
    public Text levelText;
    private float counter = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
    
    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= countTime)
        {
            levelProgress += countTime;
            levelProgressBar.fillAmount = levelProgress / 50;
            counter = 0f;
        }

        if (levelProgressBar.fillAmount >= 1)
        {
            levelProgressBar.fillAmount = 0;
            levelProgress = 0;
            currentLevel += 1;
            levelText.text = currentLevel.ToString();
        }
    }
}
