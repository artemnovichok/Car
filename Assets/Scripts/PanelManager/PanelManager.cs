using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance { get; private set; }

    private GameObject _settingPanel;

    private GameObject _pausePanel;

    private GameObject _openFinishPanel;

    private GameObject _gameOverPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _settingPanel = Resources.Load<GameObject>("Panels/SettingsPanel(Image)");
        _pausePanel = Resources.Load<GameObject>("Panels/PausePanel(Image)");
        _openFinishPanel = Resources.Load<GameObject>("Panels/WinPanel(Image)");
        _gameOverPanel = Resources.Load<GameObject>("Panels/GameOver(Image)");
        

    }

    public bool TryOpenSettings(out GameObject openedPanel)
    {
        openedPanel = null;
        if (_settingPanel != null)
        {
            openedPanel = Instantiate(_settingPanel, GameObject.Find("Canvas").gameObject.transform);
            return true;
        }
        else
        {
            Debug.LogError("Не нашел SettingsPanel(Image) в ресурсах (папка Panels)");
            return false;
        }
    }

    public bool TryOpenPause(out GameObject openedPanel)
    {
        openedPanel = null;
        if (_pausePanel != null)
        {
            openedPanel = Instantiate(_pausePanel, GameObject.Find("Canvas").gameObject.transform);
            return true;
        }
        else
        {
            Debug.LogError("Не нашел PausePanel(Image) в ресурсах (папка Panels)");
            return false;
        }
    }

    public bool TryOpenFinishPanel(out GameObject openedPanel)
    {
        openedPanel = null;
        if (_openFinishPanel != null)
        {
            openedPanel = Instantiate(_openFinishPanel, GameObject.Find("Canvas").gameObject.transform);
            return true;
        }
        else
        {
            Debug.LogError("Не нашел WinPanel(Image) в ресурсах (папка Panels)");
            return false;
        }

    }

    public bool TryOpenGameOverPanel(out GameObject openedPanel)
    {
        openedPanel = null;
        if (_gameOverPanel != null)
        {
            openedPanel = Instantiate(_gameOverPanel, GameObject.Find("Canvas").gameObject.transform);
            return true;
        }
        else
        {
            Debug.LogError("Не нашел GameOver(Image) в ресурсах (папка Panels)");
            return false;
        }

    }
}
