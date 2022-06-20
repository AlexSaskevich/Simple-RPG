using UnityEngine;
using UnityEngine.Events;

public class PlayerEquipment : MonoBehaviour
{
    public static PlayerEquipment Instance;

    private Equipment[] _currentEquipment;

    public event UnityAction<Equipment, Equipment> EquipmentChanged;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        int slotCount = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        _currentEquipment = new Equipment[slotCount];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.EquipmentSlot;

        Equipment oldItem = null;

        if (_currentEquipment[slotIndex] != null)
        {
            oldItem = _currentEquipment[slotIndex];
            Inventory.Instance.TryAdd(oldItem);
        }

        EquipmentChanged?.Invoke(newItem, oldItem);

        _currentEquipment[slotIndex] = newItem;
    }
}