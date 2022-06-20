using UnityEngine;
using UnityEngine.Events;

public class Chest : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private float _radius;

    public event UnityAction Opened;

    public float Radius => _radius;
    public Item Item => _item;

    private void OnEnable()
    {
        _playerMover.TurnedToItem += OnTook;
    }

    private void OnDisable()
    {
        _playerMover.TurnedToItem -= OnTook;
    }

    private void OnTook(string hitItemName)
    {
        if (hitItemName != _item.Name)
        {
            return;
        }

        if (Inventory.Instance.TryAdd(_item) == false)
        {
            return;
        }

        Opened?.Invoke();
        float timeToDestroying = 2.0f;
        Destroy(gameObject, timeToDestroying);
    }
}