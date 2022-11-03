using System;
using System.Linq;
using UnityEngine;

public class House : PhysicsEventBroadcaster
{
    [SerializeField]
    private Player _player;

    [SerializeField]
    private TaskPizzaComponent[] _requirements;

    public event Action GameEnded;
    private bool _isOppened = false;

    private void Start()
    {
        _player.Pizza.ToppingAdded += OnComponentsChanged;
        _player.Pizza.Cleared += OnComponentsChanged;
        GameEnded += () => Debug.Log("ended");//
    }

    private void OnDisable()
    {
        _player.Pizza.ToppingAdded -= OnComponentsChanged;
        _player.Pizza.Cleared -= OnComponentsChanged;
    }

    public void CheckCurrentGameState()
    {
        if (_isOppened)
            GameEnded?.Invoke();
    }

    private void OnComponentsChanged()
    {
        _isOppened = IsCompleted();
        Debug.Log(_isOppened);
    }

    private bool IsCompleted()
    {
        var pizza = _player.Pizza;

        if (pizza.Components.Count() != _requirements.Length)
            return false;

        foreach(var task in _requirements)
        {
            var component = pizza.Components
                .ToList()
                .Find(component => component.Kind == task._kind);

            if (component == null)
                return false;

            if (component.Amount != task._amount)
                return false;
        }

        return true;
    }

    [Serializable]
    public class TaskPizzaComponent
    {
        public Topping.Kind _kind;
        public float _amount;
    }    
}
