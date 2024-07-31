using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private int SceneId;
    public void SceneToLoad()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneId);
    }
}
