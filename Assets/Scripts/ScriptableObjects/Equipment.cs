using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment", order = 51)]

public class Equipment : Item
{
    [SerializeField] private EquipmentSlot _equipmentSlot;
    [SerializeField] private int _damageModifier;
    [SerializeField] private int _armorModifier;

    public EquipmentSlot EquipmentSlot => _equipmentSlot;
    public int DamageModifier => _damageModifier;
    public int ArmorModifier => _armorModifier;

    public override void Use()
    {
        base.Use();

        PlayerEquipment.Instance.Equip(this);
        RemoveFromInventory();
    }
}

public enum EquipmentSlot
{
    Head,
    Body,
    Weapon,
}