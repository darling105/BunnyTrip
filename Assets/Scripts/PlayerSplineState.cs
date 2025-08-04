using UnityEngine;

public class PlayerSplineState : MonoBehaviour
{
    public bool IsOnSpline { get; private set; }
    public bool GoingForward { get; private set; }

    public void EnterSpline(bool forward)
    {
        IsOnSpline = true;
        GoingForward = forward;
    }

    public void ExitSpline()
    {
        IsOnSpline = false;
    }
}
