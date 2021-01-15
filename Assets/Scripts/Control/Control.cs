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
    public Button attack;
    public Button special;
    public UpDownButton jump;
    public Button grab;
    public UpDownButton left;
    public UpDownButton right;

    public float clampDistance;
    [Range(0.1f, 1)]
    public float movementFactor = 0.198f;

    //private bool touchStart = false;
    //private bool resetJoystick = true;
    //private Vector2 pointA;
    //private Vector2 pointB;
    //private Vector2 startA;
    //private Vector2 touch;

    //private Text debugText;

    //private bool emergency = true;

    // Start is called before the first frame update
    void Start() {

        //debugText = GameObject.FindObjectOfType<Text>();

#if ((UNITY_ANDROID || UNITY_IOS)) //Handy-Seite
        attack.onClick.AddListener(Attack);

        special.onClick.AddListener(Special);

        jump.onButtonDown.AddListener(StartJump);
        jump.onButtonUp.AddListener(StopJump);

        grab.onClick.AddListener(Grab);

        left.onButtonDown.AddListener(MoveLeft);
        left.onButtonUp.AddListener(StopMoving);

        right.onButtonDown.AddListener(MoveRight);
        right.onButtonUp.AddListener(StopMoving);
#else //PC-Seite
        attack.gameObject.SetActive(false);
        special.gameObject.SetActive(false);
        jump.gameObject.SetActive(false);
        grab.gameObject.SetActive(false);
        left.gameObject.SetActive(false);
        right.gameObject.SetActive(false);
#endif

    }

    // Update is called once per frame
    void Update() {

    }

    // FixedUpdate is called every physics update
    void FixedUpdate() {
#if (!(UNITY_ANDROID || UNITY_IOS)) //Handy-Seite
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
        Debug.Log(direction);
        playerController.SendMessage("InputMovement", Vector2.ClampMagnitude(direction, movementFactor));
    }
    
    void MoveLeft() {
        MoveCharacter(new Vector2(-1, 0));
    }

    void MoveRight() {
        MoveCharacter(new Vector2(1, 0));
    }

    void StopMoving() {
        MoveCharacter(Vector2.zero);
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

    //Vector3 Multiply(Vector3 a, Vector3 b) {
    //    Vector3 result = new Vector3();
    //    result.x = a.x * b.x;
    //    result.y = a.y * b.y;
    //    result.z = a.z * b.z;

    //    return result;
    //}

    //Vector2 SignedClampMagnitude(Vector2 vector, float maxLength) { 
    //    bool positiveX = vector.x > 0;
    //    bool positiveY = vector.y > 0;

    //    float magnitude = vector.magnitude;
    //    float minimum = Mathf.Min(vector.magnitude, maxLength);
    //    float factor = minimum / magnitude;

    //    float adjustedX = vector.x * factor * (positiveX ? 1 : -1);
    //    float adjustedY = vector.y * factor * (positiveY ? 1 : -1);

    //    return new Vector2(adjustedX, adjustedY);
    //}

    //private void OnDrawGizmos() {
    //    DrawEllipse(touch, Vector3.forward, Vector3.up, 1 * transform.localScale.x, 1 * transform.localScale.y, 32, Color.red);
    //}

    //private static void DrawEllipse(Vector3 pos, Vector3 forward, Vector3 up, float radiusX, float radiusY, int segments, Color color, float duration = 0) {
    //    float angle = 0f;
    //    Quaternion rot = Quaternion.LookRotation(forward, up);
    //    Vector3 lastPoint = Vector3.zero;
    //    Vector3 thisPoint = Vector3.zero;

    //    for (int i = 0; i < segments + 1; i++) {
    //        thisPoint.x = Mathf.Sin(Mathf.Deg2Rad * angle) * radiusX;
    //        thisPoint.y = Mathf.Cos(Mathf.Deg2Rad * angle) * radiusY;

    //        if (i > 0) {
    //            Debug.DrawLine(rot * lastPoint + pos, rot * thisPoint + pos, color, duration);
    //        }

    //        lastPoint = thisPoint;
    //        angle += 360f / segments;
    //    }
    //}
}
