using TMPro;
using UnityEngine;

public class TextLocolize : MonoBehaviour
{
    
    [Header("Settings")]
    [Tooltip("Не работает при включенном AutoSize")]
    [SerializeField] private bool _oneTextSize;
    [SerializeField] private float _oneSize;

    [Space(15)]

    [Header("Set Lang")]

    [SerializeField]
    [TextArea]
    private string _textRu;
    [SerializeField] private int _textSizeRu;

    [Space(5)]

    [SerializeField]
    [TextArea]
    private string _textEn;
    [SerializeField] private int _textSizeEn;

    [Space(5)]

    [SerializeField]
    [TextArea]
    private string _textTr;
    [SerializeField] private int _textSizeTr;


    private void Start()
    {
        if (gameObject.TryGetComponent<TMP_Text>(out TMP_Text tmpText))
        {
            _oneSize = _oneSize == 0 ? tmpText.fontSize : _oneSize;
            if (tmpText.enableAutoSizing)
            {
                switch (GameLanguage.Instance.GetLanguage())
                {
                    case "ru":
                        tmpText.text = _textRu;
                        break;
                    case "en":
                        tmpText.text = _textEn;
                        break;
                    case "tr":
                        tmpText.text = _textTr;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (GameLanguage.Instance.GetLanguage())
                {
                    case "ru":
                        tmpText.text = _textRu;
                        tmpText.fontSize = _oneTextSize == true ? _oneSize : _textSizeRu ;
                        break;
                    case "en":
                        tmpText.text = _textEn;
                        tmpText.fontSize = _oneTextSize == true ? _oneSize : _textSizeEn;
                        break;
                    case "tr":
                        tmpText.text = _textTr;
                        tmpText.fontSize = _oneTextSize == true ? _oneSize : _textSizeTr;
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            gameObject.GetComponent<TextMeshPro>();
            Debug.LogWarning("Не нашел компонент TextMeshPro, добавляю свой...   (" + gameObject.name + ")");
            Start();
        }
    }
}
