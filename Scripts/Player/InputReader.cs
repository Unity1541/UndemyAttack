using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //引入System命名空間以使用Action委託
using UnityEngine.InputSystem; //引入InputSystem命名空間
public class InputReader : MonoBehaviour, Controls.IPlayerActions
{//可以去看一下Controls裡面有Interface IPlayerActions
 //這個類別是用來讀取玩家的輸入，所以有繼承的必須要實作，類似UnityEvent的On...方法


    public Vector2 movementInput { get; private set; } //用來儲存玩家的移動輸入;
    private Controls controls; //Controls是自動生成的InputAction類別
    public event Action jumpEvent; //定義一個事件，當玩家跳躍時觸發
                                   //只能透過程式碼訂閱/取消訂閱，不會顯示在 Inspector 中，因為它不是 Unity 可以序列化的類型
                                   //public UnityEvent jumpEvent; 
                                   //使用 UnityEvent 來綁定跳躍事件，這樣可以在 Inspector 中設定
                                   // 拖曳任意 public method 來綁定
                                   // 被設計來讓不懂程式的設計師也能使用事件機制
    public event Action DodgeEvent; //定義一個事件，當玩家閃避時觸發
                                    //同樣只能透過程式碼訂閱/取消訂閱，不會顯示在 Inspector 中

    private void Awake()
    {
        //在Awake方法中初始化Controls
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        // 告訴 InputSystem：當發生 Player 的輸入事件時，要呼叫 this 裡面對應的函數，類似下方以前寫的
        //jumpAction.performed += OnJump;
        //moveAction.performed += OnMove;
        controls.Player.Enable(); //啟用Controls，只有當自己創建InputAction才需要啟用

        //這樣就可以開始接收玩家的輸入了
        //使用 UnityEvent 搭配 PlayerInput 元件時，不需要手動呼叫 .Enable()
        //Unity 自動呼叫你綁定在 UnityEvent 上的函數
        // 無需你手動寫 .Enable() 或 SetCallbacks()

    }


    private void OnDestroy()
    {
        //在物件銷毀時，或者切換場景時候，禁用Controls
        controls.Player.Disable();
    }

    #region 玩家移動
    public void OnJump(InputAction.CallbackContext context)//實作IPlayerActions的OnJump
    {

        //當玩家按下跳躍鍵時，這個方法會被呼叫
        if (context.performed)
        {
            Debug.Log("Jump performed - Before event invoke"); // Add this
            jumpEvent?.Invoke();
            Debug.Log("Jump performed - After event invoke"); // Add this
            //在這裡可以加入跳躍的邏輯
        }
    }

    public void OnDodge(InputAction.CallbackContext context)//實作IPlayerActions的OnDodge
    {
        //當玩家按下閃避鍵時，這個方法會被呼叫
        if (context.performed)
        {
            DodgeEvent?.Invoke(); //觸發DodgeEvent事件，如果有訂閱的話
            Debug.Log("Dodge action performed");
            //在這裡可以加入閃避的邏輯
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //當玩家移動時，這個方法會被呼叫
        //if (context.performed)
        //你只在 performed 階段時更新 movementInput
        //當玩家鬆開鍵時，不會觸發 performed，movementInput 仍然保留上一次的值，所以會動個不停
        //應該改成在每次輸入變化時都更新 movementInput
        //這樣就能確保 movementInput 始終反映當前的輸入狀態
        movementInput = context.ReadValue<Vector2>();

    }
    #endregion

    public void OnLook(InputAction.CallbackContext context)
    {
        //cinemachine會幫忙自動帶入，這邊不用再讀取value
    }
}
