using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour  {
    [Range(1, 100)] public float accl = 10;
    public float max_speed = 10;
    [Range(1, 100)] public float jumpForce = 2;
    bool wasGrounded = false;
    
    
    public Component GroundTrigger;
    public LayerMask whatIsGround;
    
	private float jump;

    private Rigidbody2D body;
    private Vector2 moveVelocity;
    private bool isJumping = false;

    void Start() {
        body = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update() { //update werte
		jump = Input.GetAxis("Jump");
		float moveHorizontal = Input.GetAxis("Horizontal");
        moveVelocity = new Vector2(moveHorizontal, 0);
    }

    void FixedUpdate() {//physics
        //----------------
        wasGrounded = false;
        Collider2D[] collders = Physics2D.OverlapCircleAll(GroundTrigger.transform.position, .1F, whatIsGround);
        for (int i = 0; i < collders.Length; i++) {
            if (collders[i].gameObject != gameObject) {
                wasGrounded = true;
            }
        }
        
        if((jump == 1) && wasGrounded){
            body.AddForce(new Vector2(0, 100 * jumpForce));
        }
        //--------------

        float moveX = moveVelocity.x /10;
        float moveY = body.velocity.y;

        if (moveX > max_speed) {
            moveX = max_speed;
        }

        if (wasGrounded)
            body.velocity = new Vector2(moveX * accl*10, moveY);

    }

}