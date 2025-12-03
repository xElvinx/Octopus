using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentDirectionCalculator : StateMachineBehaviour
{
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("WalkingUp", false);
        animator.SetBool("WalkingDown", false);
        animator.SetBool("WalkingHorizontal", false);
    }

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float xSpeed = animator.GetFloat("xSpeed");
        float zSpeed = animator.GetFloat("zSpeed");

        
        if (Mathf.Abs(xSpeed) < Mathf.Abs(zSpeed))
        {
            if (zSpeed > 0.0f)
            {
                animator.SetBool("WalkingUp", true);
                animator.SetBool("WalkingDown", false);
                animator.SetBool("WalkingHorizontal", false);
            }
            else
            {
                animator.SetBool("WalkingDown", true);
                animator.SetBool("WalkingUp", false);
                animator.SetBool("WalkingHorizontal", false);
            }
        }
        else if (Mathf.Abs(xSpeed) > Mathf.Abs(zSpeed))
        {
            animator.SetBool("WalkingHorizontal", true);
            animator.SetBool("WalkingUp", false);
            animator.SetBool("WalkingDown", false);
        }
        else
        {
            animator.SetBool("WalkingHorizontal", false);
            animator.SetBool("WalkingUp", false);
            animator.SetBool("WalkingDown", false);
        }
    }
}
