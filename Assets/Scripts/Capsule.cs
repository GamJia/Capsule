using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    public CapsuleData capsuleData;
    public bool isMerged=false;
    private bool isHit=false;


    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!isHit&&other.gameObject.layer.Equals(0))
        {
            Claw.Instance.isDragAvailable=true;
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

    
}
