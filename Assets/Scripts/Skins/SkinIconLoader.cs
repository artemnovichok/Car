using UnityEngine;
using UnityEngine.UI;

public class SkinIconLoader : MonoBehaviour
{
    [SerializeField] private Image _playerIcon;
    [SerializeField] private Sprite[] _iconArray; 

    private void Start()
    {
        int currentSkinId = ItemHolder.Instance.skins.CurrentSkinId;
        SetPlayerIcon(currentSkinId);
    }

    private void SetPlayerIcon(int skinId)
    {
        if (skinId >= 0 && skinId < _iconArray.Length)
        {
            _playerIcon.sprite = _iconArray[skinId];
        }
    }
}
