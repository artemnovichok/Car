using UnityEngine;

public class GameOver : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.Instance.gameOver.AddListener(showPanel);
    }

    private void OnDisable()
    {
        EventManager.Instance.gameOver.RemoveListener(showPanel);
    }

    private void showPanel()
    {
        PlayerPrefs.SetInt("TimesFailed", PlayerPrefs.GetInt("TimesFailed", 0) + 1);//внутреннее сохранение, не удалять
        if (PlayerPrefs.GetInt("TimesFailed") == 5) 
        {
            
        }
        PanelManager.Instance.TryOpenGameOverPanel(out GameObject go);
    }
}
