using System.Collections;
using System;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField]
    private Data _baseData;

    [SerializeField]
    private Rigidbody2D _target;

    public bool Grounded { get; private set; } = true;
    public Data BaseData => _baseData;
    private Coroutine _jumpAnimation;
    private const float MIN_FALLING_VELOCITY = -3f;

    public event Action MovedUp;
    public event Action Landed;

    public bool IsFalling { get; private set; } = false;

    [ContextMenu("Play Jump Animation")]
    public void PlayJumpAnimation(Data data)
    {
        Interrupt();

        _jumpAnimation = StartCoroutine(AnimateJump(data));
        MovedUp?.Invoke();
    }

    public void Interrupt()
    {
        if (_jumpAnimation != null)
            StopCoroutine(_jumpAnimation);
        Landed?.Invoke();
        StopCoroutine(DetectFalling());
    }

    private IEnumerator AnimateJump(Data data)
    {
        Grounded = false;
        var expiredSeconds = 0f;
        var progress = 0f;
        var vertical = transform.position.y;

        while (progress < 1f)
        {
            expiredSeconds += Time.fixedDeltaTime;
            progress = (float)(expiredSeconds / data._duration);

            var posY = vertical + data._verticalAnimation.Evaluate(progress)
              * data._height;

            var position = new Vector2(transform.position.x,
                posY);

            transform.position = (position);

            yield return null;
        }
    }

    private IEnumerator DetectFalling()
    {
        while (true)
        {
            if (_target.velocity.y < MIN_FALLING_VELOCITY)
            {
                IsFalling = true;
                break;
            }

            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") == false)
            return;

        Interrupt();
        IsFalling = false;
        Grounded = true;
        Landed?.Invoke();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") == false)
            return;

        StartCoroutine(DetectFalling());
    }



    [Serializable]
    public class Data
    {
        public float _height;
        public float _duration;
        public AnimationCurve _verticalAnimation;
    }
}
