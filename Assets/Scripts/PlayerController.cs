using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour  {
    [Range(1, 100)] public float accl = 10;
    public float max_speed = 10;
    [Range(1, 100)] public float jumpForce = 2;
	
	private float jump;

    private Rigidbody2D body;
    private Vector2 moveVelocity;
    private bool isJumping = false;

    void Start() {
        body = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update() {
		jump = Input.GetAxis("Jump");
		//Debug.Log(Input.GetAxis("Jump"));
		
        float moveHorizontal = Input.GetAxis("Horizontal");
        moveVelocity = new Vector2(moveHorizontal, 0);
    }

    void FixedUpdate() {//physics
	
		
		if(jump==1){
			isJumping=true;
			body.AddForce(new Vector2(0,10*jumpForce));
		}
		//isJumping=false;

        if (body.velocity.magnitude < max_speed) {
            body.AddForce(moveVelocity * accl);
        }
        
    }

}

