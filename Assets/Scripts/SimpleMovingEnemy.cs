using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovingEnemy : MonoBehaviour
{

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

    private 

    // Start is called before the first frame update
    void Start()
    {
        center = enemy.position;
        lerpDistance = lerpStart;
        //For Debuging
        leftAnchor.localPosition = leftOffset;
        rightAnchor.localPosition = rightOffset;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 leftSide = leftAnchor.position;
        Vector2 rightSide = rightAnchor.position;

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

        Debug.DrawLine(leftAnchor.position, rightAnchor.position, Color.red);
    }
}
