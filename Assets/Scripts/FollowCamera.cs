using UnityEngine;

//Camera script to follow the player character
public class FollowCamera : MonoBehaviour {

    public Transform target;
    public Rigidbody2D targetBody;
    [Range(0, 1)]
    public float smoothSpeed = 0.075f;
    public Vector3 offset;
    public float maxDistanceAwayX = 5;
    public float maxDistanceAwayY = 0.1f;
    public float startSize = 8.41f;
    public float zoomedSize = 10.7f;
    [Range(0, 1)]
    public float smoothZoomSpeed = 0.125f;

    //Snap to the position of the player at the start
    private void Start() {
        transform.position = target.position + offset;
    }

    //Follow the player with smoothed movement
    private void FixedUpdate() {

        float velocityZoom = Mathf.Abs(targetBody.velocity.x) / 13.662f;
        float refVelocity = 0f;

        float targetedSize = Mathf.Lerp(startSize, zoomedSize, velocityZoom);
        Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, targetedSize, ref refVelocity, smoothZoomSpeed);

        Vector3 cameraCenter = transform.position;
        Vector3 targetPostion = target.position + offset;
        Vector3 velocity = Vector3.zero;

        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPostion, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;

    }

}
