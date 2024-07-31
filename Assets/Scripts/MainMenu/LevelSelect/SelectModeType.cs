using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectModeType : MonoBehaviour
{
    [Header("Спрайты")]
    [SerializeField][Tooltip("Спрайт первого режима")] private Sprite _spriteFirstMode;
    [SerializeField][Tooltip("Спрайт второго режима")] private Sprite _spriteSecondMode;
    [SerializeField][Tooltip("Спрайт третьего режима")] private Sprite _spriteThirdMode;

    [Header("Техническая штука")]
    [SerializeField] private Image _buttonImage;
    [SerializeField] private GameObject _firstMode;
    [SerializeField] private GameObject _secondMode;
    [SerializeField] private GameObject _thirdMode;
    [SerializeField] private TextMeshProUGUI _textModeNum;
    private int _currentMode = 0;

    private void Start()
    {
        _currentMode = 0;
        _textModeNum.text = "1";
        _buttonImage.sprite = _spriteFirstMode;
        _firstMode.SetActive(true);
        _secondMode.SetActive(false);
        _thirdMode.SetActive(false);
    }

    public void NextMode()
    {
        _currentMode++;
        if (_currentMode == 3)
        {
            _currentMode = 0;
        }
        switch (_currentMode)
        {
            case 0:
                _buttonImage.sprite = _spriteFirstMode;
                _firstMode.SetActive(true);
                _secondMode.SetActive(false);
                _thirdMode.SetActive(false);
                _textModeNum.text = "1";
                break;
            case 1:
                _buttonImage.sprite = _spriteSecondMode;
                _firstMode.SetActive(false);
                _secondMode.SetActive(true);
                _thirdMode.SetActive(false);
                _textModeNum.text = "2";
                break;
            case 2:
                _buttonImage.sprite = _spriteThirdMode;
                _firstMode.SetActive(false);
                _secondMode.SetActive(false);
                _thirdMode.SetActive(true);
                _textModeNum.text = "3";
                break;
            default:
                break;
        }
    }
}
