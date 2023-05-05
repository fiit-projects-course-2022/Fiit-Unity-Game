using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float maxSpeed = 10f;
    private bool flipRight = true;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    [SerializeField] private float jumpForce = 0.01f;
    private Animator anim;
    public bool isAttacking = false;
    public bool isRecharged = true;
    public Transform attackPos;
    public float attackRange;
    public LayerMask enemy;
    // Start is called before the first frame update

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isRecharged = true;
    }

    void Start()
    {

    }

    // Update is called once per frame
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
        if (Input.GetButtonDown("Jump") && isGrounded)
            Jump();
        if (isGrounded && !isAttacking) State = States.afk;
        if (isGrounded && Input.GetButtonDown("Jump"))
            Jump();
        if (move != 0 && isGrounded)
            State = States.run;
        if (Input.GetButtonDown("Fire1"))
            Hit();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;

    } //���������� ����� ���� �������������  ���������� ������� � ������� ������������

    private void OnCollisionExit2D(Collision2D collision)
    {
        State = States.jump;
        isGrounded = false;
    }  //���������� �����, ���������� "����� �� �������� ����� ���������" (���� ��������������� OnCollisionEnter2D)

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Hit()
    {
        if (isGrounded && isRecharged)
        {
            State = States.hit;
            isAttacking = true;
            isRecharged = false;

            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCoolDown());
        }
    }

    private void OnAttack()
    {
        var colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<Entity>().GetDamage();
        }
    }

    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
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