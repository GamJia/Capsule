using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance => instance;
    private static UIManager instance;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private GameObject option;
    private int score;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
    }
    
    void Start()
    {
        score=0;
        UpdateScore();
    }

    public void CalculateScore(int plusScore)
    {
        score+=plusScore;
        if(score<100)
        {
            string scoreFormat = score.ToString("D3");
            scoreText.text = scoreFormat;
        }

        else
        {
            scoreText.text = score.ToString();
        }
    }

    public void UpdateScore()
    {
        if(PlayerPrefs.HasKey("HighScore"))
        {
            if(score>PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore",score);
            }
        }

        else
        {
            PlayerPrefs.SetInt("HighScore",score);
        }

        if(PlayerPrefs.GetInt("HighScore")<100)
        {
            string scoreFormat=PlayerPrefs.GetInt("HighScore").ToString("D3");
            highScoreText.text=scoreFormat;
        }

        else
        {
            highScoreText.text=PlayerPrefs.GetInt("HighScore").ToString();
        }
    }

    public void TurnOnOption()
    {
        option.GetComponent<Animator>().SetTrigger("TurnOn");
        Claw.Instance.isDragAvailable=false;
    }

    public void TurnOffOption()
    {
        option.GetComponent<Animator>().SetTrigger("TurnOff");
        Claw.Instance.isDragAvailable=true;
    }
}
