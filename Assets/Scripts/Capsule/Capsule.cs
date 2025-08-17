using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Capsule : MonoBehaviour
{
    public CapsuleID capsuleID;
    public bool isMerged = true;
    private Animator animator;
    private Image image;
    private Coroutine timerCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        image = GetComponent<Image>();
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
            if (!otherCapsule)
            {
                return;
            }

            if (isMerged || otherCapsule.isMerged)
            {
                return;
            }

            if (capsuleID == otherCapsule.capsuleID)
            {
                if (CapsuleManager.Instance)
                {
                    CapsuleManager.Instance.Merge(this, otherCapsule);
                    isMerged = true;
                }


            }
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            if (GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
            {
                GameManager.Instance.UpdateCapsule(this.gameObject, true);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null)
        {
            if (GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
            {
                GameManager.Instance.UpdateCapsule(this.gameObject, false);
            }

        }
    }


}
