using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{

<<<<<<< Updated upstream
    [SerializeField]
    private Health playerHealth;
    private int damage;
    private float knockBack;
    [SerializeField]private Collider myCollider;
=======
    private int damage;
>>>>>>> Stashed changes
    //避免打到敵人一次，會觸發多次collier
    public List<Collider> hitedColliders = new List<Collider>();

    // private void OnEnable()//這只有物件本身被setActive(true)時才會呼叫
    // { 
    //     hitedColliders.Clear(); // 每一次重新啟用時清空已擊中的碰撞器列表
    // }

    private void OnTriggerEnter(Collider other)
    {
<<<<<<< Updated upstream
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
=======
        if (other.CompareTag("Player")) { return; } // 忽略與自身碰撞器的碰撞

        //if (hitedColliders.Contains(other)) { return; } // 如果已經擊中過這個碰撞器，則忽略

        if (other.CompareTag("Enemy"))
            hitedColliders.Add(other); // 將碰撞器添加到已擊中列表中
        // 檢查碰撞的物件是否有 Health 組件

        if (other.TryGetComponent<Health>(out Health health))
        {
            // 如果有，則對其造成傷害
            health.DealDamage(damage); // 假設每次攻擊造成10點傷害
            Debug.Log($"Damaged {other.name}, remaining health: {health}");

>>>>>>> Stashed changes
        }
    }

    private void OnTriggerExit(Collider other)
    {

        hitedColliders.Clear(); // 清空已擊中列表，這樣每次攻擊都會重新計算
    }


<<<<<<< Updated upstream
    public void SetAttack(int damage,float knockBack)
    { 
        this.damage = damage; // 設定攻擊傷害
        this.knockBack = knockBack; // 設定擊退力
=======
    public void SetAttack(int damage)
    { 
        this.damage = damage; // 設定攻擊傷害
        
>>>>>>> Stashed changes
    }
}
