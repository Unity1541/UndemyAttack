using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyChaseState : EnemyBaseState
{
    private readonly int enemyLocomotion = Animator.StringToHash("EnemyMovement");
    private readonly int enemySpeedHash = Animator.StringToHash("Speed");
    private const float fadeDuration = 0.1f;
    private const float animatorDampTime = 0.1f;
    public EnemyChaseState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void OnEnter()
    {
        enemyStateMachine.enemyAnimator.CrossFadeInFixedTime(enemyLocomotion, fadeDuration);

    }
    public override void Tick(float deltaTime)
    {

        if (!isChangeRange())
        {
            Debug.Log("沒有範圍");
            enemyStateMachine.SwitchState(new EnemyIdleState(enemyStateMachine));
            return;
        }
        else if (isAttackRange())
        {
            Debug.Log("攻擊範圍");
            enemyStateMachine.SwitchState(new EnemyAttackState(enemyStateMachine));
            return;
        }

        MoveToPlayer(deltaTime);
        FacePlayer();
        enemyStateMachine.enemyAnimator.SetFloat(enemySpeedHash, 1f, animatorDampTime, deltaTime);
<<<<<<< Updated upstream
        Debug.Log("在追逐狀態");
=======
>>>>>>> Stashed changes
    }

    public override void OnExit()
    {
        enemyStateMachine.navMeshAgent.ResetPath(); //重置NavMeshAgent的路徑不再追蹤玩家
        enemyStateMachine.navMeshAgent.velocity = Vector3.zero; //停止NavMeshAgent的速度
    }


    private void MoveToPlayer(float deltaTime)
    {
        enemyStateMachine.navMeshAgent.SetDestination(enemyStateMachine.player.transform.position);
        MovmentWithGravity(enemyStateMachine.navMeshAgent.desiredVelocity.normalized * enemyStateMachine.chaseMoveSpeed, deltaTime);
        enemyStateMachine.navMeshAgent.velocity = enemyStateMachine.characterController.velocity;
        //characterController的速度
        //velocity是NavMeshAgent的速度
        //這裡的desiredVelocity是指NavMeshAgent的目標速度
        //兩者差異是因為NavMeshAgent會考慮到障礙物和其他因素
        //所以我們需要使用desiredVelocity來計算角色的移動速度
        //這樣就可以讓角色在每幀都能夠移動

    }

    // protected void MovmentWithGravity(Vector3 movement, float deltaTime)//讓人物移動同時考慮重力
    // {
    //     //這個方法是用來處理角色的移動和重力
    //     //這樣就可以在子類別中使用了
    //     enemyStateMachine.characterController.Move((movement + enemyStateMachine.forceReceiver.movementWithForce) * deltaTime);
    //     //這裡的movement是指角色的移動速度，deltaTime是指每幀的時間
    //     //這樣就可以讓角色在每幀都能夠移動
    // }

    private bool isAttackRange()
    {
        float distanceToPlayer = (enemyStateMachine.player.transform.position - enemyStateMachine.transform.position).sqrMagnitude;
        //計算敵人與玩家之間的距離平方
        return distanceToPlayer <= enemyStateMachine.enemyAttackRange* enemyStateMachine.enemyAttackRange;
    }


}