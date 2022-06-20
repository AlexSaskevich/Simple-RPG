using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyStats))]

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _attackSpeed;
    [SerializeField] private PlayerStats _playerStats;

    private EnemyMover _enemyMover;
    private EnemyStats _enemyStats;
    private float _attackCooldown = 0;

    private void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
        _enemyStats = GetComponent<EnemyStats>();
    }

    private void Update()
    {
        _attackCooldown -= Time.deltaTime;
    }

    private void OnEnable()
    {
        _enemyMover.Attacked += OnAttacked;
    }

    private void OnDisable()
    {
        _enemyMover.Attacked -= OnAttacked;
    }

    private void OnAttacked()
    {
        if (_attackCooldown <= 0)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _playerStats.TakeDamage(_enemyStats.Damage.FinalValue);

        _attackCooldown = 1f / _attackSpeed;
    }
}