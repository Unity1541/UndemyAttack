using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{//platerAttackState繼承PlayerBaseState，但是自己不是abstract因此可以被new形成新的物件
    private Attack attackData;
    private bool alreadyAppliedForce = false;
    private float previousFrameTime = 0f;
    public PlayerAttackState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attackData = stateMachine.attacks[attackIndex];
        Debug.Log($"PlayerAttackState created with attackIndex: {attackIndex}, attackName: {attackData.attackName}");
    }

    //新進入一次 PlayerAttackState，它怎麼知道要跳到 attacks[1]？
    //attacks[0] 播的是 "Attack1"，它的comboIndex = 1
    //按第一次攻擊 → new PlayerAttackState(..., 0)
    // Tick() 裡檢查 combo 條件 → attackData.comboIndex = 1
    //   ↓
    // SwitchState(new PlayerAttackState(..., 1)) → 播 Attack2
    //也就是目前動畫自己身上有一個-->""1""，代表等等要呼叫第二個attack因為陣列是0開始算


    public override void OnEnter()
    {
        stateMachine.IsInteract = true;
        stateMachine.weaponDamage.SetAttack(attackData.damage);
        stateMachine.animator.CrossFadeInFixedTime(attackData.attackName, attackData.transitionDampTime);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        float normalizedTime = GetNormalizedTime();

        if (normalizedTime >= previousFrameTime && normalizedTime < 1)
        {
            if (normalizedTime > attackData.forceTime)
            {
                TryApplyForce();
            }

            if (stateMachine.inputReader.isAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            if (stateMachine.targeter.currentTargeter != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
            //go back the movement
        }
        previousFrameTime = normalizedTime;
        Debug.Log("Attacking...");
    }

    public override void OnExit()
    {
        stateMachine.IsInteract = false;
        Debug.Log("Exiting Attack State");

    }

    private void TryComboAttack(float normalizedTime)
    {
        if (attackData.comboIndex == -1) { return; }
        if (normalizedTime < attackData.comboAttackTime) { return; }
        //這段就是說，當前動畫如果可以再次攻擊的話，取得他的暗示--comboIndex，例如目前自己是第一個攻擊動畫attack[0]
        //他身上的comboIndex=1，帶入後，重新進入PlayerAttackState，此時

        stateMachine.SwitchState(new PlayerAttackState(stateMachine, attackData.comboIndex));
        // public PlayerAttackState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)，會取得int attackIndex=1
        //這樣就可以播放第二個攻擊動畫了，等於是attack[1]，也就是Attack2
        //     attackData = stateMachine.attacks[attackIndex];

    }

    private void TryApplyForce()
    { 
        if (alreadyAppliedForce) { return; }
        stateMachine.forceReceiver.AddForce(stateMachine.transform.forward * attackData.force);    
        alreadyAppliedForce = true;
    }

    private float GetNormalizedTime()
    {
        //參數 1 表示檢查 Animator 的第二層 (Layer 1，因為索引從 0 開始)
        //取得動畫的正規化時間
        //Animator.IsInTransition,這是 Unity 的 Animator 組件提供的方法，用於檢查指定層級是否正在進行動畫轉換
        AnimatorStateInfo currentInfo = stateMachine.animator.GetCurrentAnimatorStateInfo(1);//1表示第二層，此時在AttackLayer
        AnimatorStateInfo nextInfo = stateMachine.animator.GetNextAnimatorStateInfo(1);//1表示第二層，此時在AttackLayer
        //情況 1：正在切換到新的攻擊動畫
        if (stateMachine.animator.IsInTransition(1) && nextInfo.IsTag("attackTag"))
        {
            // IsInTransition 檢查是否正在切換動畫
            // IsTag("attackTag") 確認是攻擊相關的動畫
            //如果正在轉換動畫，則返回下一個動畫的正規化時間
            return nextInfo.normalizedTime;
        }
        //情況 2：當前在播放攻擊動畫
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
        // normalizedTime 是動畫播放的進度值（0-1之間）：
        // 0 = 動畫開始
        // 0.5 = 動畫播放一半
        // 1 = 動畫結束
        //previousFrameTime 儲存上一幀的動畫進度
        // rame 1: normalizedTime = 0.1, previousFrameTime = 0
        // Frame 2: normalizedTime = 0.2, previousFrameTime = 0.1
        // Frame 3: normalizedTime = 0.3, previousFrameTime = 0.2
    }


}
