using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private float _radius;

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

        Destroy(gameObject);
    }
}