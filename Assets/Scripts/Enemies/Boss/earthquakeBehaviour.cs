﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earthquakeBehaviour : StateMachineBehaviour
{
    Boss_Attacks attacks;
    public float timer;
    public float minTime;
    public float maxTime;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attacks = animator.GetComponent<Boss_Attacks>();
        attacks.Earthquake();
        timer = Random.Range(minTime, maxTime);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("idle");
        }
        else
        {
            timer -= Time.deltaTime;
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("idle");
    }

}
