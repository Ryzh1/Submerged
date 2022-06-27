using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleSecondStageBehaviour : StateMachineBehaviour
{
    public float timer;
    public float minTime;
    public float maxTime;
    bool attackChosen;
    int random;
    Vector2 target;
    Transform player;
    Rigidbody2D rb;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackChosen = false;
        timer = Random.Range(minTime, maxTime);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        target = new Vector2(Random.Range(-4, 4), rb.position.y);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (player.position.x - rb.position.x <= 2 && !attackChosen)
        {
            attackChosen = true;
            random = Random.Range(0, 10);
            if (random > 2)
            {
                animator.SetTrigger("clawattack");
            }
            else
            {
                animator.SetTrigger("earthquake");
            }

        }
        else
        {

            
            animator.transform.position = Vector2.MoveTowards(rb.position, target, 1.5f * Time.fixedDeltaTime);



        }


    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("clawattack");
        animator.ResetTrigger("earthquake");
    }

}
