using UnityEngine;
using UnityEngine.UI;

public class OpenAndCloseSettings : MonoBehaviour
{
    private enum State
    {
        Open,
        Close
    }

    [SerializeField]private State state = State.Open;

    [SerializeField] private GameObject currentPanel;

    private void Start()
    {
        if (gameObject.TryGetComponent<Button>(out Button btn))
        {
            btn.onClick.AddListener(state == State.Open ? OpenPanel : ClosePanel);
        }
        else
        {
            gameObject.AddComponent<Button>();
            Start();
        }
    }

    private void OpenPanel()
    {
        if (!PanelManager.Instance.TryOpenSettings(out GameObject openedPanel))
        {
            Debug.LogError("Не удалось открыть панель настроек.");
        }
        else
        {
            Time.timeScale = 0f;
            openedPanel.SetActive(true);
        }
    }

    private void ClosePanel()
    {
        Time.timeScale = 1f;
        Destroy(currentPanel);
    }
}
