//using Firebase.Analytics;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinSetup : MonoBehaviour
{
    [SerializeField] private TMP_Text _skinName;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Image _imageIcon;
    [SerializeField] private Button _buyButton;
    [SerializeField] private GameObject _iconActive;
    private int _id;
    private bool _isBuy;
    private bool _isDonate;
    private bool _isDonateByMoney;
    private int _cost;
    public void Init(Skin skin)
    {
        _id = skin.Id;
        _isDonate = skin.IsDonate;
        _isDonateByMoney = !skin.IsDimond;
        _isBuy = skin.IsUnlock;
        _cost = skin.Cost;
        if (ItemHolder.Instance.skins.CurrentSkinId == _id)
        {
            _iconActive.SetActive(true);
        }
        _skinName.text = GameLanguage.Instance.GetLanguage() == "ru" ? skin.NameRu : GameLanguage.Instance.GetLanguage() == "en" ? skin.NameEn : skin.NameTr;
        _imageIcon.sprite = Resources.Load<Sprite>("Skins/Icons/" + (_id + 1));
        _priceText.text = _isBuy == true ? GameLanguage.Instance.GetLanguage() == "ru" ? "Куплено" : GameLanguage.Instance.GetLanguage() == "en" ? "Purchased" : "Satın alındı" : _cost.ToString();
        _buyButton.gameObject.SetActive(!_isBuy);
    }

    public void Buy()
    {
        if (_isDonate)
        {
            if (_isDonateByMoney)
            {
                ProcessDonationPurchase();
            }
            else
            {
                object dimondObj = SaveData.Instance.GetData("Dimond");
                int dimond = dimondObj == null ? 0 : Convert.ToInt32(dimondObj);
                if (dimond >= _cost)
                {
                    dimond -= _cost;
                    SaveData.Instance.Save("Dimond", dimond);
                    ItemHolder.Instance.skins.skin[_id].IsUnlock = true;
                    ItemHolder.Instance.Save();
                    Select();
                    //FirebaseAnalytics.LogEvent("bought_skin_by_diamonds");
                    EventManager.Instance.UpdateSkin();
                }
            }
        }
        else
        {
            object moneyObj = SaveData.Instance.GetData("Money");
            int money = moneyObj == null ? 0 : Convert.ToInt32(moneyObj);
            if (money >= _cost)
            {
                money -= _cost;
                SaveData.Instance.Save("Money", money);
                ItemHolder.Instance.skins.skin[_id].IsUnlock = true;
                ItemHolder.Instance.Save();
                Select();
                //FirebaseAnalytics.LogEvent("bought_skin_by_coins");
                EventManager.Instance.UpdateSkin();
            }
        }
    }

    public void Select()
    {
        if (_isBuy && ItemHolder.Instance.skins.CurrentSkinId != _id)
        {
            _iconActive.SetActive(true);
            ItemHolder.Instance.skins.CurrentSkinId = _id;
            ItemHolder.Instance.Save();
            EventManager.Instance.UpdateSkin();
            Debug.Log("ID SKIN: " + _id);
        }
    }

    private void ProcessDonationPurchase()
    {
        ItemHolder.Instance.skins.skin[_id].IsUnlock = true;
        ItemHolder.Instance.Save();
        Select();
        //FirebaseAnalytics.LogEvent("bought_skin_by_money");
        //FirebaseAnalytics.LogEvent("players_bought_donat_skin");
        EventManager.Instance.UpdateSkin();
    }

    public int GetID()
    {
        return _id;
    }
}
