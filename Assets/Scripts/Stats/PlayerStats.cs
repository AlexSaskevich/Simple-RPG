using UnityEngine;

public class PlayerStats : CharacterStats
{
    private void Start()
    {
        PlayerEquipment.Instance.EquipmentChanged += OnEquipmentChanged;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

    private void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem == null || oldItem == null)
        {
            return;
        }

        Armor.AddModifier(newItem.ArmorModifier);
        Damage.AddModifier(newItem.DamageModifier);

        Armor.RemoveModifier(oldItem.ArmorModifier);
        Damage.RemoveModifier(oldItem.DamageModifier);
    }
}