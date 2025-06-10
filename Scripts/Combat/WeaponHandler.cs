using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Collider collider;

    //這個類別用來處理武器的碰撞器，並且可以在場景中使用，這程式碼只能掛在有animator同一層的物件身上，不然無法呼叫方法
    public void EnableCollider()
    {
        if (collider != null)
        {
            collider.enabled = true;
            Debug.Log("Collider enabled.");
        }
        else
        {
            Debug.LogWarning("Collider is not assigned.");
        }
    }

    public void DisableCollider()
    {
       
        collider.enabled = false;
         Debug.Log("Collider disabled.");
    }
}
