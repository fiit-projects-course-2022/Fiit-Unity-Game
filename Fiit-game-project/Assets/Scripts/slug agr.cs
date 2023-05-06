using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slugagr : MonoBehaviour
{
    public float maxSpeed = 10f;
    private bool flipRight = true;
    private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 0.01f;
    private Animator anim;
    private bool isGrounded = false;
    
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
        var move = Input.GetAxis("Horizontal");
        var player = GameObject.Find("Player");
        playerCoordinate = player.transform.position;
        slugCoordinate = this.transform.position;
        if (isGrounded)
        {
            State = SlugStates.afk;
            if (Math.Abs(slugCoordinate.y - playerCoordinate.y) <= 2)
            {
                State = SlugStates.jump;
            }
        }
        
        else
        {
            State = SlugStates.afk;
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;

    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        State = SlugStates.jump;
        isGrounded = false;
    }
}
public enum SlugStates
{
    afk,
    jump
}
