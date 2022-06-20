using UnityEngine;

[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EnemyAttack))]
[RequireComponent(typeof(EnemyMover))]

public class EnemyDeathHandler : MonoBehaviour
{
    private CharacterStats _enemyStats;
    private EnemyAttack _enemyAttack;
    private EnemyMover _enemyMover;

    private void Awake()
    {
        _enemyStats = GetComponent<EnemyStats>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _enemyMover = GetComponent<EnemyMover>();
    }

    private void OnEnable()
    {
        _enemyStats.Dying += OnDying;
    }

    private void OnDisable()
    {
        _enemyStats.Dying -= OnDying;
    }

    private void OnDying()
    {
        _enemyAttack.enabled = false;
        _enemyMover.enabled = false;

        float timeToDestroying = 2.0f;
        Destroy(gameObject, timeToDestroying);
    }
}