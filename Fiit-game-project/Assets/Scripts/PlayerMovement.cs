using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40;

    private float horizontalMove;
    private bool jump;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump") && isGrounded)
            jump = true;
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

    //меняешь коллайдеры - засунь в них любой нулевой physics
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
