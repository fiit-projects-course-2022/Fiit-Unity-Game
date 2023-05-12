using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireMove : MonoBehaviour
{
    [SerializeField]private bool flipRight = true;
    private Rigidbody2D rbFire;
    private bool turn;
    public Vector3 playerCoordinate;
    public Vector3 fireCoordinate;
    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.Find("Player");
        playerCoordinate = player.transform.position;
        fireCoordinate = this.transform.position;
        if (playerCoordinate.x < fireCoordinate.x)
        {
            turn = false;
        }
        else
        {
            turn = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (turn == true)
        {
            rbFire.velocity = new Vector2(4, rbFire.velocity.y);
            if (flipRight)
            {
                Flip();
            }
        }
        else
        {
            rbFire.velocity = new Vector2(-4, rbFire.velocity.y);
            if (!flipRight)
            {
                Flip();
            }
        }
        
    }
    private void Awake()
    {
        rbFire = GetComponent<Rigidbody2D>();
    }
    
    private void Flip()
    {
        flipRight = !flipRight;
        var theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
