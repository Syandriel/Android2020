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
    public float movementFactor = 0.198f;

    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;
    private Vector2 startA;

    private Text debugText;

    // Start is called before the first frame update
    void Start() {

        debugText = GameObject.FindObjectOfType<Text>();

#if (UNITY_ANDROID || UNITY_IOS) //Handy-Seite
        startA = outerJoystick.transform.position;

        attack.onClick.AddListener(Attack);
        special.onClick.AddListener(Special);
        jump.onButtonDown.AddListener(StartJump);
        jump.onButtonUp.AddListener(StopJump);
        grab.onClick.AddListener(Grab);
#else //PC-Seite
        attack.gameObject.SetActive(false);
        special.gameObject.SetActive(false);
        jump.gameObject.SetActive(false);
        grab.gameObject.SetActive(false);

        outerJoystick.gameObject.SetActive(false);
        innerJoystick.gameObject.SetActive(false);
#endif

    }

    // Update is called once per frame
    void Update() {

#if (UNITY_ANDROID || UNITY_IOS) //Handy-Seite

        int closestFinger = 0;
        Vector3 touchPosition = Vector3.zero;
        float distanceToJoystick = float.MaxValue;

        foreach (Touch touch in Input.touches) {
            if (touch.phase == TouchPhase.Began) {
                touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.transform.position.z));
                if (touch.position.x < Screen.width / 2) {
                    float distance = Vector3.Distance(touchPosition, outerJoystick.transform.position);
                    Debug.Log(distance + " " + touch.fingerId);
                    if (distance < distanceToJoystick) {
                        closestFinger = touch.fingerId;
                        distanceToJoystick = distance;
                    }
                }
            }
        }

        if (Input.touchCount > 0) {
            TouchPhase touchPhase = Input.GetTouch(closestFinger).phase;
            if (touchPhase == TouchPhase.Began) {
                pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[closestFinger].position.x, Input.touches[closestFinger].position.y, Camera.main.transform.position.z));
                if (pointA.x < 0) {
                    outerJoystick.transform.position = pointA;
                }
            }
            if (touchPhase == TouchPhase.Moved) {
                touchStart = true;
                pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[closestFinger].position.x, Input.touches[closestFinger].position.y, Camera.main.transform.position.z));
            } else {
                touchStart = false;
                outerJoystick.transform.position = startA;
                innerJoystick.transform.position = startA;
            }
        }
#else //PC-Seite

#endif
    }

    // FixedUpdate is called every physics update
    void FixedUpdate() {
#if (UNITY_ANDROID || UNITY_IOS) //Handy-Seite
        if (touchStart && pointA.x < 0) {
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, clampDistance);

            MoveCharacter(direction);

            innerJoystick.transform.position = pointA + direction;
        } else {
            MoveCharacter(Vector2.zero);
        }
#else //PC-Seite
        MoveCharacter(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

        if(Input.GetAxis("Jump") > 0) {
            StartJump();
        } else {
            StopJump();
        }
        if(Input.GetAxis("Attack") == 1) {
            Attack();
        }
        if(Input.GetAxis("Special") == 1) {
            Special();
        }
        if(Input.GetAxis("Grab") == 1) {
            Grab();
        }

#endif
    }

    void MoveCharacter(Vector2 direction) {
        Debug.Log(direction.ToString());
        playerController.SendMessage("InputMovement", Vector2.ClampMagnitude(direction, movementFactor));
    }

    void Attack() {
        Debug.Log("Attack");
        playerController.SendMessage("Attack");
    }

    void Special() {
        Debug.Log("Special");
        playerController.SendMessage("Special");
    }

    void StartJump() {
        Debug.Log("Jump");
        playerController.SendMessage("Jump", value: 1f);
    }

    void StopJump() {
        Debug.Log("NoJump");
        playerController.SendMessage("Jump", value: 0f);
    }

    void Grab() {
        Debug.Log("Grab");
        playerController.SendMessage("Grab");
    }
}
