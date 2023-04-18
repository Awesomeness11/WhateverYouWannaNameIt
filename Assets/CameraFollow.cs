using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    public float SmoothTime = 0.95f;
    public Vector3 offset;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, SmoothTime);
        transform.position = smoothedPosition;
    }

}
