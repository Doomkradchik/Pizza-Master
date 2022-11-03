using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FallingSimulationComponent : MonoBehaviour
{
    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private float _speed;

    private const int MAX_COLLISIONS = 100;
    private const float MIN_DISTANCE = 0.3f;

    private float _direction;


    public void StartFalling(Action onLanded)
    {
        StartCoroutine(Falling(onLanded));
    }

    private IEnumerator Falling(Action onLanded)
    {
        var offset = Vector2.up * _speed * Time.fixedDeltaTime;
        var rigidbody = GetComponent<Rigidbody2D>();
        float distance;
        do
        {
            rigidbody.MovePosition(rigidbody.position - offset);

            distance = rigidbody.position.y - _direction;
            distance = Mathf.Abs(distance);

            yield return null;
        }
        while (distance > MIN_DISTANCE);

        onLanded?.Invoke();
    }

    public void SetRandomFloorPoint()
    {
        var results = new RaycastHit2D[MAX_COLLISIONS];

        var contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(_layerMask);

        var amount = GetComponent<Collider2D>()
            .Cast(-Vector2.up, contactFilter, results);

        if (amount <= 0)
        {
            Destroy(gameObject);
            return;
        }

        _direction = results[Random.Range(0, amount)].point.y;
    }
}
