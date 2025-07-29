using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    private Vector3 impact;
    private Vector3 dampVelocity;
    private float verticalVelocity;

    [SerializeField] private CharacterController characterController;
    [SerializeField] private float dragSpeed = 0.2f;
    public Vector3 movementWithForce => impact + Vector3.up * verticalVelocity;
    //這個屬性用來獲取垂直方向的移動速度，這樣可以在其他地方使用
    void Update()
    {
        if (verticalVelocity < 0 && characterController.isGrounded)
        {
            verticalVelocity = 0.01f;
            //如果角色在地面上，則垂直速度為 verticalVelocity = 0
            //如果這樣直接等於0，有可能角色走到一些比較崎嶇的地方，會判斷沒有isGround，但事實上只是路面不平坦而已，
            //所以這邊要用一個小的數值來代替0，這樣就可以讓角色在崎嶇的地面上也能夠正常行走
            // 這樣就可以避免角色在崎嶇的地面上播放fall動畫起來
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime; //如果角色不在地面上，則垂直速度會受到重力影響
        }


        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampVelocity, dragSpeed);

    }

    public void AddForce(Vector3 force)
    {
        //這個方法用來添加一個力到角色身上
        //這樣就可以在其他地方使用
        impact += force;
        // Debug.Log("添加的力: " + force + "，當前impact: " + impact);
    }
    

    //在許多現代遊戲中，會結合兩者的優點：
// 使用 Root Motion 來處理角色的 Impact Forward，確保移動與動畫同步，視覺效果更自然。
// 使用 程式碼 來處理 Knockback 和其他動態效果，例如根據攻擊力度或敵人屬性動態計算擊退距離。
// 透過 Animator 的 **OnAnimatorMove` 方法動態調整 Root Motion 的影響：
}
