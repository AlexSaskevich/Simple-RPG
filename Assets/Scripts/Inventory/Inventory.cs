using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    private readonly List<Item> _items = new List<Item>();

    [SerializeField] private int _capacity;

    public event UnityAction ItemChanged;

    public IReadOnlyList<Item> Items => _items;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }

        Instance = this;
    }

    public bool TryAdd(Item item)
    {
        if (_items.Count >= _capacity)
        {
            return false;
        }

        _items.Add(item);
        ItemChanged?.Invoke();

        return true;
    }

    public void Remove(Item item)
    {
        _items.Remove(item);
        ItemChanged?.Invoke();
    }
}