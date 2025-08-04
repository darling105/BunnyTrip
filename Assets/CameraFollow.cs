using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                        // Thỏ
    public Vector3 followOffset = new Vector3(0f, 6f, -6f); // Giống camera hamster
    public float smoothTime = 0.15f;                // Mượt chuyển động

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // Vị trí cần đến
        Vector3 targetPosition = target.position + followOffset;

        // Di chuyển mượt mà (không lookAt, không xoay camera)
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
