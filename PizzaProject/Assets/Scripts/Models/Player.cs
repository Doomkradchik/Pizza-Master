using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PhysicsEventBroadcaster
{
    [SerializeField]
    private Jumper _jumper;

    [SerializeField]
    private Movement _movement;

    [SerializeField]
    private Animator _animator;

    [Header("DyingAnimation")]
    [SerializeField]
    private AnimationCurve _verticalAnim;
    [SerializeField]
    private float _duration;
    [SerializeField]
    private float _maxHeight;

    private readonly string _jumpKey = "Jump";
    private readonly string _isGroundKey = "isGround";
    private readonly string _isWalkingKey = "isWalking";
    private readonly string _winKey = "Win";

    public Jumper Jumper => _jumper;
    public Movement Movement => _movement;
    public Pizza Pizza { get; private set; }

    private void OnEnable()
    {
        Pizza = new Pizza();

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

    public void OnWin() => _animator.SetTrigger(_winKey);

    public void StartDyingAnimation() => StartCoroutine(DyingAnimation());

    private IEnumerator DyingAnimation()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        GetComponent<Rigidbody2D>().freezeRotation = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        var expiredSeconds = 0f;
        var progress = 0f;
        var vertical = transform.position.y;

        while (progress < 1f)
        {
            expiredSeconds += Time.fixedDeltaTime;
            progress = (float)(expiredSeconds / _duration);

            var posY = vertical + _verticalAnim.Evaluate(progress)
              * _maxHeight;

            var position = new Vector2(transform.position.x,
                posY);

            transform.position = (position);

            yield return null;
        }
    }


}
