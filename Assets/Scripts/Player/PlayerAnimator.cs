using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MouseHandler))]
[RequireComponent(typeof(PlayerStats))]

public class PlayerAnimator : MonoBehaviour
{
    private const string IsWalking = "IsWalking";
    private const string IsRunning = "IsRunning";
    private const string Attack = "Attack";
    private const string Take = "Take";
    private const string Die = "Die";

    private PlayerMover _playerMover;
    private Animator _animator;
    private MouseHandler _mouseHandler;
    private PlayerStats _playerStats;

    public bool IsAttackAnimationPlaying
    {
        get
        {
            AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            return animatorStateInfo.IsName(Attack);
        }
    }

    public bool IsTakeAnimationPlaying
    {
        get
        {
            AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            return animatorStateInfo.IsName(Take);
        }
    }

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _animator = GetComponent<Animator>();
        _mouseHandler = GetComponent<MouseHandler>();
        _playerStats = GetComponent<PlayerStats>();
    }

    private void OnEnable()
    {
        _playerMover.Moved += OnMoved;
        _mouseHandler.AttackButtonClicked += OnAttacked;
        _playerMover.TurnedToItem += OnTook;
        _playerStats.Dying += OnDying;
    }

    private void OnDisable()
    {
        _playerMover.Moved -= OnMoved;
        _mouseHandler.AttackButtonClicked -= OnAttacked;
        _playerMover.TurnedToItem -= OnTook;
        _playerStats.Dying -= OnDying;
    }

    private void OnTook(string itemName)
    {
        _animator.SetTrigger(Take);
    }

    private void OnAttacked()
    {
        _animator.SetTrigger(Attack);
    }

    private void OnMoved(float moveSpeed)
    {
        if (moveSpeed == _playerMover.WalkSpeed)
        {
            _animator.SetBool(IsWalking, true);
            _animator.SetBool(IsRunning, false);
        }
        else if (moveSpeed == _playerMover.RunSpeed)
        {
            _animator.SetBool(IsRunning, true);
            _animator.SetBool(IsWalking, false);
        }
        else
        {
            _animator.SetBool(IsWalking, false);
            _animator.SetBool(IsRunning, false);
        }
    }

    private void OnDying()
    {
        _animator.SetTrigger(Die);
    }
}