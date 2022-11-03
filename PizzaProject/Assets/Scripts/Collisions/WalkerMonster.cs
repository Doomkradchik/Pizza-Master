using System.Collections;
using UnityEngine;

public class WalkerMonster : Enemy
{
    private const float JUMP_RAY_DEGREE = -25f;
    private const float DISTANCE_JUMP_RAY = 1f;
    private const float DISTANCE_WALL_RAY = 0.2f;

    private readonly float _deltaRay = 1f;
    private readonly float _duration = 5f;
    private readonly float _minScale = 0.05f;

    private Movement _movement;
    private Jumper _jumper;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
        _movement = GetComponent<Movement>();
        _jumper = GetComponent<Jumper>();
    }
    void FixedUpdate()
    {
        DetectWalls();

        _movement.Move(RightDirection);

        if (IsEndOfRoad() && _jumper.Grounded)
             _jumper.PlayJumpAnimation(_jumper.BaseData);
    }

    public void Respawn()
    {
        enabled = false;
        transform.position = _startPosition;
        StartCoroutine(AppearingAnimation());
    }

    private IEnumerator AppearingAnimation()
    {
        var expiredSeconds = 0f;
        var progress = 0f;
        var scale = transform.localScale;

        while (progress < 1f)
        {
            expiredSeconds += Time.fixedDeltaTime;
            progress = (float)(expiredSeconds / _duration);

            transform.localScale = Vector3.Lerp(Vector3.one * _minScale,
                scale, progress);

            yield return null;
        }

        transform.localScale = scale;
        enabled = true;
    }

    private void DetectWalls()
    {
        var direction = Vector2.right * RightDirection;
        direction.Normalize();

        var ray = new Ray2D(transform.position + Vector3.right * _deltaRay , direction);
        var hit = Physics2D.Raycast(ray.origin, ray.direction, DISTANCE_WALL_RAY);

        Debug.DrawRay(ray.origin, ray.direction * DISTANCE_WALL_RAY);

        if (hit == false)
            return;

        _right = !_right;
    }

    private bool IsEndOfRoad()
    {
        var degrees = Mathf.Deg2Rad * JUMP_RAY_DEGREE;
        var delta = new Vector2( Mathf.Cos(degrees), 
            Mathf.Sin(degrees));

        var direction = -Vector2.up + new Vector2(delta.x * RightDirection, delta.y);
        direction.Normalize();

        var ray = new Ray2D(transform.position + (Vector3)direction * _deltaRay, direction);
        var hit = Physics2D.Raycast(ray.origin, ray.direction, DISTANCE_JUMP_RAY);

        Debug.DrawRay(ray.origin, ray.direction * DISTANCE_JUMP_RAY);

        return !hit;
    }

}
