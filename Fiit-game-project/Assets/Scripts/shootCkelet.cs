using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootCkelet : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayers;
    public float attackRate = 0.3f;
    float nextAttackTime = 0f;
    public float wait = 0f;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (ckeletagr.hit == true )
            {
                Attack();
                ckeletagr.hit = false;
                nextAttackTime = Time.time + attackRate;
            }
        }
    }
    void Attack()
    {
        var hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        foreach (var player in hitEnemies) 
        {
            Debug.Log("We hit" + player);
            player.GetComponent<health>().TakeDamage(1.5f);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
