using System.Collections;
using System;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField]
    private Data _baseData;

    public bool Grounded { get; private set; } = true;
    public event Action Landed;
    public Data BaseData => _baseData;

    private Coroutine _jumpAnimation;


    [ContextMenu("Play Jump Animation")]
    public void PlayJumpAnimation(Data data)
    {
        _jumpAnimation = StartCoroutine(AnimateJump(data));
    }

    public void Interrupt()
    {
        StopCoroutine(_jumpAnimation);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_jumpAnimation != null)
            StopCoroutine(_jumpAnimation);

        Grounded = true;
        Landed?.Invoke();
    }


    [Serializable]
    public class Data
    {
        public float _height;
        public float _duration;
        public AnimationCurve _verticalAnimation;
    }
}
