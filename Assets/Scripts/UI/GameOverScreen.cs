using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]

public class GameOverScreen : MonoBehaviour
{
    private const string Game = "Game";
    private const string MainMenu = "MainMenu";

    [SerializeField] private PlayerStats _playerStats;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        _playerStats.Dying += OnDying;
    }

    private void OnDisable()
    {
        _playerStats.Dying -= OnDying;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(Game);
    }

    public void ShowMainMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }

    private void OnDying()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
    }
}