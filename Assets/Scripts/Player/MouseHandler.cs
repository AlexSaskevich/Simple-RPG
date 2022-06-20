using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerAnimator))]

public class MouseHandler : MonoBehaviour
{
    private Camera _camera;
    private PlayerAnimator _playerAnimator;

    public event UnityAction AttackButtonClicked;
    public event UnityAction<MouseHandler, RaycastHit> ItemHit;
    public event UnityAction<RaycastHit> EnemyHit;

    private void Awake()
    {
        _camera = Camera.main;
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        if ((Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0)) && _playerAnimator.IsAttackAnimationPlaying == false)
        {
            GetMouseCameraRay();
        }
    }

    private void GetMouseCameraRay()
    {
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out ItemPicker itemPicker) || hit.collider.TryGetComponent(out Chest chest))
            {
                ItemHit?.Invoke(this, hit);
            }

            if (hit.collider.TryGetComponent(out Enemy enemy))
            {
                EnemyHit?.Invoke(hit);
                AttackButtonClicked?.Invoke();
            }
        }
    }
}