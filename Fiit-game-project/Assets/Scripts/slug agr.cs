using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slugagr : MonoBehaviour
{
    private bool flipRight = true;
    private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 0.01f;
    private Animator anim;
    [SerializeField]private bool isGrounded = false;
    private bool del = false;
    public Vector3 playerCoordinate;
    public Vector3 slugCoordinate;
    // Start is called before the first frame update
    
    private SlugStates State
    {
        get { return (SlugStates)anim.GetInteger("state"); }
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
            State = SlugStates.afk;
            while (Math.Abs(slugCoordinate.x - playerCoordinate.x) < 10 && 
                Math.Abs(slugCoordinate.y - playerCoordinate.y - 2.65) < 3)
            {

                //StartCoroutine(delay());
                //Invoke("Delay",2f);
                //if (del == true) Jump();
                Jump();
                State = SlugStates.jump;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void Delay()
    {
        del = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            
            isGrounded = false;
        }
    }
    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    IEnumerator delay()
    {
        State = SlugStates.jump;
        yield return new WaitForSeconds(2);
    }
}
public enum SlugStates
{
    afk,
    jump
}
