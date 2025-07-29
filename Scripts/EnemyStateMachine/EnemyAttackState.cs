using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
   private readonly int enemyAttack = Animator.StringToHash("EnemyAttack");
   private const float fadeDuration = 0.1f;


   public EnemyAttackState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void OnEnter()
    {
       enemyStateMachine.weaponDamage.SetAttack(enemyStateMachine.attackDamage); // 設定攻擊傷害
       enemyStateMachine.enemyAnimator.CrossFadeInFixedTime(enemyAttack,fadeDuration);
    }
    
    public override void Tick(float deltaTime)
    {
    }

    public override void OnExit()
    {
    }

}
