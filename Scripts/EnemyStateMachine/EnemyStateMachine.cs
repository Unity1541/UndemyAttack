using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    EnemyIdleState enemyIdleState;
    [field: SerializeField] public Animator enemyAnimator { get; private set; }
    [field: SerializeField] public float playerChaseRange { get; private set; }
    [field: SerializeField] public CharacterController characterController { get; private set; }
    [field: SerializeField] public ForceReceiver forceReceiver { get; private set; }
    public GameObject player { get; set; }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyIdleState = new EnemyIdleState(this);
        SwitchState(enemyIdleState);
    }
    private void OnDrawGizmosSelected()
    {
        // Only draws when object is selected in hierarchy
        Gizmos.color = Color.red;
        // Draw attack range
        Gizmos.DrawWireSphere(this.transform.position, playerChaseRange);
   
    }
    
}
