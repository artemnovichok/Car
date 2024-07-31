using UnityEngine;

public class GameLanguage : MonoBehaviour
{
    private string currentLang;

    private static GameLanguage instance;

    public static GameLanguage Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameLanguage>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameLanguage");
                    instance = obj.AddComponent<GameLanguage>();
                }
            }
            return instance;
        }
    }

    

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        //для теста. Реализовать логику получения ящыка от разных сдк
        currentLang = "ru";
    }

    public string GetLanguage()
    {
        return currentLang;
    }
}
