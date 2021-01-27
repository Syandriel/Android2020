using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour {

    public Transform player;
    public PlayerController playerController;

    public Canvas control;
    public UpDownButton jump;
    public UpDownButton left;
    public UpDownButton right;

    public float clampDistance;
    [Range(0.1f, 1)]
    public float movementFactor = 0.198f;

    void Start() {

        //Smartphone control
#if ((UNITY_ANDROID || UNITY_IOS)) 
        //Add button events for player control
        jump.onButtonDown.AddListener(StartJump);
        jump.onButtonUp.AddListener(StopJump);

        left.onButtonDown.AddListener(MoveLeft);
        left.onButtonUp.AddListener(StopMoving);

        right.onButtonDown.AddListener(MoveRight);
        right.onButtonUp.AddListener(StopMoving);
#else //PC control
        //disable control ui when playing on PC
        jump.gameObject.SetActive(false);
        left.gameObject.SetActive(false);
        right.gameObject.SetActive(false);
#endif

    }

    // Handle Movement control
    void FixedUpdate() {
#if (!(UNITY_ANDROID || UNITY_IOS)) //PC control
        MoveCharacter(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

        if(Input.GetAxis("Jump") > 0) {
            StartJump();
        } else {
            StopJump();
        }

#endif
    }

    //sends movement information to the corresponding player controller
    void MoveCharacter(Vector2 direction) {
        Debug.Log(direction);
        playerController.SendMessage("InputMovement", Vector2.ClampMagnitude(direction, movementFactor));
    }
    
    //button handler for moving left
    void MoveLeft() {
        MoveCharacter(new Vector2(-1, 0));
    }

    //button handler for moving right
    void MoveRight() {
        MoveCharacter(new Vector2(1, 0));
    }

    //button handler for stop moving
    void StopMoving() {
        MoveCharacter(Vector2.zero);
    }

    //sends information to start jumping to playercontroller
    void StartJump() {
        Debug.Log("Jump");
        playerController.SendMessage("Jump", value: 1f);
    }

    //sends information to stop jumping to playercontroller
    void StopJump() {
        Debug.Log("NoJump");
        playerController.SendMessage("Jump", value: 0f);
    }
}
