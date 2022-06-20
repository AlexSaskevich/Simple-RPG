using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(EquipmentMeshSetter))]

public class PlayerDeathHandler : MonoBehaviour
{
    private CharacterStats _playerStats;
    private PlayerMover _playerMover;
    private PlayerAttack _playerAttack;
    private PlayerAnimator _playerAnimator;
    private EquipmentMeshSetter _equipmentMeshSetter;

    private void Awake()
    {
        _playerStats = GetComponent<CharacterStats>();
        _playerMover = GetComponent<PlayerMover>();
        _playerAttack = GetComponent<PlayerAttack>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _equipmentMeshSetter = GetComponent<EquipmentMeshSetter>();
    }

    private void OnEnable()
    {
        _playerStats.Dying += OnDying;
    }

    private void OnDisable()
    {
        _playerStats.Dying -= OnDying;
    }

    private void OnDying()
    {
        _playerMover.enabled = false;
        _playerAttack.enabled = false;
        _playerAnimator.enabled = false;
        _equipmentMeshSetter.enabled = false;

        float timeToDestroying = 2.0f;
        Destroy(gameObject, timeToDestroying);
    }
}