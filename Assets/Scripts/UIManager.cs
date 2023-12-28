using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking; 
public class UIManager : MonoBehaviour
{
    public static UIManager Instance => instance;
    private static UIManager instance;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private GameObject setting;
    [SerializeField] private GameObject AudioManager;
    
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
    public void ChangeOption(Toggle toggle)
    {
        if(toggle.isOn)
        {
            setting.GetComponent<Animator>().SetTrigger("Option_On");
        }

        else
        {
            setting.GetComponent<Animator>().SetTrigger("Option_Off");
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void RateGame()
    {
        Application.OpenURL("https://gamjia.tistory.com/");
    }
    public void ShareGame()
    {
        StartCoroutine(ShareCoroutine());
    }
    private IEnumerator ShareCoroutine()
    {
        string encodedShareText = UnityWebRequest.EscapeURL("Gacha fun in every capsule pop! Try our colorful game now! 🚀💖");

        string shareURL = "https://gamjia.tistory.com/" + encodedShareText;

        Application.OpenURL(shareURL);

        yield return null;
    }
    public void ExitGame()
    {
        Application.Quit();
    }


}
