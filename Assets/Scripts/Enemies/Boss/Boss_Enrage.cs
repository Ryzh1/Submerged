using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Enrage : StateMachineBehaviour
{
    Boss_Attacks attacks;
    Boss boss;

    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<Boss>();
        attacks = animator.GetComponent<Boss_Attacks>();
        
        boss.isInvulnerable = true;
        attacks.EnragedChange();
        

        

        animator.SetTrigger("idle");
        


    }

  
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

 
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.isInvulnerable = false;
        animator.SetBool("isEnraged", false);
        
    }


}
