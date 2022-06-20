using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Pickup/Potion", order = 51)]

public class Potion : Item
{
    [SerializeField] private int _healModifier;

    public int HealModifier => _healModifier;
}