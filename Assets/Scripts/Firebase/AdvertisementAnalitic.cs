//using Firebase.Analytics;
using UnityEngine;

public class AdvertisementAnalitic : MonoBehaviour
{
    public static AdvertisementAnalitic Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
        
    public void WatchRewardAdv()
    {
        //FirebaseAnalytics.LogEvent("watched_reward_adv");
    }

    public void AdvStart()
    {
        //FirebaseAnalytics.LogEvent("adv_started");
    }

    public void FinishAdvAndReturn()
    {
        //FirebaseAnalytics.LogEvent("finished_adv_and_return");
    }

    public void WatchAdvInFinishMenu()
    {
        //FirebaseAnalytics.LogEvent("watched_reward_adv_level");
    }
}
