using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : State
{
    protected EnemyStateMachine enemyStateMachine;
    public EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        this.enemyStateMachine = enemyStateMachine;
    }
    public override void OnEnter()
    {

    }

    public override void Tick(float deltaTime)
    {

    }


    public override void OnExit()
    {

    }

    protected bool isChangeRange()
    {
        float toPlayer = (enemyStateMachine.player.transform.position - enemyStateMachine.transform.position).sqrMagnitude;
        return toPlayer <= enemyStateMachine.playerChaseRange*enemyStateMachine.playerChaseRange;
    }
   
}
