using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    //別忘記，PlayerTestState繼承PlayerBaseState
    //PlayerBaseState繼承State
    //所以State的方法必須被實作，被override,否則出現錯誤
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeSpeed");
    //Animator 在執行時，其實是透過編號（int）來存取動畫參數，因此改用把字串變成整數，傳遞比較快
    //這個變數是用來存儲動畫參數的哈希值，這樣可以提高性能，因為使用哈希值比使用字串更快
    //而且打錯字串也沒差，不會影響傳遞
    private readonly int FreeLookBlenderTreeHash = Animator.StringToHash("Movement");
    private const float smoothDamp = 0.13f;
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        //因為有繼承，所以必須呼叫父類別的建構子
        //這樣才能正確初始化父類別的成員變數
        //先call base的建構子的stateMachine參數,再call 自己的參數 stateMachine和 testVariable
        //public PlayerTestState(PlayerStateMachine stateMachine, int testVariable)
        //當你給子類（子物件）建構子參數時，只要你有呼叫 : base(...)，這些參數就會「傳遞」給父類別去初始化
        //如果父物件例如最上層的State不需要stateMachine,也不會出錯
        //stateMachine 不是儲存在這裡，而是傳上去讓父類別處理
        //這裡的PlayerTestState並沒有自己留住 stateMachine，它是將它傳給 PlayerBaseState，並在那裡儲存起來。
    }

    public override void OnEnter()
    {

        stateMachine.IsInteract = stateMachine.animator.GetBool("IsInteract");
        stateMachine.inputReader.targetEvent += OnTargetEnter;
        Debug.Log("Entering PlayerFreeLookState");
        stateMachine.animator.CrossFadeInFixedTime(FreeLookBlenderTreeHash,0.3f);
    }

    public override void Tick(float deltaTime)
    {
        if(stateMachine.inputReader.isAttacking)
        {
            stateMachine.IsInteract = true;
            //如果玩家正在攻擊，就切換到攻擊狀態,這邊的參數0表示，第一招攻擊
            Debug.Log("玩家正在攻擊，切換到攻擊狀態");
            stateMachine.SwitchState(new PlayerAttackState(stateMachine,0));
            return;
        }

        Vector2 input = stateMachine.inputReader.movementInput;
        if (input == Vector2.zero)
        {
            stateMachine.animator.SetFloat(FreeLookSpeedHash, 0f,smoothDamp, deltaTime);
            return;
        }

        // Get camera-relative movement direction
        Vector3 moveDirection = CalculateCameraRelativeMovement(input);

        // Apply gravity to the movement direction
        MovmentWithGravity(moveDirection * stateMachine.freeMoveSpeed, deltaTime);
        // Move character
        //移除未考慮重力stateMachine.characterController.Move(moveDirection * stateMachine.freeMoveSpeed * deltaTime);

        // Update animation
        stateMachine.animator.SetFloat(FreeLookSpeedHash, stateMachine.freeMoveSpeed, smoothDamp, deltaTime);
        HandleRotation(moveDirection, deltaTime);
        // Rotate character to face movement direction
        // if (moveDirection.sqrMagnitude > 0.001f)
        // {
        //     Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        //     stateMachine.transform.rotation = Quaternion.Slerp(
        //         stateMachine.transform.rotation,
        //         targetRotation,
        //        8f * deltaTime
        //     );
        // }
    }

    public override void OnExit()
    {
        stateMachine.inputReader.targetEvent -= OnTargetEnter;
        Debug.Log("Exiting PlayerTestState");

    }
    
    private void OnTargetEnter()
    {
        if (!stateMachine.targeter.SelectTarget())
        {
            Debug.Log("沒有東西，不可以進入鎖定模式");//仍然可以自由選轉
            return;
        }
        // Switch to targeting state when target is selected
        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }

    private void OnTargetExit()
    {
        // Switch back to free look state when target is deselected
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private void HandleRotation(Vector3 moveDirection, float deltaTime)
    {
        if (moveDirection.sqrMagnitude > 0.001f && !stateMachine.IsInteract)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            stateMachine.transform.rotation = Quaternion.Slerp(
                stateMachine.transform.rotation,
                targetRotation,
               8f * deltaTime
            );
        }
    }
    private Vector3 CalculateCameraRelativeMovement(Vector2 input)
    {
        Transform cameraTransform = Camera.main.transform;

        // Calculate camera-relative movement direction
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Remove vertical component to keep movement horizontal
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Create movement direction relative to camera
        return (forward * input.y + right * input.x).normalized;
    }
    
   
}
