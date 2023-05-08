using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootFIRE : MonoBehaviour
{
    [SerializeField]private float damage = 3f;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            other.GetComponent<health>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }
}