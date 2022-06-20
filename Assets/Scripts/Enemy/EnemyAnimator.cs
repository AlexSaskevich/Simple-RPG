using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyStats))]

public class EnemyAnimator : MonoBehaviour
{
    private const string IsWalking = "IsWalking";
    private const string IsAttacking = "IsAttacking";
    private const string Die = "Die";

    private EnemyMover _enemyMover;
    private Animator _animator;
    private EnemyStats _enemyStats;

    private void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
        _animator = GetComponent<Animator>();
        _enemyStats = GetComponent<EnemyStats>();
    }

    private void OnEnable()
    {
        _enemyMover.FoundTarget += OnFoundTarget;
        _enemyMover.Attacked += OnAttacked;
        _enemyMover.MissedTarget += OnMissedTarget;
        _enemyStats.Dying += OnDying;
    }

    private void OnDisable()
    {
        _enemyMover.FoundTarget -= OnFoundTarget;
        _enemyMover.Attacked -= OnAttacked;
        _enemyMover.MissedTarget -= OnMissedTarget;
        _enemyStats.Dying -= OnDying;
    }

    private void OnDying()
    {
        _animator.SetTrigger(Die);
    }

    private void OnAttacked()
    {
        _animator.SetBool(IsWalking, false);
        _animator.SetBool(IsAttacking, true);
    }

    private void OnMissedTarget()
    {
        _animator.SetBool(IsAttacking, false);
        _animator.SetBool(IsWalking, false);
    }

    private void OnFoundTarget()
    {
        _animator.SetBool(IsAttacking, false);
        _animator.SetBool(IsWalking, true);
    }
}