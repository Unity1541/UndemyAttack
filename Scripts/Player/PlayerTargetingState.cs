using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        //這個建構函式會在PlayerStateMachine中被呼叫，並且傳遞stateMachine參數
        //這樣就可以在PlayerTargetingState中使用stateMachine了
    }

    public override void OnEnter()
    {
        Debug.Log("Entering 鎖定模式");
        stateMachine.inputReader.cancelEvent += OnCancelTarget;
    }

    public override void Tick(float deltaTime)
    {
        if(stateMachine.inputReader.isAttacking)
        {
            //如果玩家正在攻擊，就切換到攻擊狀態
            Debug.Log("玩家正在攻擊，切換到攻擊狀態");
            stateMachine.SwitchState(new PlayerAttackState(stateMachine,0));
            return;
        }
        Debug.Log($"鎖定目標: {stateMachine.targeter.currentTargeter?.name ?? "無目標"}");
    }

    public override void OnExit()
    {
       stateMachine.inputReader.cancelEvent -= OnCancelTarget; 
    }
    
    private void OnCancelTarget()
    {
        stateMachine.targeter.CancelTarget();
        //當玩家按下取消鎖定按鈕時，觸發這個方法
        Debug.Log("取消鎖定目標");
        //PlayerTargeting是透過StateMachine取得PlayerFreeLookState的實例
        //然後切換到PlayerFreeLookState
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        //切換回自由視角狀態
    }
}
