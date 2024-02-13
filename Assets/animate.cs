using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animate : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool runningPressed = Input.GetKey(KeyCode.LeftShift);

        if (!isWalking && forwardPressed)
        {
            animator.SetBool("isWalking", true);
        }
        if (isWalking && !forwardPressed)
        {
            animator.SetBool("isWalking", false);
        }
        if (!isRunning && runningPressed)
        {
            animator.SetBool("isRunning", true);
        }
        if (isRunning && !runningPressed)
        {
            animator.SetBool("isRunning", false);
        }
    }
}
