using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{

    [SerializeField]
    private Health playerHealth;
    private int damage;
    private float knockBack;
    [SerializeField]private Collider myCollider;
    //避免打到敵人一次，會觸發多次collier
    public List<Collider> hitedColliders = new List<Collider>();

    // private void OnEnable()//這只有物件本身被setActive(true)時才會呼叫
    // { 
    //     hitedColliders.Clear(); // 每一次重新啟用時清空已擊中的碰撞器列表
    // }

    private void OnTriggerEnter(Collider other)
    {
        // 1. 先檢查是否已經擊中過
        if (hitedColliders.Contains(other))
        {
            Debug.Log($"Already hit {other.name}, ignoring");
            return;
        }

        // 2. 處理玩家受傷邏輯
        if (other.CompareTag("Player"))
        {   
            Debug.Log("Hit Player!");
            playerHealth.DealDamage(10);
            hitedColliders.Add(other);
            return; // 確保玩家被打到後不會執行後面的敵人邏輯
        }

        // 3. 處理敵人受傷邏輯
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy!");
            hitedColliders.Add(other);
            
            // 處理敵人的傷害
            if (other.TryGetComponent<Health>(out Health health))
            {
                health.DealDamage(damage);
            }

            // 處理敵人的擊退
            if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
            {
                Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
                forceReceiver.AddForce(direction * knockBack);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        hitedColliders.Clear(); // 清空已擊中列表，這樣每次攻擊都會重新計算
    }


    public void SetAttack(int damage,float knockBack)
    { 
        this.damage = damage; // 設定攻擊傷害
        this.knockBack = knockBack; // 設定擊退力
    }
}
