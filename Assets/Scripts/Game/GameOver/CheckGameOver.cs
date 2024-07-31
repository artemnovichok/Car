using UnityEngine;

public class CheckGameOver : MonoBehaviour
{
    private static float speedToGameOver = 0.2f;
    private Rigidbody rb;
    private Timer timer;
    private bool needCheck = true;

    private void OnEnable()
    {
        EventManager.Instance.cantMove.AddListener(stopCheck);
    }

    private void OnDisable()
    {
        EventManager.Instance.cantMove.RemoveListener(stopCheck);
    }

    private void stopCheck()
    {
        needCheck = false;
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        timer = GameObject.FindObjectOfType<Timer>();
    }
    private void Update()
    {
        if (!needCheck)
        {
            return;
        }
        if(rb.velocity.magnitude <= speedToGameOver)
        {
            timer.NeedTimer = true;
        }
        else
        {
            timer.NeedTimer = false;
        }
    }
}
