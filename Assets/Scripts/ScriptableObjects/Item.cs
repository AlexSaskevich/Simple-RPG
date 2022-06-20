using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item", order = 51)]

public class Item : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;

    public string Name => _name;
    public Sprite Icon => _icon;

    public virtual void Use()
    {
    }

    public void RemoveFromInventory()
    {
        Inventory.Instance.Remove(this);
    }
}