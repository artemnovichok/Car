using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [Header(" нопки на панели")]
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(Resume);
        _restartButton.onClick.AddListener(Restart);
        _exitButton.onClick.AddListener(Exit);
    }

    private void OnDisable()
    {
        _resumeButton.onClick?.RemoveListener(Resume);
        _restartButton.onClick?.RemoveListener(Restart);
        _exitButton.onClick?.RemoveListener(Exit);
    }

    private void Resume()
    {
        PauseManager.Instance.UnPause();
        Destroy(gameObject);
    }

    private void Restart()
    {
        PauseManager.Instance.UnPause();
        SceneManager.LoadScene(2);
    }

    private void Exit()
    {
        PauseManager.Instance.UnPause();
        SceneManager.LoadScene(0);//главное меню
    }
}
