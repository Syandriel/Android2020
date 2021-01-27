using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for handeling the movement of an enemy
public class SimpleMovingEnemy : MonoBehaviour {

    public Transform enemy;

    // For Debugging
    public Transform leftAnchor;
    public Transform rightAnchor;

    public Vector2 leftOffset;
    public Vector2 rightOffset;

    public float lerpSpeed = 0.01f;
    public float lerpStart = 0.5f;

    private Vector2 center;

    private float lerpDistance;
    private bool goingRight;

    //Setup variables needed at start
    void Start() {
        center = enemy.position;
        lerpDistance = lerpStart;
    }

    //Move the enemy continous on a linear path back and forth
    void Update() {

        Vector2 lerpedPosition = Vector2.Lerp(center + leftOffset, center + rightOffset, lerpDistance);
        enemy.position = lerpedPosition;
        if (goingRight) {
            lerpDistance += lerpSpeed;
            if (lerpDistance > 1f) {
                lerpDistance = 1f;
                goingRight = false;
            }
        } else {
            lerpDistance -= lerpSpeed;
            if (lerpDistance < 0) {
                lerpDistance = 0;
                goingRight = true;
            }
        }

        //For Debug
        leftAnchor.localPosition = leftOffset;
        rightAnchor.localPosition = rightOffset;
        Debug.DrawLine(leftAnchor.position, rightAnchor.position, Color.red);
    }
}
