using UnityEngine;

public class CellSetup : MonoBehaviour
{
    public CellSettingHolder cellSettingHolder = new CellSettingHolder();
    public void WriteOurCoordinates()
    {
        if (gameObject.TryGetComponent<RectTransform>(out var rect))
        {
            cellSettingHolder.CellPosX = rect.anchoredPosition.x;
            cellSettingHolder.CellPosY = rect.anchoredPosition.y;
        }    
        else
        {
            print("Нет ректа");
        }
    }
}
