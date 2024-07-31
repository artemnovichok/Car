using System;
using UnityEngine;

public class TestAddMoney : MonoBehaviour
{
    public void AddMoney()
    {
        object moneyObj = SaveData.Instance.GetData("Money");
        int money = moneyObj == null ? 0 : Convert.ToInt32(moneyObj);
        SaveData.Instance.Save("Money", money + 100);
        EventManager.Instance.UpdateMoney();
    }
}
