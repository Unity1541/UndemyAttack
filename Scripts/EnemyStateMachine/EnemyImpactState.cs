using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private const float CrossFadeDuration = 0.1f;
    private float duration = 1.0f;
    public EnemyImpactState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
        enemyStateMachine.enemyAnimator.CrossFadeInFixedTime(ImpactHash, CrossFadeDuration);
    }
    public override void OnEnter()
    {

    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;
        if (duration <= 0f)
        {
            enemyStateMachine.SwitchState(new EnemyIdleState(enemyStateMachine));
            return;
        }
    }
    public override void OnExit()
    {

    }
}
