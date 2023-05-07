using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireagr : MonoBehaviour
{
    private Rigidbody2D rb;
    private Rigidbody2D rbFire;
    private bool del = false;
    public Vector3 playerCoordinate;
    public Vector3 fireBallCoordinate;
    public GameObject obj;
    public GameObject placeOfSpawnLeft;
    public GameObject placeOfSpawnRight;
    private bool flipRight = true;

    public float startTimeBtwSpawns;
    private float timeBtwSpawns;

    
    // Start is called before the first frame update


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rbFire = obj.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.Find("Player");
        playerCoordinate = player.transform.position;
        fireBallCoordinate = this.transform.position;
        

        if (Math.Abs(fireBallCoordinate.x - playerCoordinate.x) < 40 &&
            Math.Abs(fireBallCoordinate.y - playerCoordinate.y) < 2)
        {
            if (fireBallCoordinate.x > playerCoordinate.x && !flipRight)
            {
                Flip();

            }
            if (fireBallCoordinate.x < playerCoordinate.x && flipRight)
            {
                Flip();
            }
            
            if (timeBtwSpawns <= 0)
            {
                if (flipRight == false)
                {
                    Instantiate(obj,placeOfSpawnLeft.transform.position,Quaternion.identity);
                }
                else
                {
                    Instantiate(obj,placeOfSpawnRight.transform.position,Quaternion.identity);
                }
                timeBtwSpawns = startTimeBtwSpawns;
            }
            else
            {
                timeBtwSpawns -= Time.deltaTime;
            }
        }
      
    }
    private void Flip()
    {
        flipRight = !flipRight;
        var theScale = obj.transform.localScale;
        theScale.x *= -1;
        obj.transform.localScale = theScale;
    }
    
    private void Move()
    {
        if (flipRight == false)
        {
            rbFire.velocity = new Vector2(-4, rb.velocity.y);
        }
        else
        {
            rbFire.velocity = new Vector2(4, rb.velocity.y);
        }
        
    }
}
