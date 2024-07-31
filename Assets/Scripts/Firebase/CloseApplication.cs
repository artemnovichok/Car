//using Firebase.Analytics;
using UnityEngine;

public class CloseApplication : MonoBehaviour
{
    public static CloseApplication Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnApplicationQuit()
    {
        //FirebaseAnalytics.LogEvent("finished_adv_and_exit");
    }
}
