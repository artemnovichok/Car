using UnityEngine;

public class Freez : MonoBehaviour
{
    private bool needRotate = false;

    private void OnEnable()
    {
        EventManager.Instance.updateFreez.AddListener(checkRotate);
    }

    private void OnDisable()
    {
        EventManager.Instance.updateFreez.RemoveListener(checkRotate);
    }

    private void checkRotate()
    {
        needRotate = true;
    }

    private void Update()
    {
        if (needRotate)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
}
