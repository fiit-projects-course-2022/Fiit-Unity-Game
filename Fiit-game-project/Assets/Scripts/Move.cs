using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Move : MonoBehaviour
{
    public float maxSpeed = 10f;
    private bool flipRight = true;
    private Rigidbody2D rb;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private float jumpForce = 0.01f;
    private Animator anim;
    public static bool isAttacking = false;
    public bool isRecharged = true;
    public static Move Instance { get; set; }

    private void SetState(States value) => anim.SetInteger("state", (int)value);
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isRecharged = true;
        Instance = this;
    }

    void Update()
    {
        var move = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        if (move > 0 && !flipRight)
        {
            Flip();
        }
        else if (move < 0 && flipRight)
        {
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isAttacking)
            Jump();
        if (isGrounded && !isAttacking) SetState(States.afk);
        if (isGrounded && move != 0 && !isAttacking)
            SetState(States.run);
        if (Input.GetButtonDown("Fire1"))
            if (isGrounded && !isAttacking && move == 0)
                Hit();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        SetState(States.jump);
        isGrounded = false;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Hit()
    {
        if (isGrounded && isRecharged)
        {
            SetState(States.hit);
            isAttacking = true;
            isRecharged = false;

            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCoolDown());
        }
    }

    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.6f);
        isRecharged = true;
    }

    private void Flip()
    {
        flipRight = !flipRight;
        var theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

public enum States
{
    afk,
    jump,
    run,
    hit
}