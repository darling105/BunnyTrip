using UnityEngine;

public class SplineTrigger : MonoBehaviour
{
    public bool forward = true;
    public SplineFollower follower;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var state = other.GetComponent<PlayerSplineState>();
        if (state == null || state.IsOnSpline) return; // Đang đi rồi thì bỏ qua

        var ctrl = other.GetComponent<PlayerController>();
        if (ctrl != null) ctrl.enabled = false;

        follower.StartFollowing(forward);
    }
}
