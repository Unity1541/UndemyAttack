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
        Move(deltaTime);
        if (isChangeRange())
        {
            Debug.Log("進入範圍");
            //enter to the chase
            return;
        }
        enemyStateMachine.enemyAnimator.SetFloat(enemySpeedHash, 0.1f, animatorDampTime, deltaTime);
    }

    public override void OnExit()
    {

    }

    protected void Move(float deltaTime)
    { 
        MovmentWithGravity(Vector3.zero, deltaTime);
    }
    protected void MovmentWithGravity(Vector3 movement, float deltaTime)//讓人物移動同時考慮重力
    {
        //這個方法是用來處理角色的移動和重力
        //這樣就可以在子類別中使用了
        enemyStateMachine.characterController.Move((movement + enemyStateMachine.forceReceiver.movementWithForce) * deltaTime);
        //這裡的movement是指角色的移動速度，deltaTime是指每幀的時間
        //這樣就可以讓角色在每幀都能夠移動
    }

}
