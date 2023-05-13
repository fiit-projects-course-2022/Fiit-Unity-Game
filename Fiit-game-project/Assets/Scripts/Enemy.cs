using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    [SerializeField] public int currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0) 
        {
            Die();
        }
    }

    void Die()
    {
        
        Destroy(gameObject,0.4f);
    }
}
