using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    public CapsuleData capsuleData;
    public bool isMerged=false;
    private bool isHit=false;

    void Start() 
    {
        UIManager.Instance.CalculateScore(capsuleData.CapsuleLevel+1);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!isHit)
        {
            Claw.Instance.isDragAvailable=true;
            isHit=true;
        }

        if(capsuleData.CapsuleLevel<10)
        {
            var otherCapsule = other.gameObject.GetComponent<Capsule>();

            if(!otherCapsule)
            {
                return;
            }

            if(isMerged)
            {
                return;
            }

            if(otherCapsule.isMerged)
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
