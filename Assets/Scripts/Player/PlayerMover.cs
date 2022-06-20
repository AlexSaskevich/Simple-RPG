using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(MouseHandler))]

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _rotationSpeed;

    private Vector3 _moveVector;
    private PlayerAnimator _animator;
    private InputHandler _inputHandler;
    private MouseHandler _mouseHandler;
    private Coroutine _previousTask;

    public event UnityAction<float> Moved;
    public event UnityAction<string> TurnedToItem;

    public float WalkSpeed => _walkSpeed;
    public float RunSpeed => _runSpeed;

    private void Awake()
    {
        _animator = GetComponent<PlayerAnimator>();
        _inputHandler = GetComponent<InputHandler>();
        _mouseHandler = GetComponent<MouseHandler>();
    }

    private void OnEnable()
    {
        _mouseHandler.ItemHit += OnHit;
    }

    private void OnDisable()
    {
        _mouseHandler.ItemHit -= OnHit;
    }

    private void Update()
    {
        _moveVector = new Vector3(_inputHandler.InputVector.x, 0, _inputHandler.InputVector.y);

        Rotate();

        if (_animator.IsAttackAnimationPlaying || _animator.IsTakeAnimationPlaying)
        {
            StopMove();
            return;
        }

        if (_moveVector == Vector3.zero)
        {
            Moved?.Invoke(0);
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Move(_runSpeed);
            return;
        }

        Move(_walkSpeed);
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up, Angle360(transform.forward, _moveVector, transform.right));
    }

    private float Angle360(Vector3 from, Vector3 to, Vector3 right)
    {
        float angle = Vector3.Angle(from, to);

        return (Vector3.Angle(right, to) > 90.0f) ? 360.0f - angle : angle;
    }

    private void Move(float moveSpeed)
    {
        Moved?.Invoke(moveSpeed);
        transform.Translate(_moveVector.normalized * moveSpeed * Time.deltaTime, Space.World);
    }

    private void StopMove()
    {
        _moveVector = Vector3.zero;
    }

    private void OnHit(MouseHandler mouseHandler, RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out ItemPicker itemPicker))
        {
            float distanceToItem = Vector3.Distance(transform.position, itemPicker.transform.position);

            if (distanceToItem <= itemPicker.Radius)
            {
                StartRotateToTarget(itemPicker.transform);
            }

            StopCoroutine(RotateToTarget(itemPicker.transform));
        }

        if (hit.collider.TryGetComponent(out Chest chest))
        {
            float distanceToItem = Vector3.Distance(transform.position, chest.transform.position);

            if (distanceToItem <= chest.Radius)
            {
                StartRotateToTarget(chest.transform);
            }

            StopCoroutine(RotateToTarget(chest.transform));
        }
    }

    private void StartRotateToTarget(Transform hitItemTransform)
    {
        if (hitItemTransform == null)
        {
            return;
        }

        if (_previousTask != null)
        {
            StopCoroutine(RotateToTarget(hitItemTransform));
        }

        _previousTask = StartCoroutine(RotateToTarget(hitItemTransform));
    }

    private IEnumerator RotateToTarget(Transform hitItemTransform)
    {
        Vector3 direction = (hitItemTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        float delta = 5.0f;

        while (Quaternion.Angle(transform.rotation, lookRotation) >= delta)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);

            yield return null;
        }

        if (hitItemTransform.TryGetComponent(out ItemPicker itemPicker))
        {
            TurnedToItem?.Invoke(itemPicker.Item.Name);
        }

        if (hitItemTransform.TryGetComponent(out Chest chest))
        {
            TurnedToItem?.Invoke(chest.Item.Name);
        }
    }
}