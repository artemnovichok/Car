using UnityEngine;

public class CountBlockOnStart : MonoBehaviour
{
    private int count;
    public void countBlock()
    {
        CarBlocksSettings[] carBlokSettings = GameObject.FindObjectsOfType<CarBlocksSettings>();
        count = carBlokSettings.Length;
    }

    public int GetCount()
    {
        return count;
    }
}
