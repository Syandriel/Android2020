using UnityEngine;

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

    private void Start() {
        transform.position = target.position + offset;
    }

    private void FixedUpdate() {

        float velocityZoom = Mathf.Abs(targetBody.velocity.x) / 13.662f;
        float refVelocity = 0f;

        float targetedSize = Mathf.Lerp(startSize, zoomedSize, velocityZoom);
        Debug.Log(velocityZoom);
        Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, targetedSize, ref refVelocity, smoothZoomSpeed);

        Vector3 cameraCenter = transform.position;
        Vector3 targetPostion = target.position + offset;
        Vector3 velocity = Vector3.zero;
        Vector2 distance = new Vector2(Mathf.Abs(cameraCenter.x - targetPostion.x), Mathf.Abs(cameraCenter.y - targetPostion.y));

        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPostion, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;

    }

    private void OnDrawGizmos() {
        DrawEllipse(transform.position - offset, transform.forward, transform.up, maxDistanceAwayX * transform.localScale.x, maxDistanceAwayY * transform.localScale.y, 32, Color.red);
    }

    private static void DrawEllipse(Vector3 pos, Vector3 forward, Vector3 up, float radiusX, float radiusY, int segments, Color color, float duration = 0) {
        float angle = 0f;
        Quaternion rot = Quaternion.LookRotation(forward, up);
        Vector3 lastPoint = Vector3.zero;
        Vector3 thisPoint = Vector3.zero;

        for (int i = 0; i < segments + 1; i++) {
            thisPoint.x = Mathf.Sin(Mathf.Deg2Rad * angle) * radiusX;
            thisPoint.y = Mathf.Cos(Mathf.Deg2Rad * angle) * radiusY;

            if (i > 0) {
                Debug.DrawLine(rot * lastPoint + pos, rot * thisPoint + pos, color, duration);
            }

            lastPoint = thisPoint;
            angle += 360f / segments;
        }
    }

}
