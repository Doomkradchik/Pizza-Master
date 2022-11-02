using System.Collections.Generic;
using System;

public sealed class Pizza
{
    public Pizza()
    {
        _components = new List<PizzaComponent>();
    }

    public event Action ToppingAdded;
    public event Action Cleared;
    public bool TrashCollided { get; set; }
    public IEnumerable<PizzaComponent> Components => _components;

    private List<PizzaComponent> _components;

    public bool CanClean()
    {
        return TrashCollided && _components.Count > 0;
    }

    public void AddTopping(Topping topping)
    {
        var component = _components
            .Find(component => component.Kind == topping.ToppingKind);

        if (component == null)
        {
            _components.Add(new PizzaComponent(topping.ToppingKind));
            ToppingAdded?.Invoke();
            return;
        }

        component.Amount++;
        ToppingAdded?.Invoke();
    }

    public void CleanUp()
    {
        if (CanClean() == false)
            return;

        _components = new List<PizzaComponent>();
        Cleared?.Invoke();
    }

    [Serializable]
    public class PizzaComponent
    {
        public PizzaComponent(Topping.Kind kind, float amount = 1f)
        {
            Kind = kind;
            _amount = amount;
        }
        public Topping.Kind Kind { get; }
        private float _amount;

        public float Amount
        {
            get => _amount;
            set => _amount = value;
        }
    }
}