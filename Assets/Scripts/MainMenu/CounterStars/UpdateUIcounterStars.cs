using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUIcounterStars : MonoBehaviour
{
    [Header("Íàâåäèñü è ïîëó÷è èíôîðìàöèþ(äëÿ äèçàéíåðîâ)")]
    [Tooltip("ýòî ñëàéäåð(ïîëîñêà), êîòîðàÿ îòîáðàæàåò ïðîãðåññ èãðîêà äî ñë óðîâíÿ")]
    [SerializeField] private Slider _slider;
    [Tooltip("Òåêñò, îòîáðàæàþùèé êîëëè÷åñòâî çâåçä äî ïðîøëîãî óðîâíÿ")]
    [SerializeField] private TMP_Text _lastStarsLevel_Text;
    [Tooltip("Òåêñò, îòîáðàæàþùèé êîëëè÷åñòâî çâåçä äî ñë óðîâíÿ")]
    [SerializeField] private TMP_Text _starsToNextLevell_Text;
    [Tooltip("Òåêñò, îòîáðàæàþùèé êîëëè÷åñòâî çâåçä(òåêóùèõ)")]
    [SerializeField] private TMP_Text _currentStars_Text;
    [Tooltip("Òåêñò, îòîáðàæàþùèé òåêóùèé óðîâåíü")]
    [SerializeField] private TMP_Text _currentLevel_Text;
    private int starsToNextLevel;
    private int lastStarsLevel;

    private void Start()
    {
        int currentStars = 0;
        object starsData = SaveData.Instance.GetData("CurrentNumOfStar");
        currentStars = starsData == null ? 0 : Convert.ToInt32(starsData);
        updateUI(currentStars);
    }

    private void updateUI(int amountStars)
    {
        int currentPlayerLevel = CurrentLevelManager.Instance.GetCurrentLevel();
        string levelText = $"{getLocolizeLevelText()}{currentPlayerLevel}";
        _currentLevel_Text.text = levelText;
        if (currentPlayerLevel == 0)
        {
            starsToNextLevel = 30;
            lastStarsLevel = 0;
        }
        else
        {
            starsToNextLevel = currentPlayerLevel * 30;
            lastStarsLevel = starsToNextLevel - 30;
        }
        _currentStars_Text.text = amountStars.ToString();
        _lastStarsLevel_Text.text = lastStarsLevel.ToString();
        _starsToNextLevell_Text.text = starsToNextLevel.ToString();
        float percent = 0;
        if (starsToNextLevel != lastStarsLevel)
        {
            percent = (amountStars - lastStarsLevel) / (starsToNextLevel - lastStarsLevel);
        }
        _slider.value = percent;
    }

    private string getLocolizeLevelText()
    {
        string text;
        switch(GameLanguage.Instance.GetLanguage())
        {
            case "ru":
                text = "Уровень: ";
                break;
            case "en":
                text = "Level: ";
                break;
            case "tr":
                text = "Seviye: ";
                break;
                default:
                text = "îøèáêà ïîëó÷åíèÿ ÿçûêà";
                break;
        }
        return text;
    }
}
