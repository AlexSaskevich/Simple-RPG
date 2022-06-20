using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    private static bool _isPaused = false;

    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button _button;
    [SerializeField] private CanvasGroup _inventory;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                UnPause();
            }
            else
            {
                Pause();
            }
        }
    }

    private void OnButtonClick()
    {
        if (_isPaused)
        {
            UnPause();
        }
        else
        {
            Pause();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        _canvasGroup.alpha = 1;
        _isPaused = true;

        _inventory.interactable = false;
    }

    private void UnPause()
    {
        Time.timeScale = 1;
        _canvasGroup.alpha = 0;
        _isPaused = false;

        _inventory.interactable = true;
    }
}