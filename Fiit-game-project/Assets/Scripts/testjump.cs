using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testjump : MonoBehaviour
{
    private bool flipRight = true;
    private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 0.1f;
    private Animator anim;
    [SerializeField]private bool isGrounded = false;
    private bool del = false;
    public Vector3 playerCoordinate;
    public GameObject slug;
    [SerializeField]public float wait = 0f;
    public Vector3 slugCoordinate;
    // Start is called before the first frame update
    
    private SlugStates1 State
    {
        get { return (SlugStates1)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        var player = GameObject.Find("Player");
        playerCoordinate = player.transform.position;
        slugCoordinate = this.transform.position;


        if (isGrounded)
        {
            if (Math.Abs(slugCoordinate.x - playerCoordinate.x) < 10 && 
                Math.Abs(slugCoordinate.y - playerCoordinate.y - 2.65) < 3)
            {
                if (slugCoordinate.x > playerCoordinate.x && flipRight)
                {
                    Flip();
                }
                if (slugCoordinate.x < playerCoordinate.x && !flipRight)
                {
                    Flip();
                }
                State = SlugStates1.jump;
                wait += Time.deltaTime;
                if (wait > 2f)
                {
                    Jump();
                    while (!isGrounded)
                    {
                        wait += Time.deltaTime;
                    }
                    wait = 0;
                }
            }
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            rb.velocity = Vector2.zero;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            State = SlugStates1.jump1;
            isGrounded = false;
        }
    }
    
    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        if (flipRight)
        {
            rb.velocity = new Vector2(2, rb.velocity.y);
        }
        else rb.velocity = new Vector2(-2, rb.velocity.y);
    }
    
    private void Flip()
    {
        flipRight = !flipRight;
        var theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
public enum SlugStates1
{
    afk,
    jump,
    jump1
}
