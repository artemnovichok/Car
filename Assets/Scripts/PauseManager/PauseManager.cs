using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

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
    }

    public void OnPause()
    {
        if (!PanelManager.Instance.TryOpenPause(out GameObject openedPanel))
        {
            Debug.LogError("Не удалось открыть панель паузы.");
        }
        else
        {
            Time.timeScale = 0f;
            openedPanel.SetActive(true);
        }
    }

    public void UnPause()
    {
        Time.timeScale = 1f;
    }
}
