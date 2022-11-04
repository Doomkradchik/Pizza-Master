using System;
using UnityEngine;
using UnityEngine.UI;

public class TableCellView : MonoBehaviour
{
    [SerializeField]
    private GameObject _relevantPrefab;

    [SerializeField]
    private GameObject _irrelevantPrefab;

    [SerializeField]
    private Image _icon;

    public Pizza.ToppingData Topping { get; private set; }
    private int _requested;
    private bool _inited = false;

    private readonly string _amountName = "amount";
    private readonly string _requestName = "request";

    public void Init(Pizza.ToppingData topping, int requested = 0)
    {
        try
        {
            Validate();
        }
        catch (Exception e)
        {
            enabled = false;
            throw e;
        }

        _requested = requested;
        Topping = topping;
        _inited = true;

        var relevant = requested != 0f;
        _relevantPrefab.SetActive(relevant);
        _irrelevantPrefab.SetActive(!relevant);      
    }

    public void UpdateCell(int amount)
    {
        Text amountText;
        Color color;

        _icon.sprite = Topping.ToppingIcon;

        if (_requested == 0f)
        {
            amountText = _irrelevantPrefab.transform.Find(_amountName)
                .GetComponent<Text>();

            amountText.text = amount.ToString();
            return;
        }

        if (amount > _requested)
           color = Color.red;
        else
            if (amount < _requested)
              color = Color.yellow;
        else
              color = Color.green;

        amountText = _relevantPrefab.transform.Find(_amountName).GetComponent<Text>();
        var requestText = _relevantPrefab.transform.Find(_requestName).GetComponent<Text>();

        amountText.text = amount.ToString();
        requestText.text = $"/{_requested}";
        amountText.color = color;
        requestText.color = color;
    }

    private void Validate()
    {
        if (_relevantPrefab.transform.Find(_amountName) == null)
            throw new InvalidOperationException();

        if (_relevantPrefab.transform.Find(_requestName) == null)
            throw new InvalidOperationException();

        if (_irrelevantPrefab.transform.Find(_amountName) == null)
            throw new InvalidOperationException();
    }
    
}
