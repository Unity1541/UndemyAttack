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
        stateMachine.weaponDamage.SetAttack(attackData.damage,attackData.knockBack);
        stateMachine.animator.CrossFadeInFixedTime(attackData.attackName, attackData.transitionDampTime);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        float normalizedTime = GetNormalizedTime(stateMachine.animator);

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

    
}
