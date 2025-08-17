using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance => instance;
    private static UIManager instance;
    private int score = 0;
    [SerializeField] Button settingButton;
    [SerializeField] Text capsuleText;
    [SerializeField] Text scoreText;
    [SerializeField] Text countDownText;
    [SerializeField] Text[] highScoreTexts;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        var highScore = PlayerPrefs.GetInt("HighScore", 0); // 기본값 0
        foreach (Text higtScoreText in highScoreTexts)
        {
            higtScoreText.text = highScore.ToString("D3");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateCapsuleText(int capsuleID)
    {
        if (GameManager.Instance.isGameOver)
        {
            capsuleText.text = "GameOver:(";
            return;
        }

        switch (capsuleID)
        {
            case 0:
                capsuleText.text = "Red";
                break;
            case 1:
                capsuleText.text = "Orange";
                break;
            case 2:
                capsuleText.text = "Yellow";
                break;
            case 3:
                capsuleText.text = "Green";
                break;
            case 4:
                capsuleText.text = "Blue";
                break;
            default:
                capsuleText.text = "Unknown";
                break;
        }

    }

    public void UpdateScore(int addScore)
    {
        score += addScore;
        scoreText.text = score.ToString("D3");
    }

    public void UpdateCountDown(int timer = -1, bool isGameOver = false)
    {
        if (timer != -1)
        {
            countDownText.text = timer.ToString();
        }

        countDownText.gameObject.SetActive(timer != -1);
        settingButton.enabled = timer == -1;

        if (isGameOver)
        {
            if (score > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", score);
                PlayerPrefs.Save();
            }

            var highScore = PlayerPrefs.GetInt("HighScore", 0); // 기본값 0
            foreach (Text higtScoreText in highScoreTexts)
            {
                higtScoreText.text = highScore.ToString("D3");
            }
            capsuleText.text = "GameOver:(";
            score = 0;
            scoreText.text = "0";
        }

    }

    public void ExitGame()
    {
        Application.Quit();
    }



}
