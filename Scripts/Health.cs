using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int health;

    void Start()
    {
        health = maxHealth; // 初始化生命值為最大生命值
    }

    public void DealDamage(int damage)
    {
        if (health == 0)
        {
            return;
        }

        health = Mathf.Max(health - damage, 0); // 減少生命值，確保不會低於0
        Debug.Log($"{gameObject.name} took {damage} damage, remaining health: {health}");
        
    }
}
