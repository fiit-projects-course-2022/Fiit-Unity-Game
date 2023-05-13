using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ckeletagr : MonoBehaviour
{
    [SerializeField] public AudioSource soundOfMove;
    [SerializeField] public AudioSource soundOfHit;
    private Animator anim;
    private Rigidbody2D rb;
    public Vector3 playerCoordinate;
    public Vector3 skeletCoordinate;
    [SerializeField]private bool flipRight = true;
    private bool flag = true;
    private bool firstEnter = false;
    [SerializeField]public float wait;
    public static bool hit = false;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask playerLayers;
    
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void SetState(CkeletStates value) => anim.SetInteger("state", (int)value);

    void OnAttack()
    {
        var hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        foreach (var player in hitEnemies)
        {
            Debug.Log("We hit" + player);
            player.GetComponent<health>().TakeDamage(2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.Find("Player");
        playerCoordinate = player.transform.position;
        skeletCoordinate = transform.position;
        SetState(CkeletStates.afk);
        
        if (Math.Abs(skeletCoordinate.x - playerCoordinate.x) < 3 &&
            Math.Abs(skeletCoordinate.y - playerCoordinate.y) < 2)
        {
            firstEnter = true;
            flag = false;
        }

        if (firstEnter)
        {
            rb.velocity = Vector2.zero;
            wait += Time.deltaTime;
            rb.velocity = Vector2.zero;
            SetState(CkeletStates.hit);
            if (wait >= 2.09f)
            {
                flag = true;
                hit = true;
                firstEnter = false;
                wait = 0f;
                
            }
            
        }
        
        if (Math.Abs(skeletCoordinate.x - playerCoordinate.x) < 13 &&
            Math.Abs(skeletCoordinate.y - playerCoordinate.y) < 2 && flag)
        {
            if (soundOfMove.isPlaying) return;
            soundOfMove.Play();
            if (skeletCoordinate.x > playerCoordinate.x && !flipRight)
            {
                Flip();
            }

            if (skeletCoordinate.x < playerCoordinate.x && flipRight)
            {
                Flip();
            }
            
            if (flipRight)
            {
                rb.velocity = new Vector2(-2, rb.velocity.y);
            }
            else rb.velocity = new Vector2(2, rb.velocity.y);
        }
        else soundOfMove.Stop();
    }

    private void SoundOfHit()
    {
        soundOfHit.Play();
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
