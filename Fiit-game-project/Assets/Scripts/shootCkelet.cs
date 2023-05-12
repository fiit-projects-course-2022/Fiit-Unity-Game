using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootCkelet : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange;
    public LayerMask playerLayers;
    public float attackRate;
    float nextAttackTime = 0f;
    
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
            player.GetComponent<health>().TakeDamage(2f);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
