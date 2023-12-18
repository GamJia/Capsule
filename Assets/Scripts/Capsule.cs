using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    public CapsuleData capsuleData;
    public bool isMerged=false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(capsuleData.CapsuleLevel<10)
        {
            var otherCapsule = other.gameObject.GetComponent<Capsule>();

            if(!otherCapsule)
            {
                return;
            }

            if(otherCapsule.isMerged)
            {
                return;
            }

            if (!otherCapsule.capsuleData.CapsuleLevel.Equals(capsuleData.CapsuleLevel)&&!isMerged)
            {
                return;
            }

            Claw.Instance.Merge(this,otherCapsule);
            isMerged=true;
        }

        
    }

    
}
