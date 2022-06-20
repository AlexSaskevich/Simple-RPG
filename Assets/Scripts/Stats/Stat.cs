using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Stat
{
    [SerializeField] private int _defaultValue;

    private List<int> _modifiers = new List<int>();

    public int FinalValue
    {
        get
        {
            int finalValue = _defaultValue;

            _modifiers.ForEach(modifier => finalValue += modifier);

            return finalValue;
        }
    }

    public void AddModifier(int modifier)
    {
        if (modifier != 0)
        {
            _modifiers.Add(modifier);
        }
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
        {
            _modifiers.Remove(modifier);
        }
    }
}