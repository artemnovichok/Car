using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BlockCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _counterText;
    private void OnEnable() => EventManager.Instance.updateCounterBlock.AddListener(updateBlockCounter);
    private void OnDisable() => EventManager.Instance.updateCounterBlock.RemoveListener(updateBlockCounter);

    private void Start()
    {
        EventManager.Instance.UpdateCounterBlock(0);
    }

    private void updateBlockCounter(int value)
    {
        int currentPlayerLevel = CurrentLevelManager.Instance.GetCurrentLevel();
        int maxDetails = 0;
        switch (currentPlayerLevel)
        {
            case 0:
                maxDetails = 5;
                break;
            case 1:
                maxDetails = 8;
                break;
            case 2:
                maxDetails = 11;
                break;
            case 3:
                maxDetails = 14;
                break;
            case 4:
                maxDetails = 16;
                break;
            case 5:
                maxDetails = 18;
                break;
            case 6:
                maxDetails = 20;
                break;
            case 7:
                maxDetails = 22;
                break;
            case 8:
                maxDetails = 22;
                break;
            case 9:
                maxDetails = 25;
                break;
            default:
                break;
        }
        DragImage[] dragImages = GameObject.FindObjectsOfType<DragImage>();
        if(value != 0)
        {
            UpdateVisual(dragImages.Length + value, maxDetails);
        }
        else
        {
            UpdateVisual(dragImages.Length, maxDetails);
        }
    }

    private void UpdateVisual(int current, int max)
    {
        _counterText.text = current + "/" + max;
    }    
}
