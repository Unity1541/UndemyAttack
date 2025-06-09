using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{//platerAttackState繼承PlayerBaseState，但是自己不是abstract因此可以被new形成新的物件
    private Attack attackData;
    private float previousFrameTime = 0f;
    public PlayerAttackState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attackData = stateMachine.attacks[attackIndex];
    }

    public override void OnEnter()
    {
        stateMachine.animator.CrossFadeInFixedTime(attackData.attackName, attackData.transitionDampTime);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        float normalizedTime = GetNormalizedTime();
        if (normalizedTime > previousFrameTime && normalizedTime < 1)
        {
            if (stateMachine.inputReader.isAttacking)
            {
                 TryComboAttack(normalizedTime);
            }     
        }
        else
        {
            //go back the movement
        }
        previousFrameTime = normalizedTime;
        Debug.Log("Attacking...");
    }

    public override void OnExit()
    {
        // 在這裡可以加入退出攻擊狀態時的邏輯
        Debug.Log("Exiting Attack State");

    }

    private void TryComboAttack(float normalizedTime)
    {
        if (attackData.comboIndex == -1) { return; }
        if (normalizedTime < attackData.comboAttackTime) { return; }
        stateMachine.SwitchState(new PlayerAttackState(stateMachine, attackData.comboIndex));
       
    }

    private float GetNormalizedTime()
    {
        // 取得動畫的正規化時間
        AnimatorStateInfo currentInfo = stateMachine.animator.GetCurrentAnimatorStateInfo(1);//1表示第二層，此時在AttackLayer
        AnimatorStateInfo nextInfo = stateMachine.animator.GetNextAnimatorStateInfo(1);//1表示第二層，此時在AttackLayer
        if (stateMachine.animator.IsInTransition(1) && nextInfo.IsTag("attackTag"))
        {
            //如果正在轉換動畫，則返回下一個動畫的正規化時間
            return nextInfo.normalizedTime;
        }
        else if (!stateMachine.animator.IsInTransition(1) && currentInfo.IsTag("attackTag"))
        {
            //如果沒有轉換動畫，則返回當前動畫的正規化時間
            return currentInfo.normalizedTime;
        }
        else
        {
            //如果沒有在攻擊動畫中，則返回0
            return 0f;
        }
    }


}
