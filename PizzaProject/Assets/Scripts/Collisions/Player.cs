using System;
using UnityEngine;

public class Player : PhysicsEventBroadcaster
{
    [SerializeField]
    private Jumper _jumper;

    [SerializeField]
    private Movement _movement;

    [SerializeField]
    private Animator _animator;

    private readonly string _jumpKey = "Jump";
    private readonly string _isGroundKey = "isGround";
    private readonly string _isWalkingKey = "isWalking";

    public Jumper Jumper => _jumper;
    public Movement Movement => _movement;
    public Pizza Pizza { get; private set; }

    private void OnEnable()
    {
        Pizza = new Pizza();
        Pizza.ToppingAdded += () => Debug.Log("Added");//
        Pizza.Cleared += () => Debug.Log("Cleared");//

        Movement.Moved += () => _animator.SetBool(_isWalkingKey, true);
        Movement.Stopped += () => _animator.SetBool(_isWalkingKey, false);
        Jumper.MovedUp += () => _animator.SetBool(_isGroundKey, false);
        Jumper.MovedUp += () => _animator.SetTrigger(_jumpKey);
        Jumper.Landed += () => _animator.SetBool(_isGroundKey, true);
    }

    public void TryClimb(float vertical)
    {
        if (Movement.InterructedWithLadder == false)
        {
            Movement.ResetGravity();
            return;
        }

        Movement.AddStaticHorizontalVelocity(vertical);
    }
}
