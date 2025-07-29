using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControll : StateMachineBehaviour
{
    public AnimationCurve speedCurve;
    public float attackDuration = 1.0f;

    private float timer;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        animator.speed = 1f; // 確保進場時是正常速度
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        float normalizedTime = Mathf.Clamp01(timer / attackDuration);
        float speed = speedCurve.Evaluate(normalizedTime);
        animator.speed = Mathf.Max(speed, 0.01f); // 確保不為 0 或負值
        //nimator.speed = speed;
        
        Debug.Log("進入動作攻擊變化");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.speed = 1f; // 離開狀態後恢復正常速度
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
