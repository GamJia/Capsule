using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking; 
using System.IO;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance => instance;
    private static UIManager instance;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text finalScoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private GameObject setting;
    [SerializeField] private GameObject AudioManager;
    
    private int score;
    private bool isProcessing = false;
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
            finalScoreText.text="Your Score\n"+scoreFormat;
        }

        else
        {
            scoreText.text = score.ToString();
            finalScoreText.text="Your Score\n"+score.ToString();
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
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.GamJia.Capsule&hl=en-KR");
    }
    public void ShareGame()
    {
        StartCoroutine(TakeScreenshotAndShare());
    }

    private IEnumerator TakeScreenshotAndShare()
    {
        if(setting.activeSelf)
        {
            setting.SetActive(false);
        }

        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false );
        ss.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0 );
        ss.Apply();

        string filePath = Path.Combine( Application.temporaryCachePath, "shared img.png" );
        File.WriteAllBytes( filePath, ss.EncodeToPNG() );

        // To avoid memory leaks
        Destroy( ss );

        new NativeShare().AddFile( filePath )
            .SetSubject( "Play WaterMelon Game - Gold Capsule" ).SetText( "Gacha fun in every capsule pop! Try our colorful game now! 🚀💖" ).SetUrl( "https://play.google.com/store/apps/details?id=com.GamJia.Capsule&hl=en-KR" )
            .SetCallback( ( result, shareTarget ) => Debug.Log( "Share result: " + result + ", selected app: " + shareTarget ) )
            .Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
        setting.SetActive(true);
    }




    public void ExitGame()
    {
        Application.Quit();
    }


}
