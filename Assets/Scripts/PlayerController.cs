using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour  {
    [Range(1, 100)] public float accl = 10;
    public float max_speed = 10;
    [Range(1, 100)] public float jumpForce = 2;
    bool wasGrounded = false;
    
    
    public Component groundTrigger;
    public LayerMask whatIsGround;
    
    private float jump;

    private Rigidbody2D body;
    private Vector2 moveVelocity;
    private bool isJumping = false;
    private bool oneJump = true;

    void Start() {
        body = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update() { //update werte
        //jump = Input.GetAxis("Jump");
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Debug.Log(moveHorizontal);
        //moveVelocity = new Vector2(moveHorizontal, 0);
    }


    private bool notMoving() {
        return (body.velocity.y == 0 && body.velocity.x == 0);
    }
    
    
    void FixedUpdate() {//physics
        //----------------
        wasGrounded = false;
        Collider2D[] collders = Physics2D.OverlapCircleAll(groundTrigger.transform.position, .1F, whatIsGround);
        for (int i = 0; i < collders.Length; i++) {
            if (collders[i].gameObject != gameObject) {
                wasGrounded = true;
            }
        }
        
        if((jump == 1) && (wasGrounded || (!wasGrounded && notMoving()&&oneJump))){
            if (!wasGrounded && notMoving()) { //walljump

                float xcord = body.position.x;
                body.AddForce(new Vector2(100*jumpForce, 50 * jumpForce));
                if (body.position.x.Equals(xcord)) {
                    body.AddForce(new Vector2(-100*jumpForce, 50 * jumpForce));
                }
                oneJump = false;
            } else { 
                body.AddForce(new Vector2(0, 100 * jumpForce));  
            }
        }
        //--------------

        float moveX = moveVelocity.x /10;
        float moveY = body.velocity.y;

        if (moveX > max_speed) {
            moveX = max_speed;
        }

        if (wasGrounded)
            oneJump = true;
        
        
        //if (wasGrounded)
            body.velocity = new Vector2(moveX * accl*10, moveY);
    }

    void InputMovement(Vector2 movement) {
        moveVelocity = movement;
    }

    void Attack() {
        //Debug.Log("Attack in PlayerController");
    }

    void Special() {

        //Debug.Log("Special in PlayerController");
    }

    void Grab() {
        //Debug.Log("Grab in PlayerController");
    }

    void Jump(float value) {
        Debug.Log("Jump: " + value);
        Debug.Log("was Grounded: " + wasGrounded);
        jump = value;
    }

}