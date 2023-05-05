using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected int lives;
    // Start is called before the first frame update
    public virtual void GetDamage()
    {
        lives--;
        if (lives < 1)
            Die();
    }

    // Update is called once per frame
    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
