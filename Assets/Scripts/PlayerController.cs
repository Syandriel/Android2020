﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour  {
    [Range(1, 100)] public float accl = 10;
    public float max_speed = 10;
    [Range(1, 100)] public float jumpForce = 2;
    
    
    public Component groundTrigger;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
    
    private float jump;

    private Rigidbody2D body;
    private CapsuleCollider2D collision;
    private Vector2 moveVelocity;
    private bool wasGrounded = false;
    private bool wasHurt = false;

    void Start() {
        body = this.gameObject.GetComponent<Rigidbody2D>();
        collision = this.gameObject.GetComponent<CapsuleCollider2D>();
    }

    void Update() { //update werte
        //jump = Input.GetAxis("Jump");
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Debug.Log(moveHorizontal);
        //moveVelocity = new Vector2(moveHorizontal, 0);
    }

    void FixedUpdate() {//physics
        //----------------
        wasGrounded = false;
        Collider2D[] groundColliders = Physics2D.OverlapCircleAll(groundTrigger.transform.position, .1F, whatIsGround);
        for (int i = 0; i < groundColliders.Length; i++) {
            if (groundColliders[i].gameObject != gameObject) {
                wasGrounded = true;
            }
        }
        Collider2D[] enemyColliders = Physics2D.OverlapCapsuleAll(collision.transform.position, collision.size, collision.direction, 0, whatIsEnemy);
        for (int i = 0; i < enemyColliders.Length; i++) {
            if(enemyColliders[i].gameObject != gameObject) {
                wasHurt = true;
                Debug.Log("AAAAAAAHHHHHHHH");
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
        //Debug.Log("Jump: " + value);
        //Debug.Log("was Grounded: " + wasGrounded);
        jump = value;
    }

}