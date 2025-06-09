using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    private float verticalVelocity;
   
    [SerializeField] private CharacterController characterController;
    public Vector3 movmmentY => Vector3.up * verticalVelocity;
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
            Debug.Log("垂直速度重置為: " + verticalVelocity);
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime; //如果角色不在地面上，則垂直速度會受到重力影響
        }
    }
}
