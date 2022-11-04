using UnityEngine;
using System.Collections.Generic;

public class HouseTaskTableView : MonoBehaviour
{
    [SerializeField]
    private Transform _grid;

    [SerializeField]
    private TableCellView _cellView;

    private List<TableCellView> _cells;
    public IEnumerable<TableCellView> Cells => _cells;

    private void Start()
    {
        _cells = new List<TableCellView>();
    }

    public void RemoveAllCells()
    {
        foreach (var cell in _cells)
            Destroy(cell.gameObject);

        _cells = new List<TableCellView>();
    }

    public void CreateCell(Pizza.ToppingData topping, int amount, int requested)
    {
        var cell = Create();
        cell.Init(topping, requested);
        cell.UpdateCell(amount);
    }

    private TableCellView Create()
    {
        var cell = Instantiate(_cellView);
        cell.transform.parent = _grid;
        _cells.Add(cell);
        return cell;
    }
}
