using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class MultiplayerControl : NetworkBehaviour {

    //public Transform player;
    public MultiplayerController playerController;

    public Canvas control;
    public UpDownButton jump;
    public UpDownButton left;
    public UpDownButton right;

    public float clampDistance;
    [Range(0.1f, 1)]
    public float movementFactor = 0.198f;

    [SerializeField] private Text debugText;

    // Start is called before the first frame update
    [Client]
    void Start() {
#if ((UNITY_ANDROID || UNITY_IOS)) //Smartphone control
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

    [Client]
    void FixedUpdate() {
        if (playerController != null)
            debugText.text = playerController.ToString();
#if (!(UNITY_ANDROID || UNITY_IOS)) //PC control
        if (playerController != null) {
            MoveCharacter(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

            if (Input.GetAxis("Jump") > 0) {
                StartJump();
            } else {
                StopJump();
            }
        }
#endif
    }


    //sends movement information to the corresponding multiplayer controller
    [Client]
    void MoveCharacter(Vector2 direction) {
        Debug.Log(direction);
        playerController.SendMessage("InputMovement", Vector2.ClampMagnitude(direction, movementFactor));
    }

    //button handler for moving left
    [Client]
    void MoveLeft() {
        MoveCharacter(new Vector2(-1, 0));
    }

    //button handler for moving right
    [Client]
    void MoveRight() {
        MoveCharacter(new Vector2(1, 0));
    }

    //button handler for stop moving
    [Client]
    void StopMoving() {
        MoveCharacter(Vector2.zero);
    }

    //sends information to start jumping to multiplayer controller
    [Client]
    void StartJump() {
        Debug.Log("Jump");
        playerController.SendMessage("Jump", value: 1f);
    }

    //sends information to stop jumping to multiplayer controller
    [Client]
    void StopJump() {
        Debug.Log("NoJump");
        playerController.SendMessage("Jump", value: 0f);
    }

    //is called by MultiplayerController when connecting to a server
    //assigns this MultiplayerController if none could be assigned automaticly
    public void ConnectPlayer(MultiplayerController pc) {
        if (pc.isLocalPlayer && playerController == null)
            playerController = pc;
    }
}
