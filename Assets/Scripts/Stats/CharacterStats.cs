using UnityEngine;
using UnityEngine.Events;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] public Stat Armor;
    [SerializeField] public Stat Damage;

    [SerializeField] private int _maxHealth = 100;

    private int _currentHealth;
    private int _minHealth = 0;

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction Dying;
    public event UnityAction<int> Damaged;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;
    public Stat damageeee => Damage;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        damage -= Armor.FinalValue;
        _currentHealth -= Mathf.Clamp(damage, _minHealth, _maxHealth);

        HealthChanged?.Invoke(_maxHealth, _currentHealth);
        Damaged?.Invoke(damage);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        _currentHealth += Mathf.Clamp(healAmount, _minHealth, MaxHealth);

        HealthChanged?.Invoke(_maxHealth, _currentHealth);
    }

    public virtual void Die()
    {
        Dying?.Invoke();
    }
}