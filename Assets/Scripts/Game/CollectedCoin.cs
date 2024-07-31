using System;
using UnityEngine;

public class CollectedCoin : MonoBehaviour
{
    [HideInInspector]
    public int collectedCoinOnLevel = 0;


    private void OnEnable()
    {
        EventManager.Instance.finishLevel.AddListener(SaveCoins);
    }

    private void OnDisable()
    {
        EventManager.Instance.finishLevel.RemoveListener(SaveCoins);
    }

    public void SaveCoins()
    {
        object moneyObj = SaveData.Instance.GetData("Money");
        int money = moneyObj == null ? 0 : Convert.ToInt32(moneyObj);
        int currentMoney = collectedCoinOnLevel + money;
        SaveData.Instance.Save("Money", currentMoney);
    }
}
