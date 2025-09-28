using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyIdleState : EnemyBaseState
{
    private readonly int enemyLocomotion = Animator.StringToHash("EnemyMovement");
    private readonly int enemySpeedHash = Animator.StringToHash("Speed");
    private const float fadeDuration = 0.1f;
    private const float animatorDampTime = 0.1f;
    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void OnEnter()
    {
        enemyStateMachine.enemyAnimator.CrossFadeInFixedTime(enemyLocomotion, fadeDuration);

    }
    public override void Tick(float deltaTime)
    {
        enemyStateMachine.enemyAnimator.SetFloat(enemySpeedHash, 0.2f, animatorDampTime, deltaTime);
        Move(deltaTime);
        FacePlayer();
        if (isChangeRange())
        {
            Debug.Log("進入範圍");
            //enter to the chase
            enemyStateMachine.SwitchState(new EnemyChaseState(enemyStateMachine));
            return;
        }
         Debug.Log("在閒置狀態");

    }

    public override void OnExit()
    {

    }

   
}
