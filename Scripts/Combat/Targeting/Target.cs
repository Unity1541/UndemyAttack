using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Target : MonoBehaviour
{
    //     //這邊要考慮自已如果被摧毀的話，就無法被移除當前的list，因此也要同時自身被摧毀的時候，就要從list消失
    // <Target>: 在這裡，Target 是一個類型（例如一個你自訂的類別名稱）。這表示，當 targetHitEvent 被觸發時，必須提供一個 Target 型別的物件作為參數傳遞給所有訂閱者。
    // 簡單來說，targetHitEvent 不僅是一個通知，還附帶了重要情報。 當事件被觸發時，它會告訴所有訂閱者：「事件發生了，而且這是與事件相關的 Target 物件。」
    // 比喻： 就像快遞送貨通知。快遞員（觸發者）不僅會告訴你「有你的包裹」（觸發事件），還會把包裹本人（Target 物件）交給你（傳遞參數）。
    #region 目標屬性比喻
    // 一個代表目標的類別
    // public class Target
    // {
    //     public int health;
    //     public string targetName;
    // }

    // 在子彈 (Bullet) 類別中
    // public event Action<Target> targetHitEvent;

    // void OnCollisionEnter(Collision collision)
    // {
    //     Target hitTarget = collision.gameObject.GetComponent<Target>();
    //     if (hitTarget != null)
    //     {
    //         // 觸發事件，並將擊中的目標物件傳遞出去
    //         targetHitEvent?.Invoke(hitTarget);
    //     }
    // }
    #endregion
    public event Action <Target> OnDestroyed;
    void OnDestroy()//只是預防自己被摧毀時候調用開方法，給Targeter使用，不見得會用到
    {
        //主要是為了預防性地處理 Target 物件被銷毀（例如敵人被擊敗、場景切換等）的情況，確保通知 Targeter（或其他訂閱者）來執行清理邏輯，例如從 targets 列表中移除該目標或更新相機追蹤狀態。
        //這種設計是一種防範措施，確保系統在動態場景中保持一致性，但並不一定在每次遊戲運行中都會觸發（例如，如果目標從未被銷毀，這個邏輯就不會執行）。
        OnDestroyed?.Invoke(this);
        //這樣就可以在其他地方訂閱這個事件，當目標被摧毀時，執行相應的邏輯
        //例如：從Targeter的targets列表中移除這個目標
    }


}
