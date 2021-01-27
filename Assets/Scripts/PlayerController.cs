using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

//Controller for controlling the singleplayer character
public class PlayerController : MonoBehaviour  {
    [Range(1, 100)] public float accl = 10;
    public float max_speed = 10;
    [Range(1, 100)] public float jumpForce = 2;
    public float maxY_speed = 10;

    [Space]
    
    public BoxCollider2D groundTrigger;
    public Component rightWallTrigger;
    public Component leftWallTrigger;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsLevelEnd;
    private float jump;

    private Rigidbody2D body;
    private CapsuleCollider2D collision;
    private Animator animator;
    private Vector2 moveVelocity;
    private bool wasGrounded = false;
    private bool leftWalled = false;
    private bool rightWalled = false;
    private bool wasHurt = false; //not currently used, necesary if more player lives are implemented
    private int oneJump = 0;
    private bool facingRight = true;


    void Start() {
        body = this.gameObject.GetComponent<Rigidbody2D>();
        collision = this.gameObject.GetComponent<CapsuleCollider2D>();
        animator = this.gameObject.GetComponent<Animator>();
    }
    
    void FixedUpdate() {
        //Check if the player is on the ground this frame
        wasGrounded = false;
        Collider2D[] groundColliders = Physics2D.OverlapBoxAll(groundTrigger.transform.position, groundTrigger.size, 0f, whatIsGround);
        for (int i = 0; i < groundColliders.Length; i++) {
            if (groundColliders[i].gameObject != gameObject) {
                wasGrounded = true;
            }
        }

        //Check if the player is colliding with an enemy and show the death screen accordingly
        Collider2D[] enemyColliders = Physics2D.OverlapCapsuleAll(collision.transform.position, collision.size, collision.direction, 0, whatIsEnemy);
        for (int i = 0; i < enemyColliders.Length; i++) {
            if(enemyColliders[i].gameObject != gameObject) {
                wasHurt = true; //If more lives are implemented
                Debug.Log("AAAAAAAAAH");
                SceneManager.LoadScene("DeathScreen");
            }
        }

        //Check if the player is colliding with a wall to his left
        leftWalled = false;
        Collider2D[] leftWallColliders = Physics2D.OverlapCircleAll(leftWallTrigger.transform.position, .1F, whatIsGround);
        for (int i = 0; i < leftWallColliders.Length; i++) {
            if (leftWallColliders[i].gameObject != gameObject) {
                leftWalled = true;
            }
        }

        //Check if the player is colliding with a wall to his right
        rightWalled = false;
        Collider2D[] rightWallColliders = Physics2D.OverlapCircleAll(rightWallTrigger.transform.position, .1F, whatIsGround);
        for (int i = 0; i < rightWallColliders.Length; i++) {
            if (rightWallColliders[i].gameObject != gameObject) {
                rightWalled = true;
            }
        }

        //Check if the player is colliding with a wall to his right
        bool jumping = false;
        if((jump == 1 && wasGrounded) || (jump == 1 && !wasGrounded && (leftWalled || rightWalled)&& (oneJump<1))) {
            if (!wasGrounded) { //walljump
                if (leftWalled) {
                    body.AddForce(new Vector2(100*jumpForce, 100 * jumpForce));
                }

                if (rightWalled) {
                    body.AddForce(new Vector2(-100*jumpForce, 100 * jumpForce));
                }

                jumping = true;
                oneJump++;
            } else { //normal jump
                body.AddForce(new Vector2(0, 100 * jumpForce));
            }
        }

        //Check if the player is colliding with a wall to his right
        float moveX = moveVelocity.x /10;
        float moveY = body.velocity.y;

        if (moveX > 0 && !facingRight)
            SwitchFacing();
        else if (moveX < 0 && facingRight)
            SwitchFacing();

        if (wasGrounded)
            oneJump = 0;
        
        if (!jumping)
            body.velocity = new Vector2(moveX * accl*10, moveY);

        //Check if the player is colliding with a wall to his right
        animator.SetFloat("Speed", Mathf.Abs(body.velocity.x));
        animator.SetBool("IsJumping", !wasGrounded);

        //Detect if player is at the end of the level
        Collider2D[] endLevelColliders = Physics2D.OverlapCapsuleAll(collision.transform.position, collision.size, collision.direction, 0, whatIsLevelEnd);
        for (int i = 0; i < endLevelColliders.Length; i++) {
            if (endLevelColliders[i].gameObject != gameObject) {
                SceneManager.LoadScene("LevelTransition");
            }
        }
    }

    //Recieving Method for player movement, from Control.cs
    void InputMovement(Vector2 movement) {
        moveVelocity = movement;
    }

    //Recieving Method for jumping, from Control.cs
    void Jump(float value) {
        Debug.Log("Jump: " + value);
        Debug.Log("was Grounded: " + wasGrounded);
        jump = value;
    }

    //Switch the playerfacing, eg. from left to right and vice-versa
    void SwitchFacing() {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

    }

}