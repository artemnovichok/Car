using System;
using TMPro;
using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _textMoney;

    private void OnDisable() => EventManager.Instance.updateMoney.RemoveListener(updateText);

    private void Start()
    {
        EventManager.Instance.updateMoney.AddListener(updateText);
        updateText();
    }

    private void updateText()
    {
        object moneyObj = SaveData.Instance.GetData("Money");
        int money = moneyObj == null ? 0 : Convert.ToInt32(moneyObj);
        _textMoney.text = money.ToString();
    }
}
