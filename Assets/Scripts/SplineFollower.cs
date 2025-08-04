using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

[RequireComponent(typeof(PlayerSplineState))]
public class SplineFollower : MonoBehaviour
{
    public SplineContainer splineContainer;
    public float speed = 5f;

    private float t;
    private bool isMoving = false;
    private bool forward = true;
    private PlayerSplineState splineState;

    void Start()
    {
        splineState = GetComponent<PlayerSplineState>();
    }

    void Update()
    {
        if (!isMoving || splineContainer == null) return;

        float length = splineContainer.CalculateLength();
        float delta = (speed / length) * Time.deltaTime;
        t += forward ? delta : -delta;

        t = Mathf.Clamp01(t);

        splineContainer.Evaluate(t, out var pos, out var tangent, out var up);
        transform.position = pos;
        transform.rotation = Quaternion.LookRotation(tangent, up);

        if (t == 0f || t == 1f)
        {
            isMoving = false;
            splineState.ExitSpline();
            GetComponent<PlayerController>().enabled = true;
        }
    }

    public void StartFollowing(bool goForward)
    {
        forward = goForward;
        t = forward ? 0f : 1f;
        isMoving = true;
        splineState.EnterSpline(forward);
    }
}
