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
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float attackRate = 0.565f;
    float nextAttackTime = 0f;

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
                if (Time.time >= nextAttackTime)
                {
                    Hit();
                    Attack();
                    nextAttackTime = Time.time + attackRate;
                }
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

    void Attack()
    {
        var hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (var enemy in hitEnemies)
        {
            Debug.Log("We hit" + enemy);
            enemy.GetComponent<Enemy>().TakeDamage(1);
            return;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.465f);
        isAttacking = false;
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(attackRate);
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