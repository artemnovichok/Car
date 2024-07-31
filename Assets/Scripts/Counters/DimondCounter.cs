using System;
using TMPro;
using UnityEngine;

public class DimondCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _textDimond;

    private void OnDisable() => EventManager.Instance.updateDimond.RemoveListener(updateText);

    private void Start()
    {
        EventManager.Instance.updateDimond.AddListener(updateText);
        updateText();
    }

    private void updateText()
    {
        object dimondObj = SaveData.Instance.GetData("Dimond");
        int dimond = dimondObj == null ? 0 : Convert.ToInt32(dimondObj);
        _textDimond.text = dimond.ToString();
    }
}
