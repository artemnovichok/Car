using System.Collections.Generic;
using UnityEngine;

public class CarWheel : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float raycastDistance = 2f;
    [SerializeField] private float speedWh = 1f;
    private float speed;
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

    private void Start()
    {
        CarBlocksSettings[] carBlocksSettings = GameObject.FindObjectsOfType<CarBlocksSettings>();
        List<CarBlocksSettings> carBlocksSettingsList = new List<CarBlocksSettings>(carBlocksSettings);
        foreach (CarBlocksSettings settings in carBlocksSettingsList)
        {
            if (settings.blockSettings.id == 4)
            {
                speed = ((int)settings.blockSettings.ourRarity + 1) * 3 * speedWh;
                break;
            }
        }
    }
    private void cantMove()
    {
        fastStop = true;
    }
    private void Update()
    {
        if (!needMove || !checkGround())
        {
            return;
        }
        if(fastStop)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else
        {
            Vector3 direction = transform.right;
            rb.AddForce(direction * speed, ForceMode.Force);
        }
    }

    private bool checkGround()
    {
        Vector3 direction = -transform.up;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, raycastDistance, LayerMask.GetMask("Game")))
        {
            if (hit.collider.CompareTag("ground"))
            {
                return true;
            }
        }
        else
        {
            return false;
        }
        return false;
    }

    private void startMove()
    {
        needMove = true;
    }

    private void stopMove()
    {
        needMove = false;
    }



    //отрисовка
    private void OnDrawGizmosSelected()
    { 
        Vector3 direction = -transform.up;
        Gizmos.color = checkGround() ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position, direction * raycastDistance);
    }
}
