using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    EnemyIdleState enemyIdleState;
<<<<<<< Updated upstream
    [field: SerializeField] public Health enemyHealth { get; private set; }
=======
>>>>>>> Stashed changes
    [field: SerializeField] public Animator enemyAnimator { get; private set; }
    [field: SerializeField] public float playerChaseRange { get; private set; }
    [field: SerializeField] public float enemyAttackRange { get; private set; }
    [field: SerializeField] public CharacterController characterController { get; private set; }
    [field: SerializeField] public ForceReceiver forceReceiver { get; private set; }
    [field: SerializeField] public NavMeshAgent navMeshAgent { get; private set; }
    [field: SerializeField] public WeaponDamage weaponDamage { get; private set; }
    [field: SerializeField] public float chaseMoveSpeed { get; private set; }
    [field: SerializeField] public int attackDamage { get; private set; }
<<<<<<< Updated upstream
    [field: SerializeField] public float knockBack { get; private set; }
=======
>>>>>>> Stashed changes
    public GameObject player { get; set; }

    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyIdleState = new EnemyIdleState(this);
        SwitchState(enemyIdleState);
        navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = false;
        //不使用NavMeshAgent的自動位置更新和旋轉更新
        //這樣可以讓我們手動控制角色的位置和旋轉

    }
<<<<<<< Updated upstream

    private void OnEnable()
    {
        enemyHealth.OnTakeDamage += HandleTakeDamage;
    }

    private void OnDisable()
    {
        enemyHealth.OnTakeDamage -= HandleTakeDamage;
    }


    private void HandleTakeDamage()
    {
        SwitchState(new EnemyImpactState(this));
        Debug.Log($"{gameObject.name} took damage!");
    }

=======
>>>>>>> Stashed changes
    private void OnDrawGizmosSelected()//選到才會在螢幕顯示Gizmos
    {
        // Only draws when object is selected in hierarchy
        Gizmos.color = Color.red;
        // Draw attack range
        Gizmos.DrawWireSphere(this.transform.position, playerChaseRange);
<<<<<<< Updated upstream

=======
   
>>>>>>> Stashed changes
    }
    
}
