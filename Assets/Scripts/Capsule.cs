using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    public CapsuleID capsuleID;
    public bool isMerged=false;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var otherCapsule = other.gameObject.GetComponent<Capsule>();

        if (otherCapsule != null)
        {
            if(!otherCapsule)
            {
                return;
            }

            if(isMerged||otherCapsule.isMerged)
            {
                return;
            }

            if (capsuleID == otherCapsule.capsuleID)
            {
                if(CapsuleManager.Instance)
                {
                    CapsuleManager.Instance.Merge(this,otherCapsule);
                    isMerged=true;
                }
                

            }
        }
    }


}
