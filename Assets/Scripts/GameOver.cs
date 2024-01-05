using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject currentCapsule;
    [SerializeField] private float deadTime;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        GetComponent<Collider2D>().enabled = true;
        currentCapsule=null;
        deadTime=0;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other)
        {
            if (!currentCapsule)
            {
                currentCapsule = other.gameObject;
            }
            
            deadTime+=Time.deltaTime;

            if(deadTime>0.5f)
            {
                currentCapsule.GetComponent<Image>().color = new Color(1f, 136f / 255f, 136f / 255f);

                if(deadTime>2.5f)
                {
                    GameManager.Instance.GameOver();
                    GetComponent<Collider2D>().enabled=false;
                }
            }

            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(currentCapsule)
        {
            currentCapsule.GetComponent<Image>().color = Color.white;
            currentCapsule=null;
            deadTime=0;
        }
    }

}
