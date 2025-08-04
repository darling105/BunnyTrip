using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask wallLayer;

    private Vector3 moveDirection;
    private bool isMoving = false;

    void Update()
    {
        if (!isMoving && TouchInput.SwipeDirection != Vector2.zero)
        {
            moveDirection = new Vector3(TouchInput.SwipeDirection.x, 0, TouchInput.SwipeDirection.y);

            if (moveDirection != Vector3.zero)
                transform.forward = moveDirection;

            StartCoroutine(Move());
        }
    }

    System.Collections.IEnumerator Move()
    {
        isMoving = true;

        while (CanMove())
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            yield return null;
        }

        isMoving = false;
    }

    bool CanMove()
    {
        Ray ray = new Ray(transform.position, moveDirection);
        return !Physics.Raycast(ray, 0.6f, wallLayer);
    }
}
