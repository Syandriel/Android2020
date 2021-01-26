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
    public float maxY_speed = 10;
    
    public Component groundTrigger;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
    public Component rightWallTrigger;
    public Component leftWallTrigger;
    private float jump;

    private Rigidbody2D body;
    private CapsuleCollider2D collision;
    private Vector2 moveVelocity;
    private bool wasGrounded = false;
    private bool leftWalled = false;
    private bool rightWalled = false;
    private bool wasHurt = false;
    private bool isJumping = false;
    private int oneJump = 0;
    private int framecounter = 0;
    private bool startCounting = false;


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


    private bool notMoving() {
        return (body.velocity.y == 0 && body.velocity.x == 0);
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

        leftWalled = false;
        Collider2D[] leftWallColliders = Physics2D.OverlapCircleAll(leftWallTrigger.transform.position, .1F, whatIsGround);
        for (int i = 0; i < leftWallColliders.Length; i++) {
            if (leftWallColliders[i].gameObject != gameObject) {
                leftWalled = true;
            }
        }
        
        
        rightWalled = false;
        Collider2D[] rightWallColliders = Physics2D.OverlapCircleAll(rightWallTrigger.transform.position, .1F, whatIsGround);
        for (int i = 0; i < rightWallColliders.Length; i++) {
            if (rightWallColliders[i].gameObject != gameObject) {
                rightWalled = true;
            }
        }

        bool jumpung = false;
        
        
        if((jump == 1 && wasGrounded) || (jump == 1 && !wasGrounded && (leftWalled || rightWalled)&& (oneJump<1))) {
            if (!wasGrounded) { //walljump
                if (leftWalled) {
                    body.AddForce(new Vector2(100*jumpForce, 100 * jumpForce));
                }

                if (rightWalled) {
                    body.AddForce(new Vector2(-100*jumpForce, 100 * jumpForce));
                }

                jumpung = true;
                oneJump++;
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

       // bool sign = moveY > 0;

       // moveY = Mathf.Min(Mathf.Abs(moveY), maxY_speed);
        

        if (wasGrounded)
            oneJump = 0;
        
        if (!jumpung)
            body.velocity = new Vector2(moveX * accl*10, moveY);
        //*(sign?1:-1)
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