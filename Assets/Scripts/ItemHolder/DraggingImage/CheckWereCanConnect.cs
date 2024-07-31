using Unity.VisualScripting;
using UnityEngine;
using static System.Net.WebRequestMethods;

public class CheckWereCanConnect : MonoBehaviour
{
    [Header("С какой стороны могу присоединиться")]
    [SerializeField] private bool canConnectUp;
    [SerializeField] private bool canConnectDown;
    [SerializeField] private bool canConnectLeft;
    [SerializeField] private bool canConnectRight;

    [Space(5)]

    [Header("Поворот")]
    [SerializeField] private float degreeUp = 0;
    [SerializeField] private float degreeDown = 0;
    [SerializeField] private float degreeLeft = 0;
    [SerializeField] private float degreeRight = 0;

    public bool CanConnectUp()
    {
        return canConnectUp;
    }
    public bool CanConnectDown()
    {
        return canConnectDown;
    }
    public bool CanConnectLeft()
    {
        return canConnectLeft;
    }
    public bool CanConnectRight()
    {
        return canConnectRight;
    }

    public bool CheckConnection(out float DegreeRotate, out bool ConnectUp, out bool ConnectDown, out bool ConnectLeft, out bool ConnectRight)
    {
        DegreeRotate = 0;
        ConnectUp = false;
        ConnectDown = false;
        ConnectLeft = false;
        ConnectRight = false;

        RaycastHit2D[] hitUp = Physics2D.RaycastAll(transform.position, Vector2.up, 140f, LayerMask.GetMask("BlockImageLayer"));
        RaycastHit2D[] hitDown = Physics2D.RaycastAll(transform.position, Vector2.down, 140f, LayerMask.GetMask("BlockImageLayer"));
        RaycastHit2D[] hitLeft = Physics2D.RaycastAll(transform.position, Vector2.left, 140f, LayerMask.GetMask("BlockImageLayer"));
        RaycastHit2D[] hitRight = Physics2D.RaycastAll(transform.position, Vector2.right, 140f, LayerMask.GetMask("BlockImageLayer"));


        if (canConnectUp)
        {
            foreach(RaycastHit2D ray in hitUp)
            { 
                if(ray.collider.gameObject != gameObject && ray.collider.gameObject.GetComponent<CheckWereCanConnect>().CanConnectDown())
                {
                    DegreeRotate = degreeUp;
                    ConnectUp = true;
                }
            }
        }

        if (canConnectDown)
        {
            foreach (RaycastHit2D ray in hitDown)
            {
                if (ray.collider.gameObject! != gameObject && ray.collider.gameObject.GetComponent<CheckWereCanConnect>().CanConnectUp())
                {
                    DegreeRotate = degreeDown;
                    ConnectDown = true;
                }
            }
        }

        if (canConnectLeft)
        {
            foreach (RaycastHit2D ray in hitLeft)
            {
                if (ray.collider.gameObject! != gameObject && ray.collider.gameObject.GetComponent<CheckWereCanConnect>().CanConnectRight())
                {
                    DegreeRotate = degreeLeft;
                    ConnectLeft = true;
                }
            }
        }
        if (canConnectRight)
        {
            foreach (RaycastHit2D ray in hitRight)
            {
                if (ray.collider.gameObject! != gameObject && ray.collider.gameObject.GetComponent<CheckWereCanConnect>().CanConnectLeft())
                {
                    DegreeRotate = degreeRight;
                    ConnectRight = true;
                }
            }
        }


        bool customUp = false;
        bool customDown = false;
        bool customLeft = false;
        bool customRight = false;

        foreach(RaycastHit2D ray in hitUp)
        {
            if(ray.collider.gameObject != gameObject)
            {
                customUp = true;
            }
        }

        foreach(RaycastHit2D ray in hitDown)
        {
            if (ray.collider.gameObject != gameObject)
            {
                customDown = true;
            }
        }

        foreach (RaycastHit2D ray in hitLeft)
        {
            if (ray.collider.gameObject != gameObject)
            {
                customLeft = true;
            }
        }

        foreach (RaycastHit2D ray in hitRight)
        {
            if (ray.collider.gameObject != gameObject)
            {
                customRight = true;
            }
        }

        if ((!customUp && !customDown && !customLeft && !customRight) ||ConnectUp || ConnectDown || ConnectLeft || ConnectRight)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    void OnDrawGizmos()
    {
        if (canConnectUp)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * 140f);
        }
        if (canConnectDown)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 140f);
        }
        if (canConnectLeft)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.left * 140f);
        }
        if (canConnectRight)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.right * 140f);
        }
    }
}
