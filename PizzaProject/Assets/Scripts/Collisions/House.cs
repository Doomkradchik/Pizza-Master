using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class House : PhysicsEventBroadcaster
{
    [SerializeField]
    private HouseTaskTableView _view;

    [SerializeField]
    private HouseStateView _viewState;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private SectionRequest[] _requests;

    private List<Pizza.ToppingSection> _requiredSections;

    public event Action GameEnded;
    private bool _isOppened = false;

    private void Start()
    {
        InitializeRequestsView();

        _player.Pizza.ToppingAdded += OnToppingAdded;
        _player.Pizza.ToppingAdded += (section) => _isOppened = IsCompleted();
        _player.Pizza.ToppingAdded += (section) => _viewState.UpdateState(_isOppened);

        _player.Pizza.Cleared += OnCleared;
        _player.Pizza.Cleared += () => _viewState.UpdateState(_isOppened);
    }

    private void OnDisable()
    {
       _player.Pizza.ToppingAdded -= OnToppingAdded;
       _player.Pizza.Cleared -= OnCleared;
    }

    public void CheckCurrentGameState()
    {
        if (_isOppened)
            GameEnded?.Invoke();
    }

    private void InitializeRequestsView()
    {
        _requiredSections = new List<Pizza.ToppingSection>();
        foreach (var request in _requests)
        {
            var data = new Pizza.ToppingData(request._kind, request._toppingSprite);
            _requiredSections.Add(new Pizza.ToppingSection(data, request._amount));

            _view.CreateCell(data, 0, request._amount);
        }
    }

    private void OnCleared()
    {
        _view.RemoveAllCells();
        InitializeRequestsView();
    }

    // to do

    private void OnToppingAdded(Pizza.ToppingSection section)
    {
        TableCellView cell;
        var request = _requiredSections
            .ToList()
            .Find(r => r.Toppping.Kind == section.Toppping.Kind);
        if (request == null)
        {
             cell = _view.Cells
                .ToList()
                .Find(c => c.Topping.Kind == section.Toppping.Kind);

            if (cell == null)
                _view.CreateCell(section.Toppping, section.Amount, 0);
        }
        else
        {
             cell = _view.Cells
                .ToList()
                .Find(c => c.Topping.Kind == request.Toppping.Kind);  
        }

        if(cell != null)
            cell.UpdateCell(section.Amount);
    }


    private bool IsCompleted()
    {
        var pizza = _player.Pizza;

        if (pizza.Sections.Count() != _requiredSections.Count())
            return false;
        foreach (var task in _requiredSections)
        {
            var component = pizza.Sections
                .ToList()
                .Find(c => c.Toppping.Kind == task.Toppping.Kind);
            if (component == null)
                return false;
            if (component.Amount != task.Amount)
                return false;
        }
        return true;
    }

    [Serializable]
    public class SectionRequest
    {
        public Topping.Kind _kind;
        public Sprite _toppingSprite;
        public int _amount;
    }    
}
