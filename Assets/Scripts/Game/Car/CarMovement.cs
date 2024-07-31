using TouchControlsKit;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private bool needCheck = true;
    private void OnEnable()
    {
        EventManager.Instance.cantMove.AddListener(cantMove);
    }

    private void OnDisable()
    {
        EventManager.Instance.cantMove.RemoveListener(cantMove);
    }

    private void cantMove()
    {
        needCheck = false;
    }

    private void Update()
    {
        if (!needCheck)
        {
            return;
        }
        if(TCKInput.GetButtonDown("GoButton"))
        {
            EventManager.Instance.StartMoveCar();
        }
        if(TCKInput.GetButtonUp("GoButton"))
        {
            EventManager.Instance.StopMoveCar();
        }
    }
}
