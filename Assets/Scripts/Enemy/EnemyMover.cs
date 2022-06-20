using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _lookRadius = 10f;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private float _rotationSpeed = 5f;

    private NavMeshAgent _navMeshAgent;

    public event UnityAction FoundTarget;
    public event UnityAction MissedTarget;
    public event UnityAction Attacked;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_playerStats == null)
        {
            return;
        }

        float distance = Vector3.Distance(_playerStats.transform.position, transform.position);

        if (distance <= _lookRadius)
        {
            FoundTarget?.Invoke();

            _navMeshAgent.SetDestination(_playerStats.transform.position);

            if (distance <= _navMeshAgent.stoppingDistance)
            {
                RotateToTarget();
                Attacked?.Invoke();
            }
        }

        if (_navMeshAgent.velocity == Vector3.zero)
        {
            MissedTarget?.Invoke();
        }
    }

    private void RotateToTarget()
    {
        Vector3 direction = (_playerStats.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);
    }
}