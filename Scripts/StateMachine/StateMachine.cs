using UnityEngine;
using UnityEngine.EventSystems;

public abstract class StateMachine : MonoBehaviour
{
    private State currentState;
    //  抽象類別（abstract class）可以包含「具體實作的方法（concrete methods）」與「虛擬方法（virtual methods）」，不一定要宣告 abstract 方法。
    // 當你把一個類別標成 abstract，只是代表它不能被直接實例化（new AbstractClass() 會編譯錯誤），
    // //但裡面的方法既可以是「完全實作好、子類別可以直接繼承的具體方法」，
    //也可以標示為 virtual 讓子類別有機會「選擇性地覆寫」。
    // 如果你不希望子類別「必須」覆寫某些行為，就不要把該方法宣告成 abstract；只要用 virtual 加上一個預設實作就好。
    // 若子類別沒有覆蓋（override）該 virtual 方法，就會自動拿到父類別裡的預設實作；若子類別用 override 去重寫，就能改掉該功能的行為。
    // 在遊戲開發中，這種設計方式常見於「狀態機（State Machine）」、或「角色基底（Character Base）」等架構。

    private void Update()
    {

        currentState?.Tick(Time.deltaTime);// Call the Tick method of the current state
    }

    public void SwitchState(State newState)
    {
        // Exit the current state
        currentState?.OnExit();
        
        // Switch to new state
        currentState = newState;
        
        // Enter the new state
        currentState?.OnEnter();
        
        Debug.Log($"Switched to state: {newState.GetType().Name}");
    }

    public State GetCurrentState()
    {
        return currentState;
    }
}