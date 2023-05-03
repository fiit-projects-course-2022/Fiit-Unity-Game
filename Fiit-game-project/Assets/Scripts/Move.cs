using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float maxSpeed = 10f;
    private bool flipRight = true;
    private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 0.01f;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }

    private bool isGrounded = false; // Она уже должна быть создана выше, как в видео

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;

    } //Вызывается когда есть прикосновение  коллайдера объекта с другими коллайдерами



    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }  //Вызывается когда, происходит "выход из коллизии между объектами" (Есть противоположное OnCollisionEnter2D)

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }    

    private void Flip()
    {
        flipRight = !flipRight;
        var theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
