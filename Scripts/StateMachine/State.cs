using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class State
{  // With abstract:you have to implement the methods in derived classes, otherwise it will not compile.
   //也就是說，如果你有一個抽象類別，裡面有抽象方法，那麼任何繼承這個抽象類別的子類別都必須實現這些抽象方法。
   // Without abstract: you can have a base implementation in the base class, and derived classes can choose to override it or not.
   //也就是說如果沒有抽象類別，你可以在基類中有一個基本實現，而派生類可以選擇覆蓋它或不覆蓋它。
   //所以只是強迫問題，避免忘記繼承後，實現某些重要方法

    // This prevents accidentally forgetting to implement critical state behaviors
    // Without abstract, you might create a new state and forget to implement 
    public abstract void OnEnter();

    public abstract void Tick(float deltaTime);
    // why us deltaTime?
    // deltaTime is used to ensure that the state behaves consistently regardless of frame rate.


    public abstract void OnExit();


    protected float GetNormalizedTime(Animator animator)//放在這邊讓玩家或敵人都可以用
    {
        //參數 1 表示檢查 Animator 的第二層 (Layer 1，因為索引從 0 開始)
        //取得動畫的正規化時間
        //Animator.IsInTransition,這是 Unity 的 Animator 組件提供的方法，用於檢查指定層級是否正在進行動畫轉換
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(1);//1表示第二層，此時在AttackLayer
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(1);//1表示第二層，此時在AttackLayer
        //情況 1：正在切換到新的攻擊動畫
        if (animator.IsInTransition(1) && nextInfo.IsTag("attackTag"))
        {
            // IsInTransition 檢查是否正在切換動畫
            // IsTag("attackTag") 確認是攻擊相關的動畫
            //如果正在轉換動畫，則返回下一個動畫的正規化時間
            return nextInfo.normalizedTime;
        }
        //情況 2：當前在播放攻擊動畫
        else if (!animator.IsInTransition(1) && currentInfo.IsTag("attackTag"))
        {
            //如果沒有轉換動畫，則返回當前動畫的正規化時間
            return currentInfo.normalizedTime;
        }
        else
        {
            //如果沒有在攻擊動畫中，則返回0
            return 0f;
        }
        // normalizedTime 是動畫播放的進度值（0-1之間）：
        // 0 = 動畫開始
        // 0.5 = 動畫播放一半
        // 1 = 動畫結束
        //previousFrameTime 儲存上一幀的動畫進度
        // rame 1: normalizedTime = 0.1, previousFrameTime = 0
        // Frame 2: normalizedTime = 0.2, previousFrameTime = 0.1
        // Frame 3: normalizedTime = 0.3, previousFrameTime = 0.2
    }
   
}
