using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private Potion _potion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStats playerStats))
        {
            TryAddHealth(playerStats);
        }
    }

    private void TryAddHealth(PlayerStats playerStats)
    {
        if (playerStats.CurrentHealth == playerStats.MaxHealth)
        {
            return;
        }

        playerStats.Heal(_potion.HealModifier);
        Destroy(gameObject);
    }
}