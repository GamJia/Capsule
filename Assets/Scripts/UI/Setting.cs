using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    private bool isActive = false;
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

    public void UpdateSetting(bool isActive)
    {
        if (this.isActive == isActive)
        {
            return;
        }

        this.isActive = isActive;
        if (animator)
        {
            var trigger = isActive ? "isOpen" : "isClose";
            animator.SetTrigger(trigger);

            isActive = !isActive;
            if (!isActive)
            {
                AudioManager.Instance.PlaySFX(AudioID.Lift);
            }
        }
    }
}
