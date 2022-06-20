using UnityEngine;
using TMPro;

public class FloatingTextViewer : MonoBehaviour
{
    [SerializeField] private TextMeshPro _floatingText;

    private CharacterStats _characterStats;

    private void Awake()
    {
        _characterStats = GetComponent<CharacterStats>();
    }

    private void OnEnable()
    {
        _characterStats.Damaged += OnDamaged;
    }

    private void OnDisable()
    {
        _characterStats.Damaged -= OnDamaged;
    }

    private void OnDamaged(int finalDamage)
    {
        var floatingText = Instantiate(_floatingText, transform.position, Quaternion.identity, transform);

        floatingText.GetComponent<TextMeshPro>().text = finalDamage.ToString();
    }
}