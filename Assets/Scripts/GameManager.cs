using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => instance;
    private static GameManager instance;

    private Animator animator;
    public List<GameObject> capsuleList = new List<GameObject>();
    private Coroutine timerCoroutine;
    public bool isGameOver = false;

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
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }




    public void UpdateCapsule(GameObject capsule, bool isEnter)
    {

        if (isEnter)
        {
            if (!capsuleList.Contains(capsule))
            {
                capsuleList.Add(capsule);

                if (timerCoroutine == null)
                {
                    timerCoroutine = StartCoroutine(TimerCoroutine());
                }
            }
        }
        else
        {
            if (capsuleList.Contains(capsule))
            {
                capsuleList.Remove(capsule);

                if (capsuleList.Count == 0 && timerCoroutine != null)
                {
                    if (animator != null)
                    {
                        animator.SetTrigger("isReset");
                    }
                    UIManager.Instance.UpdateCountDown(0, false);
                    StopCoroutine(timerCoroutine);
                    timerCoroutine = null;
                }
            }
        }
    }

    private IEnumerator TimerCoroutine()
    {
        // 첫 대기: 5초
        yield return new WaitForSeconds(3f);

        if (animator != null)
        {
            animator.SetTrigger("isWarning");
        }

        // 카운트다운: 5부터 1초씩 감소
        for (int i = 5; i >= 0; i--)
        {
            UIManager.Instance.UpdateCountDown(i, true);
            yield return new WaitForSeconds(1f);
        }

        // 게임 오버
        GameOver();
        timerCoroutine = null;
    }


    private void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;

            ClawManager.Instance.EnableTouchEvents(false);
            UIManager.Instance.UpdateCountDown(0, false);
            CapsuleManager.Instance.GameOver();

            if (animator != null)
            {
                animator.SetTrigger("isGameOver");
            }
        }
    }

    public void Reset()
    {
        isGameOver = false;

        ClawManager.Instance.EnableTouchEvents(true);
        CapsuleManager.Instance.Reset();

        if (animator != null)
        {
            animator.SetTrigger("isReset");
        }
    }

}
