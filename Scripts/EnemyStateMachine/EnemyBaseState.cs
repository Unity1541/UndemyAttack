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

    protected void Move(float deltaTime)
    {
        //這個方法是用來處理角色的移動
        //這樣就可以在子類別中使用了
        MovmentWithGravity(Vector3.zero, deltaTime);
    }
    protected bool isChangeRange()
    {
        float toPlayer = (enemyStateMachine.player.transform.position - enemyStateMachine.transform.position).sqrMagnitude;
        return toPlayer <= enemyStateMachine.playerChaseRange * enemyStateMachine.playerChaseRange;
        //解釋上方
        //這段程式碼的目的是判斷敵人是否在追擊範圍內
        //首先計算敵人與玩家之間的距離平方
        //然後將這個平方距離與玩家的追擊範圍平方進行比較
        //如果平方距離小於等於追擊範圍，則表示玩家在追擊範圍內，返回true
        //否則返回false
    }

    protected void MovmentWithGravity(Vector3 movement, float deltaTime)//讓人物移動同時考慮重力
    {
        //這個方法是用來處理角色的移動和重力
        //這樣就可以在子類別中使用了
        enemyStateMachine.characterController.Move((movement + enemyStateMachine.forceReceiver.movementWithForce) * deltaTime);
        //這裡的movement是指角色的移動速度，deltaTime是指每幀的時間
        //這樣就可以讓角色在每幀都能夠移動
    }

    protected void FacePlayer()
    {
        //這個方法是用來讓敵人面對玩家
        Vector3 direction = (enemyStateMachine.player.transform.position - enemyStateMachine.transform.position);
        direction.y = 0f; // Ignore vertical difference
        direction.Normalize();

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            enemyStateMachine.transform.rotation = Quaternion.Slerp(
                enemyStateMachine.transform.rotation,
                lookRotation,
                Time.deltaTime * 4f
            );
        }
    }
   
}
