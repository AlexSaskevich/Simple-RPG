using UnityEngine;

[RequireComponent(typeof(Chest))]
[RequireComponent(typeof(Animator))]

public class ChestAnimator : MonoBehaviour
{
    private const string Open = "Open";

    private Chest _chest;
    private Animator _animator;

    private void Awake()
    {
        _chest = GetComponent<Chest>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _chest.Opened += OnOpened;
    }

    private void OnDisable()
    {
        _chest.Opened -= OnOpened;
    }

    private void OnOpened()
    {
        _animator.SetTrigger(Open);
    }
}