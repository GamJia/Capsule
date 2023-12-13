using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    public CapsuleData capsuleData;
    private bool isDropped=false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Capsule otherCapsule = other.gameObject.GetComponent<Capsule>();

        if(!isDropped)
        {
            Claw.Instance.Generate();
            isDropped=true;
        }

        if (otherCapsule != null && otherCapsule.capsuleData.CapsuleLevel.Equals(capsuleData.CapsuleLevel))
        {
            Destroy(gameObject);
        }
    }
    
    
}
