using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ToppingsSpawner : MonoBehaviour, IPauseHandler
{
    [SerializeField]
    private float _deltaTime;

    [SerializeField]
    private Topping[] _toppings;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private FallingSimulation _simulation;

    private readonly float _verticalOffset = 1f;

    private void Awake() => PauseManager.Subscribe(this);
    private void OnDestroy() => PauseManager.Unsubscribe(this);

    public void Pause()
    {
        StopAllCoroutines();
    }

    public void Unpause()
    {
        StartCoroutine(SpawningRoutine());
    }

    private Vector2 RandomPosition
    {
        get
        {
            var left = Vector2.up;
            var right = new Vector2(1f, 1f);

            right = _camera.ViewportToWorldPoint(right);
            left = _camera.ViewportToWorldPoint(left);

            right += Vector2.up * _verticalOffset;
            left += Vector2.up * _verticalOffset;

            return Vector2.Lerp(left, right, Random.value);
        }
    }

    private void SpawnTopping(Topping prefab)
    {
        var instance = Instantiate(prefab);
        instance.GetComponent<Rigidbody2D>().position = RandomPosition;

         var fallingSimComponent
            = instance.gameObject.GetComponent<FallingSimulationComponent>();

        if (fallingSimComponent == null)
            throw new InvalidCastException();

        fallingSimComponent.SetRandomFloorPoint();
        _simulation.StartSimulation(fallingSimComponent);
    }

    private IEnumerator SpawningRoutine()
    {
        while (true)
        {
            var topping = _toppings[Random.Range(0, _toppings.Length)];
            SpawnTopping(topping);
            yield return new WaitForSeconds(_deltaTime);
        }
    }
}