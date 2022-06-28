using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{

    private bool isOpened;
    private Animator animator;

    private float lastTime;

    void Start()
    {
        animator = GetComponentInParent<Animator>();
        lastTime = Time.time;
        animator.SetBool("isOpened", isOpened);
        isOpened = !isOpened;
    }

    public void InteractionWithDoor()
    {
        if (Time.time - lastTime > 1f)
        {
            animator.SetBool("isOpened", isOpened);
            isOpened = !isOpened;
            lastTime = Time.time;
        }
    }
}
