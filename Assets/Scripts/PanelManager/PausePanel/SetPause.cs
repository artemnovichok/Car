using UnityEngine;

public class SetPause: MonoBehaviour
{
    public void SetPauses()
    {
        PauseManager.Instance.OnPause();
    }
}
