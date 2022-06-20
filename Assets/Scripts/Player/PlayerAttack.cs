using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(MouseHandler))]

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _attackRange = 3.0f;

    private CharacterStats _playerStats;
    private MouseHandler _mouseHandler;

    private void Awake()
    {
        _playerStats = GetComponent<CharacterStats>();
        _mouseHandler = GetComponent<MouseHandler>();
    }

    private void OnEnable()
    {
        _mouseHandler.EnemyHit += OnEnemyHit;
    }

    private void OnDisable()
    {
        _mouseHandler.EnemyHit -= OnEnemyHit;
    }

    private void OnEnemyHit(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out CharacterStats enemyStats))
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemyStats.transform.position);

            if (distanceToEnemy <= _attackRange)
            {
                StartCoroutine(RotateToTarget(enemyStats));
                Attack(enemyStats);
            }
        }
    }

    private void Attack(CharacterStats enemyStats)
    {
        enemyStats.TakeDamage(_playerStats.Damage.FinalValue);
    }

    private IEnumerator RotateToTarget(CharacterStats enemyStats)
    {
        Vector3 direction = (enemyStats.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        float rotationSpeed = 10;
        float delta = 5.0f;

        while (Quaternion.Angle(transform.rotation, lookRotation) >= delta && enemyStats != null)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

            yield return null;
        }
    }
}