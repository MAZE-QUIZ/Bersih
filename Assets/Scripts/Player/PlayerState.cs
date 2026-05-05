using UnityEngine;

public enum MovementState
{
    Idle,
    Moving
}

public class PlayerState : MonoBehaviour
{
    public MovementState CurrentState { get; private set; } = MovementState.Idle;

    public Vector2 FacingDirection { get; private set; } = Vector2.down;

    public void SetMovementState(MovementState newState)
    {
        CurrentState = newState;
    }

    public void SetFacingDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            FacingDirection = direction.normalized;
        }
    }
}