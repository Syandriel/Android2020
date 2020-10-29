using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(1, 100)] public float accl = 10;
    public float max_speed = 10;
    [Range(1, 100)] public float jumpForce = 2;

    private Rigidbody2D body;
    private Vector2 moveVelocity;
    private bool isJumping;

    void Start()
    {
        body = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        moveVelocity = new Vector2(moveHorizontal, 0);
    }

    void FixedUpdate()
    {
        if (body.velocity.magnitude < max_speed)
        {
            body.velocity = (moveVelocity * accl);
        }
    }

}

