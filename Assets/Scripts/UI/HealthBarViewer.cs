using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]

public class HealthBarViewer : MonoBehaviour
{
    [SerializeField] private GameObject _healthBarPrefab;
    [SerializeField] private Transform _target;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private float _fillingSpeed = 1f;

    private Transform _healthBar;
    private CharacterStats _characterStats;
    private Image _healthBarSlider;
    private Coroutine _previousTask;

    private void Awake()
    {
        _characterStats = GetComponent<CharacterStats>();
    }

    private void OnEnable()
    {
        _characterStats.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _characterStats.HealthChanged -= OnHealthChanged;
    }

    private void Start()
    {
        _healthBar = Instantiate(_healthBarPrefab, _canvas.transform).transform;
        int fillImageIndex = 1;
        _healthBarSlider = _healthBar.GetChild(fillImageIndex).GetComponent<Image>();
    }

    private void LateUpdate()
    {
        if (_healthBar != null)
        {
            _healthBar.position = _target.position;
        }
    }

    private void OnHealthChanged(int maxHealth, int currentHealth)
    {
        if (_healthBar != null)
        {
            float healthPercent = (float)currentHealth / maxHealth;

            StartChangeFillAmount(healthPercent);
        }
    }

    private void StartChangeFillAmount(float health)
    {
        if (_previousTask != null)
        {
            StopCoroutine(_previousTask);
        }

        _previousTask = StartCoroutine(ChangeFillAmount(health));
    }

    private IEnumerator ChangeFillAmount(float health)
    {
        while (_healthBarSlider.fillAmount != health && _healthBarSlider.fillAmount != 0)
        {
            _healthBarSlider.fillAmount = Mathf.MoveTowards(_healthBarSlider.fillAmount, health, _fillingSpeed * Time.deltaTime);

            yield return null;
        }

        if (_healthBarSlider.fillAmount == 0)
        {
            Destroy(_healthBar.gameObject);
        }
    }
}