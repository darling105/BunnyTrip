using UnityEngine;

public class TouchInput : MonoBehaviour
{
    public static Vector2 SwipeDirection { get; private set; }

    private Vector2 startTouch;
    private Vector2 endTouch;
    private float minSwipeDistance = 50f;

    void Update()
    {
        SwipeDirection = Vector2.zero;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouch = touch.position;
                    break;
                case TouchPhase.Ended:
                    endTouch = touch.position;
                    Vector2 swipe = endTouch - startTouch;

                    if (swipe.magnitude > minSwipeDistance)
                    {
                        swipe.Normalize();
                        if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
                            SwipeDirection = swipe.x > 0 ? Vector2.right : Vector2.left;
                        else
                            SwipeDirection = swipe.y > 0 ? Vector2.up : Vector2.down;
                    }
                    break;
            }
        }
    }
}
