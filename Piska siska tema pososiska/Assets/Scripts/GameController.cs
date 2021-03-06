using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public static int score = 0;
    private float levelProgress = 0f;
    private int currentLevel = 1;
    private float countTime = 0.1f;
    public Image levelProgressBar;
    public Text levelText;
    private float counter = 0f;

    public GameObject upgradeButton;

    private Image upgradeButtonImage;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name.Contains("Local"))
            upgradeButtonImage = upgradeButton.GetComponent<Image>();
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name.Contains("Local"))
        {
            float scoreFloat = score;
            upgradeButtonImage.fillAmount = scoreFloat / 20f;

            if (score >= 20)
            {
                var button = upgradeButton.GetComponent<Button>();
                button.interactable = true;
            }
        }

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


    public void GoToMenu()
    {
        Destroy(GameObject.FindWithTag("SoundManager").gameObject);
        SceneManager.LoadScene("Menu");
    }
}
