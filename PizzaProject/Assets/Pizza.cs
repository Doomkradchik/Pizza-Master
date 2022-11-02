using System.Collections.Generic;
using System;

public sealed class Pizza
{
    public Pizza()
    {
        _toppings = new List<Topping>();
    }
    public event Action<Topping> ToppingAdded;
    private List<Topping> _toppings;

    public void AddTopping(Topping topping)
    {
        _toppings.Add(topping);
        ToppingAdded?.Invoke(topping);
    }

    public void Clear()
    {
        _toppings = new List<Topping>();
    }
}