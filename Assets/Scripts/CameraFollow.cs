using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    public GameObject player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void LateUpdate() {
        Vector3 desiredPosition = player.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
        transform.LookAt(player.transform);
    }
}