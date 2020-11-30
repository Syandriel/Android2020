using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour {

    public Transform player;
    public PlayerController playerController;

    public Transform outerJoystick;
    public Transform innerJoystick;

    public Canvas control;
    public Button attack;
    public Button special;
    public UpDownButton jump;
    public Button grab;

    public float clampDistance;
    [Range(0.1f, 1)]
    public float movementFactor;

    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;
    private Vector2 startA;

    // Start is called before the first frame update
    void Start() {
        startA = outerJoystick.transform.position;

        //TODO: make platform specific
        attack.onClick.AddListener(Attack);
        special.onClick.AddListener(Special);
        jump.onButtonDown.AddListener(StartJump);
        jump.onButtonUp.AddListener(StopJump);
        grab.onClick.AddListener(Grab);
    }

    // Update is called once per frame
    void Update() {

#if (UNITY_ANROID || UNITY_IOS || true)
        //TODO: Maybe convert to UI
        //TODO: Refactor to use Input.Touch()
        //TODO: make Joystick always visible and snap to point of touch
        if (Input.GetMouseButtonDown(0)) {
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            if (pointA.x < 0) {
                outerJoystick.transform.position = pointA;
                //outerJoystick.GetComponent<SpriteRenderer>().enabled = true;
                //innerJoystick.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        //TODO: Refactor to use Input.Touch()
        if(Input.GetMouseButton(0)) {
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        } else {
            touchStart = false;
            outerJoystick.transform.position = startA;
            innerJoystick.transform.position = startA;
            //outerJoystick.GetComponent<SpriteRenderer>().enabled = false;
            //innerJoystick.GetComponent<SpriteRenderer>().enabled = false;
        }
#else
        //Input for PC
        //MoveCharacter(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

#endif
    }

    // FixedUpdate is called every physics update
    void FixedUpdate() {
        //TODO: Distinguish between Mobile and PC
#if (UNITY_ANROID || UNITY_IOS || true)
        if(touchStart && pointA.x < 0) {
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, clampDistance);

            //TODO: instead sendMessage to PlayerController for movement
            MoveCharacter(direction);

            innerJoystick.transform.position = pointA + direction;
        } else {
            MoveCharacter(Vector2.zero);
        }
#else
        //TODO: PC Update function here
#endif
    }

    void MoveCharacter(Vector2 direction) {
        //player.Translate(direction * Time.deltaTime);
        playerController.SendMessage("InputMovement", Vector2.ClampMagnitude(direction, movementFactor));
    }

    void Attack() {
        //TODO: send Message to PlayerController->Attack()
        playerController.SendMessage("Attack");
        Debug.Log("ATTACK");
    }

    void Special() {
        playerController.SendMessage("Special");
        Debug.Log("SPECIAL");
    }

    void StartJump() {
        Debug.Log("JUMP");
        playerController.SendMessage("Jump", value: 1f);
    }

    void StopJump() {
        Debug.Log("Stop Jump");
        playerController.SendMessage("Jump", value: 0f);
    }

    void Grab() {
        playerController.SendMessage("Grab");
        Debug.Log("grab");
    }
}
