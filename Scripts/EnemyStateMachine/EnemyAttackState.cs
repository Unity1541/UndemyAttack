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

<<<<<<< Updated upstream
   public override void OnEnter()
   {
      enemyStateMachine.weaponDamage.SetAttack(enemyStateMachine.attackDamage,enemyStateMachine.knockBack); // 設定攻擊傷害
      enemyStateMachine.enemyAnimator.CrossFadeInFixedTime(enemyAttack, fadeDuration);
     
    }

   public override void Tick(float deltaTime)//在update檢查對方有沒有在範圍內
   {
      AnimatorStateInfo stateInfo = enemyStateMachine.enemyAnimator.GetCurrentAnimatorStateInfo(1);
    
    // 檢查攻擊動畫是否播放完畢
    if (stateInfo.normalizedTime >= .8f && !enemyStateMachine.enemyAnimator.IsInTransition(1))
    {
        enemyStateMachine.SwitchState(new EnemyChaseState(enemyStateMachine));
        return;
    }
    
    Debug.Log("在攻擊狀態");
         
=======
    public override void OnEnter()
    {
       enemyStateMachine.weaponDamage.SetAttack(enemyStateMachine.attackDamage); // 設定攻擊傷害
       enemyStateMachine.enemyAnimator.CrossFadeInFixedTime(enemyAttack,fadeDuration);
    }
    
    public override void Tick(float deltaTime)
    {
>>>>>>> Stashed changes
    }

    public override void OnExit()
    {
    }

}
