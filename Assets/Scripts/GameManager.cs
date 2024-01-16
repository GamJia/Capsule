using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => instance;
    private static GameManager instance;
    [SerializeField] private GameObject capsuleGroup;
    [SerializeField] private GameObject settingButton;
    [SerializeField] private GameObject setting;
    [SerializeField] private Collider2D gameoverCollider;
    private Coroutine gameoverCoroutine;

    
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
    }
    public void GameOver()
    {
        gameoverCollider.enabled=false;
        UIManager.Instance.UpdateScore();
        if (gameoverCoroutine == null)
        {
            gameoverCoroutine = StartCoroutine(GameOverAnimation());
        }
    }

    IEnumerator GameOverAnimation()
    {
        settingButton.SetActive(false);
        for(int i=0;i<capsuleGroup.transform.childCount;i++)
        {
            capsuleGroup.transform.GetChild(i).GetComponent<Animator>().SetTrigger("isGameOver");
            yield return new WaitForSeconds(0.06f);
        }

        setting.GetComponent<Animator>().SetTrigger("Game_Over");
        
    }
    
}
