using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        EventManager.Instance.updateCoinTextOnLevel.AddListener(updateText);
    }

    private void OnDisable()
    {
        EventManager.Instance.updateCoinTextOnLevel.RemoveListener(updateText);
    }
    private void Start()
    {
        updateText();
    }

    private void updateText()
    {
        _text.text = FindFirstObjectByType<CollectedCoin>().collectedCoinOnLevel.ToString();
    }
}
