using System.Collections.Generic;
using System;
using UnityEngine;

public sealed class Pizza
{
    public Pizza()
    {
        _components = new List<ToppingSection>();
    }

    public event Action<ToppingSection> ToppingAdded;
    public event Action Cleared;
    public bool TrashCollided { get; set; }
    public IEnumerable<ToppingSection> Sections => _components;

    private List<ToppingSection> _components;

    public bool CanClean()
    {
        return TrashCollided && _components.Count > 0;
    }

    public void AddTopping(ToppingData topping)
    {
        var section = _components
            .Find(component => component.Toppping.Kind == topping.Kind);

        if (section == null)
        {
            var newSection = new ToppingSection(topping);
            _components.Add(newSection);
            ToppingAdded?.Invoke(newSection);
            return;
        }

        section.Amount++;
        ToppingAdded?.Invoke(section);
    }

    public void CleanUp()
    {
        if (CanClean() == false)
            return;

        _components = new List<ToppingSection>();
        Cleared?.Invoke();
    }

    [Serializable]
    public class ToppingSection
    {
        public ToppingSection(ToppingData topping, int amount = 1)
        {
            Toppping = topping;
            _amount = amount;
        }
        public ToppingData Toppping { get; }
        private int _amount;

        public int Amount
        {
            get => _amount;
            set => _amount = value;
        }
    }

    public class ToppingData
    {
        public ToppingData(Topping.Kind Kind, Sprite toppingIcon)
        {
            this.Kind = Kind;
            ToppingIcon = toppingIcon;
        }

        public Topping.Kind Kind { get; }
        public Sprite ToppingIcon { get; }
    }
}