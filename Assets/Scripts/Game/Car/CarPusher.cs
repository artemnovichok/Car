using UnityEngine;

public class CarPusher : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool invertDirection = false;
    [SerializeField] private float speed = 1f;
    private bool needMove = false;
    private bool fastStop = false;

    private void OnEnable()
    {
        EventManager.Instance.startMoveCar.AddListener(startMove);
        EventManager.Instance.stopMoveCar.AddListener(stopMove);
        EventManager.Instance.cantMove.AddListener(cantMove);
    }

    private void OnDisable()
    {
        EventManager.Instance.startMoveCar.RemoveListener(startMove);
        EventManager.Instance.stopMoveCar.RemoveListener(stopMove);
        EventManager.Instance.cantMove.RemoveListener(cantMove);
    }

    private void cantMove()
    {
        fastStop = true;
    }

    private void Update()
    {
        if (needMove)
        {
            if(fastStop)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            else
            {
                Vector3 direction = transform.right;
                if (invertDirection)
                {
                    direction *= -1;
                }
                rb.AddForce(direction * speed, ForceMode.Force);
            }
        }
    }

    private void startMove()
    {
        needMove = true;
    }

    private void stopMove()
    {
        needMove = false;
    }

    private void OnDrawGizmos()
    {
            Vector3 direction = transform.right;
        if (invertDirection)
        {
            direction *= -1; 
        }
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, direction * speed);
    }
}
