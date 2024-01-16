using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Capsule : MonoBehaviour
{
    public CapsuleData capsuleData;
    public float touchTime;
    public bool isMerged=false;
    public bool isHit=false;

    private void Start() {
        touchTime=0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!isHit&&other.gameObject.layer.Equals(0))
        {
            UIManager.Instance.CalculateScore(capsuleData.CapsuleLevel+1);
            isHit=true;
        }

        if(capsuleData.CapsuleLevel<10)
        {
            var otherCapsule = other.gameObject.GetComponent<Capsule>();

            if(!otherCapsule)
            {
                return;
            }

            if(isMerged||otherCapsule.isMerged)
            {
                return;
            }


            if (!otherCapsule.capsuleData.CapsuleLevel.Equals(capsuleData.CapsuleLevel))
            {
                return;
            }

            Claw.Instance.Merge(this,otherCapsule);
            isMerged=true;
        }
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        touchTime+=Time.deltaTime;
            
        if(touchTime>0.5f)
        {
            GetComponent<Image>().color = new Color(1f, 136f / 255f, 136f / 255f);

            if(touchTime>2.5f)
            {
                GameManager.Instance.GameOver();
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        GetComponent<Image>().color = Color.white;
        touchTime=0;
    }

    
}
