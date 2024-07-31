//using Firebase.Analytics;
using System;
using UnityEngine;

public class CurrentLevelManager : MonoBehaviour
{
    public static CurrentLevelManager Instance { get; private set; }

    private int curLevel;



    private void Awake()
    {
        curLevel = 9; //убрать перед релизом
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        object starsData = SaveData.Instance.GetData("CurrentNumOfStar");
        int currentStars = starsData == null ? 0 : Convert.ToInt32(starsData);
        //curLevel = (int)Mathf.Floor(currentStars / 30f); //раскоментировать перед релизом

        object lvlToAnalData = SaveData.Instance.GetData("lvlToAnalitic" + curLevel);
        if (lvlToAnalData == null)
        {
            //FirebaseAnalytics.LogEvent("players_level_up", new Parameter("level", curLevel));
            SaveData.Instance.Save("lvlToAnalitic" + curLevel, true);
        }
    }

    public int GetCurrentLevel()
    {
        return curLevel;
    }
}
