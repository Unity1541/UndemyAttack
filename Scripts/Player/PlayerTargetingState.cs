using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetBlenderTreeHash = Animator.StringToHash("TargetBlenderTree");
    private readonly int TargetForwardSpeedHash = Animator.StringToHash("TargetForwardSpeed");
    //這個變數是用來存儲動畫參數的哈希值，這樣可以提高性能，因為使用哈希值比使用字串更快
    private readonly int TargetRightSpeedHash = Animator.StringToHash("TargetRightSpeed");
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        //這個建構函式會在PlayerStateMachine中被呼叫，並且傳遞stateMachine參數
        //這樣就可以在PlayerTargetingState中使用stateMachine了
    }

    public override void OnEnter()
    {
        Debug.Log("Entering 鎖定模式");
        stateMachine.inputReader.cancelEvent += OnCancelTarget;
        stateMachine.animator.Play(TargetBlenderTreeHash);
    }

    public override void Tick(float deltaTime)
    {
        Debug.Log($"鎖定目標: {stateMachine.targeter.currentTargeter?.name ?? "無目標"}");
        //每秒檢查有沒有目標，沒有的話，回復到freeLook狀態
        if (stateMachine.targeter.currentTargeter == null)
        {
            Debug.Log("沒有鎖定目標，切換到自由視角狀態");
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        Vector3 movement = CalculateMovementWithTargeting();
        //計算移動速度，這個方法會考慮到鎖定目標的情況
        MovmentWithGravity(movement * stateMachine.movementWithTargetSpeed, deltaTime);
        //如果有目標，就面對目標
        FaceTarget();
        UpdateTargetAnimation(deltaTime);
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

    //如同PlayerFreeLookState的控制移動方法，這邊也要創造一個方法控制速度
    private Vector3 CalculateMovementWithTargeting()
    {
        Vector3 moveDirection = new Vector3();
        moveDirection += stateMachine.inputReader.movementInput.x * stateMachine.transform.right;//繞目標左右走而已
        moveDirection += stateMachine.inputReader.movementInput.y * stateMachine.transform.forward;//繞目標前後走而已
        return moveDirection;
    }

    private void UpdateTargetAnimation(float deltaTime)
    {
        Vector2 input = stateMachine.inputReader.movementInput;
        float dampTime = 0.05f;

        // Handle forward/backward animation
        float targetForwardSpeed = Mathf.Abs(input.y) > 0.01f ? input.y : 0f;
        stateMachine.animator.SetFloat(TargetForwardSpeedHash, targetForwardSpeed, dampTime, deltaTime);

        // Handle left/right animation
        float targetRightSpeed = Mathf.Abs(input.x) > 0.01f ? input.x : 0f;
        stateMachine.animator.SetFloat(TargetRightSpeedHash, targetRightSpeed, dampTime, deltaTime);
    }
}
