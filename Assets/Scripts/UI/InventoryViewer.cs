using UnityEngine;

public class InventoryViewer : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Transform _itemsParent;

    private InventorySlot[] _slots;

    private void OnEnable()
    {
        _inventory.ItemChanged += UpdateView;
    }

    private void OnDisable()
    {
        _inventory.ItemChanged -= UpdateView;
    }

    private void Start()
    {
        _inventory = Inventory.Instance;
        _slots = _itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    private void UpdateView()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (i < _inventory.Items.Count)
            {
                _slots[i].AddItem(_inventory.Items[i]);
            }
            else
            {
                _slots[i].ClearSlot();
            }
        }
    }
}