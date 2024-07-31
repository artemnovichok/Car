//using Firebase.Analytics;
using SamuraiGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClaimRewards : MonoBehaviour
{
    public static ClaimRewards instance;
    private void Awake()
    {
        instance = this;
    }
    void OnEnable()
    {
        DailyRewards.instance.onClaimPrize += OnClaimPrizeDailyRewards;
    }

    void OnDisable()
    {
        DailyRewards.instance.onClaimPrize -= OnClaimPrizeDailyRewards;
    }

    // this is your integration function. Can be on Start or simply a function to be called
    public void OnClaimPrizeDailyRewards(int day)
    {
        //switch (day) //для аналитики
        //{
        //    case 1:
        //        FirebaseAnalytics.LogEvent("daily_reward_1");
        //        break;
        //    case 3:
        //        FirebaseAnalytics.LogEvent("daily_reward_3");
        //        break;
        //    case 7:
        //        FirebaseAnalytics.LogEvent("daily_reward_7");
        //        break;
        //    case 15:
        //        FirebaseAnalytics.LogEvent("daily_reward_15");
        //        break;
        //    default:
        //        Debug.LogWarning("Неизвестный день для ежедневной награды: " + day);
        //        break;
        //}

        object moneyObj = SaveData.Instance.GetData("Money");
        int money = moneyObj == null ? 0 : Convert.ToInt32(moneyObj);
        object dimondObj = SaveData.Instance.GetData("Dimond");
        int dimond = dimondObj == null ? 0 : Convert.ToInt32(dimondObj);

        switch (day)
        {
            case 1:
                SaveData.Instance.Save("Money", money + 30);
                break;
            case 2:
                SaveData.Instance.Save("Money", money + 90);
                break;
            case 3:
                SaveData.Instance.Save("Dimond", dimond + 5);
                break;
            case 4:
                SaveData.Instance.Save("Money", money + 140);
                break;
            case 5:
                SaveData.Instance.Save("Money", money + 180);
                break;
            case 6:
                SaveData.Instance.Save("Money", money + 240);
                break;
            case 7:
                SaveData.Instance.Save("Dimond", dimond + 10);
                break;
            case 8:
                SaveData.Instance.Save("Money", money + 300);
                break;
            case 9:
                SaveData.Instance.Save("Money", money + 340);
                break;
            case 10:
                SaveData.Instance.Save("Dimond", dimond + 20);
                break;
            case 11:
                SaveData.Instance.Save("Money", money + 400);
                break;
            case 12:
                SaveData.Instance.Save("Money", money + 430);
                break;
            case 13:
                SaveData.Instance.Save("Dimond", dimond + 35);
                break;
            case 14:
                SaveData.Instance.Save("Money", money + 500);
                break;
            case 15:
                ItemHolder.Instance.skins.skin[13].IsUnlock = true;
                ItemHolder.Instance.Save();
                EventManager.Instance.UpdateSkin();
                break;
        }
        EventManager.Instance.UpdateDimond();
        EventManager.Instance.UpdateMoney();

    }
}
