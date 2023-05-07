using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireMove : MonoBehaviour
{
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
        }
        else
        {
            rbFire.velocity = new Vector2(-4, rbFire.velocity.y);
        }
    }
    private void Awake()
    {
        rbFire = GetComponent<Rigidbody2D>();
    }
}
