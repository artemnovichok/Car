//using Firebase.Analytics;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DonateItemVisual : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _priceText;
    private int _cost;
    private int _change;
    private bool _isDonate;

    public void Init(DonateItem donateItem)
    {
        _isDonate = donateItem.RealToVitual;
        //_icon.sprite = donateItem.Sprite;
        _priceText.text = donateItem.Cost.ToString();
        _cost = donateItem.Cost;
        _change = donateItem.Change;
    }

    public void Buy()
    {
        object dimondObj = SaveData.Instance.GetData("Dimond");
        int dimond = dimondObj == null ? 0 : Convert.ToInt32(dimondObj);
        if (_isDonate)
        {
            //FirebaseAnalytics.LogEvent("bought_diamonds");
            //FirebaseAnalytics.LogEvent("players_bought_diamonds");
            //реализайия донатной покупки
            SaveData.Instance.Save("Dimond", (dimond + _change));
        }
        else
        {
            if(dimond >= _cost)
            {
                SaveData.Instance.Save("Dimond", (dimond - _cost));
                object moneyObj = SaveData.Instance.GetData("Money");
                int money = moneyObj == null ? 0 : Convert.ToInt32(moneyObj);
                SaveData.Instance.Save("Money", money + _change);
            }
        }
    }
}
