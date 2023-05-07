using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ckeletagr : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public Vector3 playerCoordinate;
    public Vector3 skeletCoordinate;
    [SerializeField]private bool flipRight = true;

    public static bool hit = false;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    private CkeletStates State
    {
        get { return (CkeletStates)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.Find("Player");
        playerCoordinate = player.transform.position;
        skeletCoordinate = this.transform.position;
        State = CkeletStates.afk;

        if (Math.Abs(skeletCoordinate.x - playerCoordinate.x) < 40 &&
            Math.Abs(skeletCoordinate.y - playerCoordinate.y) < 2)
        {
            if (skeletCoordinate.x > playerCoordinate.x && flipRight)
            {
                Flip();
            }

            if (skeletCoordinate.x < playerCoordinate.x && !flipRight)
            {
                Flip();
            }
            
            if (flipRight)
            {
                rb.velocity = new Vector2(2, rb.velocity.y);
            }
            else rb.velocity = new Vector2(-2, rb.velocity.y);
            
            if (Math.Abs(skeletCoordinate.x - playerCoordinate.x) < 3)
            {
                hit = true;
                rb.velocity = Vector2.zero;
                State = CkeletStates.hit;
            }
        }
        
        
    }
    private void Flip()
    {
        flipRight = !flipRight;
        var theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    
    
}
public enum CkeletStates
{
    afk,
    hit,
}
