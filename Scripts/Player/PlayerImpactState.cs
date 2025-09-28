using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private const float CrossFadeDuration = 0.3f;
    private float duration = 1.0f;
   
    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
       
    }
    

    public override void OnEnter()
    {
        stateMachine.animator.CrossFadeInFixedTime(ImpactHash, CrossFadeDuration);
        Debug.Log("Entering Impact State");
        
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        duration -= deltaTime;
        
        Debug.Log($"Impact Duration: {duration}");
        
        if (duration <= 0f)
        {
            Debug.Log("Impact State Complete");
            ReturnLocomotion();
        }
    }

    public override void OnExit()
    {

    }

    private void ReturnLocomotion()
    {
        if (stateMachine.targeter.currentTargeter != null)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
        else
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }
}
